using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingASP.Models
{
    public class CreateRestaurantRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int TableAmount { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime OpeningTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime ClosingTime { get; set; }
        public string PhoneNumber { get; set; }

        public CreateRestaurantRequest()
        {
        }
    }
}
