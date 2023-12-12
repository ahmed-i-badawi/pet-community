
using Microsoft.AspNetCore.Identity;
using PetCommunity.Data.Enums;
using PetCommunity.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PetCommunity.Models
{
    public class User : IdentityUser
    {
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        public string  Email { get; set; }
        [DisplayName("Phone")]
        public string  PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserType Type { get; set; }
        public List<Pet> Pets { get; set; }
        public Doctor Doctor { get; set; }
        public List<Post> Posts { get; set; }
        public List<DocktorCreationRequest> DocktorCreationRequests { get; set; }
        public List<PetSeller> PetSeller { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Course> Courses { get; set; }


    }
}
