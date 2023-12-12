using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class ServicePlanned
    {

            public int Id { get; set; }


            public string Planned_Start_time { get; set; }
            public string Planned_End_time { get; set; }
        public float Planned_units { get; set; }
        public float Cost_per_unit { get; set; }
        public float Planned_price { get; set; }
        public string Note { get; set; }
           

        public int CaseId { get; set; }
        public Case Cases = new Case();
        public int ServiceId { get; set; }
        public Service Services = new Service();

    }
    }

