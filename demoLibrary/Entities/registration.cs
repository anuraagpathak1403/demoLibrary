using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demoLibrary.Entities
{
    public class registration
    {
        [Key]
        public int Id { get; set; }
        public string enrollmentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public byte[] password { get; set; }
        public byte[] passwordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime created { get; set; } = DateTime.Now;
        public string gender { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public bool boolLibrary { get; set; }
        public byte[] qrCode { get; set; }

    }
}