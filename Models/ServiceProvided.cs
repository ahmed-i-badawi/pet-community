using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class ServiceProvided
    {
       
            public int Id { get; set; }

            public string Start_time { get; set; }
            public string End_time { get; set; }
            public float Units { get; set; }
            public float Cost_per_unit { get; set; }
            public float Price_changed { get; set; }
            public string Note { get; set; }


            public int CaseId { get; set; }
            public Case Cases = new Case();
            public int ServiceId { get; set; }
            public Service Services = new Service();

        }
    }
