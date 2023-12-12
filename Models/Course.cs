using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PetCommunity.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Specialization { get; set; }
        public string Source { get; set; }
        [DisplayName("Upload Time")]
        public DateTime UploadTime{ get; set; }
        public string Language { get; set; }
        [DisplayName("Paid")]
        public bool IsPaid { get; set; }
        [DisplayName("Doctor Name")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [DisplayName("Profile Image")]
        public string ImageUrl { get; set; }

    }
}

