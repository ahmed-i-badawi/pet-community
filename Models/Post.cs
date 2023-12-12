using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace PetCommunity.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [DisplayName("Post Wallpaper")]
        public string ImageUrl { get; set; }
        public string Specialization { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; }
        [DisplayName("User Name")]
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}