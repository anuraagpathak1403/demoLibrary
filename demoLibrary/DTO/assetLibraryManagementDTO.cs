using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoLibrary.DTO
{
    public class assetLibraryManagementDTO
    {
        public string enrollmentId { get; set; }
        public int barcodeId { get; set; }
        public int assetId { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime returnDate { get; set; }
        public double lateFine { get; set; }
    }
}