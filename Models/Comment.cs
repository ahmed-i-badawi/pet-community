using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string  Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        [DisplayName("User Name")]
        public string UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }

    }
}
