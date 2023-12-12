using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class Learn
    {
        public int Id { get; set; }
        public string Tips { get; set; }

        public List<User> Users { get; set; }
    }
}

