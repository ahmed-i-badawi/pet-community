using Microsoft.AspNetCore.Http;
using PetCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.ViewModel
{
    public class doctorViewModel
    {
        public int Id { get; set; }
        public int WorkMobile { get; set; }
        public string MedicalSpeciality { get; set; }
        public string WorkAddress { get; set; }
        public string WorkPhone { get; set; }
        public string Notes { get; set; }
        public string WorkEmail { get; set; }
        public List<Course> Courses { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }

        // -

        public IList<Learn> Learns { get; set; }
        public IList<Service> Services { get; set; }
        public IList<Facility> Facilities { get; set; }

    }
}
