using demoLibrary.Data;
using demoLibrary.DTO;
using System;
using System.Collections.Generic;
using demoLibrary.Entities;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace demoLibrary.Controllers
{
    [RoutePrefix("api/assetLibrary")]
    public class assetLibraryController : ApiController
    {
        public readonly dataContext _context;
        public assetLibraryController(dataContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("assetInsert")]
        public virtual async Task<assetLibraryDTO> assetLibraryAsync(assetLibraryDTO registerDTO)
        {
            assetLibrary assetLibrary = new assetLibrary();
            assetLibrary.category=registerDTO.category;
            assetLibrary.authorName=registerDTO.authorName;
            assetLibrary.columnWise=registerDTO.columnWise;
            assetLibrary.bookName=registerDTO.bookName;
            byte[] generatedBookBarcode=generateBookBarcode(assetLibrary.bookName);
            assetLibrary.bookBarcodeId = generatedBookBarcode;
            _context.assetLibraries.Add(assetLibrary);
            await _context.SaveChangesAsync();
            return new assetLibraryDTO()
            {
                bookName = registerDTO.bookName,
                columnWise = registerDTO.columnWise,
                authorName = registerDTO.authorName,
                category = registerDTO.category,
                bookBarcodeId = registerDTO.bookBarcodeId,
            };
        }

        private byte[] generateBookBarcode(string bookName)
        {
            string barCode = bookName;
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M Free Version", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    byte[] ImageUrl = ms.ToArray();
                    return ImageUrl;
                }
            }
        }
    }
}