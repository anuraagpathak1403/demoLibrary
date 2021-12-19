using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demoLibrary.Entities
{
    public class assetLibrary
    {
        [Key]
        public int bookId { get; set; }
        public string bookName { get; set; }
        public string category { get; set; }
        public string authorName { get; set; }
        public string columnWise { get; set; }
        public byte[] bookBarcodeId { get; set; }
    }
}