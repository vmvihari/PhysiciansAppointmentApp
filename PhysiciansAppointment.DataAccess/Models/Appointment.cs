using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PhysiciansAppointment.DataAccess.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string PatientFirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string PatientLastName { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public Kind Kind { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }
    }

    public enum Kind
    {
        [EnumMember(Value = "New Patient")]
        NewPatient,
        [EnumMember(Value = "Follow-up")]
        Followup
    }
}
