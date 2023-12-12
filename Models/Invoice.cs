using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PetCommunity.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InvoiceCode { get; set; }
        public string TimeGenerate { get; set; }
        public string TimeChange { get; set; }
        public double InvoiceAmount { get; set; }
        public double AmountChange { get; set; }
        public double Discount { get; set; }
        public string Note { get; set; }


        public int CaseId { get; set; }
        public Case Cases = new Case();

    }
}
