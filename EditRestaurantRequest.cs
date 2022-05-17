using System;
namespace TrainingASP.Models
{
    public class EditRestaurantRequest
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int TableAmount { get; set; }
        public string Address { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public string PhoneNumber { get; set; }
        public string Tables { get; set; }

        public EditRestaurantRequest()
        {
        }
    }
}
