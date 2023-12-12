using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAvaible { get; set; }
        public double CostPerUnit { get; set; }


        public IList<PUnit> Units { get; private set; } = new List<PUnit>();
        public IList<Specie> Species { get; private set; } = new List<Specie>();
        public IList<Facility> Facilities { get; private set; } = new List<Facility>();
        public IList<ServiceProvided> ServiceProvideds { get; private set; } = new List<ServiceProvided>();
        public IList<ServicePlanned> ServicePlanneds { get; private set; } = new List<ServicePlanned>();
        public IList<User> Users { get; private set; } = new List<User>();
        public IList<Doctor> Doctors { get; private set; } = new List<Doctor>();
    }
}

