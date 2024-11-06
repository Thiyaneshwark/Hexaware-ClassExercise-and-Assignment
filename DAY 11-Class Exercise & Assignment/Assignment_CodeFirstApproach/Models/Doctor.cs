using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment_CodeFirstApproach.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Specialty { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
