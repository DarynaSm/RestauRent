using System;
using System.Collections.Generic;

namespace TrainingASP.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Address { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public string PhoneNumber { get; set; }
        public string Tables { get; set; }

        public Restaurant()
        {

        }
    }
    public class Table
    {
        //public string  Type { get; set; }
        public int CustomerCount { get; set; }
        public int Price { get; set; }
        public int Number { get; set; }
        public List<TimeSlot> Time { get; set; }


        public Table()
        {
        }

        public bool CheckFreeTime(DateTime t, int hours)
        {
            for (int i = 0; i < Time.Count; i++)
            {

                List<bool> Free = new List<bool>();
                if (t == Time[i].StartTime)
                {
                    for (int j = 0; j <= hours - 1; j++)
                    {
                        if (t.AddHours(j) == Time[i + j].StartTime && Time[i + j].IsAvailable)
                        {
                            Free.Add(true);
                        }
                        else
                        {
                            Free.Add(false);
                        }
                    }
                    var a = Free.TrueForAll(x => x);
                    return a;
                }
            }
            return false;
        }

        public class TimeSlot
        {
            public DateTime Date { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public bool IsAvailable { get; set; } = true;

            public TimeSlot()
            {
            }
        }
    }
}




