using System;
using System.Collections.Generic;

namespace TrainingASP.Models
{
    public class RestaurantDto
    {

        public string Name { get; set; }
        public int ID { get; set; }
        public string Address { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public string PhoneNumber { get; set; }
        public List<Table> Tables { get; set; }

        public RestaurantDto()
        {
        }
    }
}
