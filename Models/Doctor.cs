using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PetCommunity.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [DisplayName("Work Mobile")]
        public int WorkMobile { get; set; }
        [DisplayName("Medical Speciality")]
        public string MedicalSpeciality { get; set; }
        [DisplayName("Work Address")]
        public string WorkAddress { get; set; }
        [DisplayName("Work Phone")]
        public string WorkPhone { get; set; }
        public string Notes { get; set; }
        [DisplayName("Work Email")]
        public string WorkEmail { get; set; }
        public List<Course> Courses { get; set; }
        [DisplayName("User Name")]
        public string UserId { get; set; }
        public User User { get; set; }
        [DisplayName("Profile Image")]
        public string ImageUrl { get; set; }


        // -

        public IList<Learn> Learns { get; set; }
        public IList<Service> Services { get; set; }
        public IList<Facility> Facilities { get; set; }

    }


}
