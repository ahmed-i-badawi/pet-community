using Microsoft.AspNetCore.Http;
using PetCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.ViewModel
{
    public class DocktorCreationRequestViewModel
    {
        public IFormFile File { get; set; }
        public int Id { get; set; }
        public int WorkMobile { get; set; }
        public string MedicalSpeciality { get; set; }
        public string WorkAddress { get; set; }
        public string WorkPhone { get; set; }
        public string Notes { get; set; }
        public string WorkEmail { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
