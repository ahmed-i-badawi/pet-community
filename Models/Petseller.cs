using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCommunity.Models
{
    public class PetSeller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pet> Pets { get; set; }
        public IList<User> Users { get;  set; }

    }
}
