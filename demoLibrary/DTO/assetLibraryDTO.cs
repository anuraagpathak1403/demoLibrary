using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoLibrary.DTO
{
    public class assetLibraryDTO
    {
        public string bookName { get; set; }
        public string category { get; set; }
        public string authorName { get; set; }
        public string columnWise { get; set; }
        public byte[] bookBarcodeId { get; set; }
    }
}