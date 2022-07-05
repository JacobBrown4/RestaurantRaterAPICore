using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterAPI.Models
{
    public class RestaurantListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double AverageScore { get; set; }
    }
}