using demoLibrary.Data;
using demoLibrary.DTO;
using demoLibrary.Entities;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace demoLibrary.Controllers
{
    [RoutePrefix("api/registration")]
    public class registrationController : ApiController
    {
        public readonly dataContext _context;
        public registrationController(dataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public virtual async Task<userDTO> registerAsync(registerDTO registerDTO)
        {
            var hmac = new HMACSHA512();
            registration registerUser = new registration();
            registerUser.firstName = registerDTO.firstName;
            registerUser.lastName = registerDTO.lastName;
            registerUser.boolLibrary = true;
            registerUser.DateOfBirth = registerDTO.DateOfBirth;
            registerUser.gender = registerDTO.gender;
            registerUser.motherName = registerDTO.motherName;
            registerUser.fatherName = registerDTO.fatherName;
            registerUser.city = registerDTO.city;
            registerUser.country = registerDTO.country;
            registerUser.password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password));
            registerUser.passwordSalt = hmac.Key;
            var fullName = registerUser.firstName + " " + registerUser.lastName;
            Random rnd = new Random();
            int value = rnd.Next(10000, 99999);
            registerUser.enrollmentId = registerDTO.firstName.Substring(0, 2).ToUpper() + registerDTO.lastName.Substring(0, 2).ToUpper() + value;

            byte[] qrCode = generateQrCode(fullName);
            registerUser.qrCode = qrCode;
            _context.registrations.Add(registerUser);
            await _context.SaveChangesAsync();
            return new userDTO
            {
                enrollmentId = registerUser.enrollmentId,
                firstName = registerUser.firstName,
                lastName = registerUser.lastName,
                fatherName = registerUser.fatherName,
                motherName = registerUser.motherName,
                qrCode = registerUser.qrCode,
            };
        }
        public byte[] generateQrCode(string fullName)
        {
            string code = fullName;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 150;
            imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    return byteImage;
                }
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<userDTO> login(loginDTO loginDTO)
        {
            var registerUser = await _context.registrations
                .SingleOrDefaultAsync(x => x.enrollmentId.ToLower() == loginDTO.enrollmentId.ToLower());

            if (registerUser == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    ReasonPhrase = "Invalid Username !"
                };
                throw new HttpResponseException(msg);
            }

            var hmac = new HMACSHA512(registerUser.passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != registerUser.password[i])
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Invalid Password !"
                    };
                    throw new HttpResponseException(msg);
                }

            return new userDTO
            {
                enrollmentId = registerUser.enrollmentId,
                firstName = registerUser.firstName,
                lastName = registerUser.lastName,
                fatherName = registerUser.fatherName,
                motherName = registerUser.motherName,
                qrCode = registerUser.qrCode,
            };
        }

    }
}