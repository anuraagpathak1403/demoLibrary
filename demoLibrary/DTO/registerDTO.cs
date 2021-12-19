using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demoLibrary.DTO
{
    public class registerDTO
    {
        [Required] public string firstName { get; set; }
        [Required] public string lastName { get; set; }
        [Required] public string fatherName { get; set; }
        [Required] public string motherName { get; set; }
        public string password { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        public DateTime created { get; set; } = DateTime.Now;
        [Required] public string gender { get; set; }
        [Required] public string city { get; set; }
        [Required] public string country { get; set; }
        [Required] public bool boolLibrary { get; set; }
    }
}