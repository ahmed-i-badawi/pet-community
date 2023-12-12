using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommunity.Models
{
    public class PetsType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pet> Pets { get; set; }
        public List<Specie> Species { get; set; }
    }
}
