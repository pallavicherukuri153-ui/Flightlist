using System;
using System.ComponentModel.DataAnnotations;

namespace FlightsList.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Flight Number")]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string From { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string To { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, 999999)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
