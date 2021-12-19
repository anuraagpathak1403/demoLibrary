using demoLibrary.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demoLibrary.Entities
{
    public class assetLibraryManagement
    {
        [Key]
        public int Id { get; set; }
        public string enrollmentId { get; set; }
        public int barcodeId { get; set; }
        [ForeignKey("bookId")]
        public int assetId { get; set; }
        public assetLibrary bookId { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime returnDate { get; set; }
        public double lateFine { get; set; }

        public static explicit operator assetLibraryManagement(assetLibraryManagementDTO v)
        {
            throw new NotImplementedException();
        }
    }
}