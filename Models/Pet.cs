using Microsoft.AspNetCore.Http;
using PetCommunity.Data.Enums;
using PetCommunity.Models.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PetCommunity.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        public PetGender Gender { get; set; }
        public string Color { get; set; }
        public string Notes { get; set; }
        [DisplayName("Pet Image")]
        public string ImageUrl { get; set; }
        public int CaseId { get; set; }
        public List<Case> Cases { get; set; }
        [DisplayName("User Name")]
        public string UserId { get; set; }
        public User User { get; set; }
        [NotMapped]
        [DisplayName("Pet Species")]
        public int SpeciesPetId { get; set; }
        [NotMapped]
        public Specie SpeciesPet { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        [DisplayName("Pet Type")]
        public int PetTypeId { get; set; }
        public PetsType PetType { get; set; }
        public List<PetSeller> PetSeller { get; set; }

    }
}

