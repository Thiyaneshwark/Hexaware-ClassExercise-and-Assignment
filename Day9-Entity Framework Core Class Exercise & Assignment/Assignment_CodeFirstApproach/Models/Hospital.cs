using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Assignment_CodeFirstApproach.Models
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
