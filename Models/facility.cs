using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string ContantPerson { get; set; }

        public int CaseId { get; set; }
        public Case Cases = new Case();

        public IList<Service> Services { get; private set; } = new List<Service>();

    }
}

