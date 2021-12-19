using demoLibrary.Data;
using demoLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoLibrary.Entities;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using demoLibrary.Repository;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using System.Net.Http;
using System.Net;

namespace demoLibrary.Controllers
{
    [RoutePrefix("api/assetLibraryManagement")]
    public class assetLibraryManagementController : ApiController
    {
        public readonly dataContext _context;
        public readonly assetLibraryManagementRepository _assetLibraryManagementRepository;
        public readonly assetLibraryRepository _assetLibraryRepository;
        public assetLibraryManagementController(dataContext context, assetLibraryManagementRepository assetLibraryManagementRepository, assetLibraryRepository assetLibraryRepository)
        {
            _context = context;
            _assetLibraryManagementRepository = assetLibraryManagementRepository;
            _assetLibraryRepository = assetLibraryRepository;
        }
        [HttpPost]
        [Route("assetLibraryManagement")]
        public virtual async Task<assetLibraryManagementDTO> assetLibraryManagementAsync(assetLibraryManagementDTO assetLibraryManagementDTO)
        {
            assetLibraryManagement assetLibraryManagement = new assetLibraryManagement();
            assetLibraryManagement.assetId = assetLibraryManagementDTO.assetId;
            assetLibraryManagement.enrollmentId = assetLibraryManagementDTO.enrollmentId;
            assetLibraryManagement.issueDate = assetLibraryManagementDTO.issueDate;
            assetLibraryManagement.returnDate = assetLibraryManagementDTO.issueDate.AddDays(7);
            assetLibraryManagement.lateFine = 0;
            assetLibrary assetLibraryDetails = await _assetLibraryRepository.getAssetLibraryManagementAsync(assetLibraryManagement.assetId);
            assetLibraryManagement.barcodeId = assetLibraryDetails.bookId;

            _context.assetLibraryManagements.Add(assetLibraryManagement);
            await _context.SaveChangesAsync();
            return new assetLibraryManagementDTO
            {
                enrollmentId = assetLibraryManagement.enrollmentId,
                assetId = assetLibraryManagement.assetId,
                returnDate = assetLibraryManagement.returnDate,
                lateFine = assetLibraryManagement.lateFine,
                issueDate = assetLibraryManagement.issueDate,
                barcodeId = assetLibraryManagement.barcodeId,
            };
        }

        [HttpGet]
        [Route("getIssuedBook")]
        public async Task<assetLibraryManagement> getBookId(string enrollmentId)
        {
            var assetLibraryManagement = await _assetLibraryManagementRepository.getBookAssetLibraryManagementAsync(enrollmentId);
            DateTime currentDateTime = DateTime.Now;
            int dateDifference = (int)(assetLibraryManagement.returnDate - currentDateTime).TotalDays;
            if (dateDifference > 0)
                assetLibraryManagement.lateFine = 0;
            else
            {
                decimal value;
                if (decimal.TryParse(dateDifference.ToString(), out value))
                {
                    value = Math.Round(value);
                    var newValue = System.Math.Abs(value);
                    assetLibraryManagement.lateFine = ((double)(newValue * 2));
                }

            }
            assetLibrary assetLibraryDetails = await _assetLibraryRepository.getAssetLibraryManagementAsync(assetLibraryManagement.assetId);
            assetLibraryManagement.barcodeId = assetLibraryDetails.bookId;
            return (assetLibraryManagement)assetLibraryManagement;
        }

        [HttpPut]
        [Route("updateBookData")]
        public async Task<assetLibraryManagement> updateBookDataAsync(assetLibraryManagementDTO assetLibraryManagementDTO)
        {
            var assetLibraryManagement = await _assetLibraryManagementRepository.getBookAssetLibraryManagementAsync(assetLibraryManagementDTO.enrollmentId);
            assetLibraryManagement.assetId = assetLibraryManagementDTO.assetId;
            assetLibraryManagement.issueDate = assetLibraryManagementDTO.issueDate;
            assetLibraryManagement.lateFine=assetLibraryManagementDTO.lateFine;
            assetLibraryManagement.returnDate= assetLibraryManagementDTO.returnDate;
            _assetLibraryManagementRepository.update(assetLibraryManagement);
            if (await _assetLibraryManagementRepository.SaveAllAsync()) return (assetLibraryManagement)assetLibraryManagement;
            else
            {
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Failed to update data !"
                    };
                    throw new HttpResponseException(msg);
                }
            }
        }
    }
}