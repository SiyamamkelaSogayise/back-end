using System.ComponentModel.DataAnnotations;

namespace braidingweb.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        [Required]
        public string HairLength { get; set; }
        [Required]
        public string BraidType { get; set; }
        [Required]
        public string HairSize { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string ProofOfPayment { get; set; } // File path or reference to proof of payment

       
        public Booking()
        {
            // Initialize any default values or perform any other setup logic here
        }
    }
}
