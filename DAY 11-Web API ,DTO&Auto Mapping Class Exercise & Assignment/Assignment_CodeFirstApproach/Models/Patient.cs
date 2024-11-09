using System.ComponentModel.DataAnnotations;

namespace Assignment_CodeFirstApproach.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
