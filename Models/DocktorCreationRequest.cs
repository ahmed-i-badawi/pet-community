using PetCommunity.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PetCommunity.Models
{
    public class DocktorCreationRequest
    {
        public int Id { get; set; }
        [DisplayName("Work Mobile")]
        public int WorkMobile { get; set; }
        [DisplayName("Medical Speciality")]
        public string MedicalSpeciality { get; set; }
        [DisplayName("Work Address")]
        public string WorkAddress { get; set; }
        [DisplayName("Work Phone")]
        public string WorkPhone { get; set; }
        public string Notes { get; set; }
        [DisplayName("Profile Image")]
        public string ImageUrl { get; set; }
        [DisplayName("Work Email")]
        public string WorkEmail { get; set; }
        public DoctorRequestStatus Status { get; set; }
        [DisplayName("User Name")]
        public string UserId { get; set; }
        public User User { get; set; }
        [NotMapped]
        public bool HasCurrentRequest { get { return Status == DoctorRequestStatus.Processing; } }
    }
}
