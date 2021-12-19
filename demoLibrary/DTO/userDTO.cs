using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoLibrary.DTO
{
    public class userDTO
    {
        public string enrollmentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public byte[] qrCode { get; set; }

    }
}