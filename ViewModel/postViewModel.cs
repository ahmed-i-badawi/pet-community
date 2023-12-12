using Microsoft.AspNetCore.Http;
using PetCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.ViewModel
{
    public class postViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
        public string Specialization { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
