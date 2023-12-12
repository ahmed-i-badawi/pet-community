using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCommunity.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Note { get; set; }
        public bool IsClosed { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        // --
        public List<Facility> Facilities { get; set; }
        public List<ServiceProvided> ServiceProvideds { get; set; }
        public List<ServicePlanned> ServicePlanneds { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}