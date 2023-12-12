using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class Connect
    {//animal adoption
       // Searching for lost animals


            public int Id { get; set; }
        public string Name { get; set; }


        public IList<User> Users { get; private set; } = new List<User>();

        public IList<Pet> Pets { get; private set; } = new List<Pet>();
        }
}
