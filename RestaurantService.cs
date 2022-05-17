using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TrainingASP.Data;
using TrainingASP.Models;
using static TrainingASP.Models.Table;

namespace TrainingASP.Services
{
    public class RestaurantService: IRestaurantService
    {
        private readonly ApplicationDbContext _context;

        public RestaurantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddRestaurant(CreateRestaurantRequest request)
        {
            Restaurant res = new Restaurant();
            res.Name = request.Name;
            res.Address = request.Address;
            res.PhoneNumber = request.PhoneNumber;
            res.OpeningTime = request.OpeningTime;
            res.ClosingTime = request.ClosingTime;
            res.Tables = JsonConvert.SerializeObject(GetTable(request.TableAmount, request.OpeningTime, request.ClosingTime));

            _context.Restaurant.Add(res);
            _context.SaveChanges();
        }

        public RestaurantDto GetRestaurantDtoById(int? id)
        {
            var restaurant = GetRestaurantById(id);
            return ToDto(restaurant);
        }

        public Restaurant GetRestaurantById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var restaurant = _context.Restaurant.FirstOrDefault(m => m.ID == id);
            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
        }

        public List<RestaurantDto> GetAll()
        {
            return _context.Restaurant.Select(r => ToDto(r)).ToList();
        }

        private List<Table> GetTable(int c, DateTime openTime, DateTime closeTime)
        {
            int i = 0;
            var rand = new Random();
            List<Table> tables = new List<Table>();
            while (i < c)
            {
                Table t = new Table();
                t = GenerateTableInfo();
                t.Time = GetDateTimePairs(openTime, closeTime);
                tables.Add(t);
                i++;
            }

            return tables;
        }

        public static Table GenerateTableInfo()
        {
            Table t = new Table();
            var rand = new Random();


            t.CustomerCount = rand.Next(1, 8);
            t.Price = rand.Next(100, 1000);
            t.Number = rand.Next(1, 10);

            return t;
        }

        public static List<TimeSlot> GetDateTimePairs(DateTime fromDate, DateTime toDate)
        {
            TimeSpan bigInterval = toDate - fromDate;
            TimeSpan smallInterval = new TimeSpan(1, 0, 0);
            List<TimeSlot> returnValue = new List<TimeSlot>();
            DateTime temp = fromDate;

            while (temp < toDate)
            {
                DateTime currentFromDate = temp;
                DateTime currentToDate = temp + smallInterval;

                // Your logic

                temp = temp + smallInterval;
                returnValue.Add(new TimeSlot() { StartTime = currentFromDate, FinishTime = currentToDate });
            }

            return returnValue;
        }

        public static RestaurantDto ToDto(Restaurant res)
        {
            RestaurantDto resDto = new RestaurantDto();
            resDto.Name = res.Name;
            resDto.Address = res.Address;
            resDto.ID = res.ID;
            if (resDto.Tables != null)
            {
                resDto.Tables = JsonConvert.DeserializeObject<List<Table>>(res.Tables);
            }
            else
            {

            }
            resDto.OpeningTime = res.OpeningTime.ToShortTimeString();
            resDto.ClosingTime = res.ClosingTime.ToShortTimeString();
            resDto.PhoneNumber = res.PhoneNumber;
            return resDto;
        }
        public void Update(EditRestaurantRequest editedRestaurant)
        {
           Restaurant res = GetRestaurantById(editedRestaurant.ID);
            res.Name = editedRestaurant.Name;
            res.Address = editedRestaurant.Address;
            res.PhoneNumber = editedRestaurant.PhoneNumber;
            List<Table> tables = new List<Table>();
            if (res.Tables != null)
            {
                tables = JsonConvert.DeserializeObject<List<Table>>(res.Tables);
            }
            var OpeningTime = DateTime.Parse(editedRestaurant.OpeningTime);
            var ClosingTime = DateTime.Parse(editedRestaurant.ClosingTime);

            if (editedRestaurant.TableAmount != tables.Count ||
                editedRestaurant.OpeningTime != res.OpeningTime.ToShortTimeString() ||
                editedRestaurant.ClosingTime != res.OpeningTime.ToShortTimeString())
            {
                res.Tables = JsonConvert.SerializeObject(GetTable(editedRestaurant.TableAmount, OpeningTime, ClosingTime));
            }
            res.OpeningTime = OpeningTime;
            res.ClosingTime = ClosingTime;

            _context.SaveChanges();
        }
    }

    public interface IRestaurantService
    {
        void AddRestaurant(CreateRestaurantRequest request);
        List<RestaurantDto> GetAll();
        RestaurantDto GetRestaurantDtoById(int? id);
        void Update(EditRestaurantRequest editedRestaurant);
    }
}
