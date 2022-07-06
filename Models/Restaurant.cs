using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public double AverageScore
        {
            get
            {
                return (AverageAtmosphereScore + AverageCleanlinessScore + AverageFoodScore) / 3;
            }
        }
        public double AverageFoodScore
        {
            get
            {
                if (Ratings.Count == 0)
                {
                    return 0;
                }
                double total = 0;
                foreach (Rating rating in Ratings)
                {
                    total += rating.FoodScore;
                }
                return total / Ratings.Count;
            }
        }
        public double AverageAtmosphereScore
        {
            get
            {
                if (Ratings.Count == 0)
                {
                    return 0;
                }
                double total = 0;
                foreach (Rating rating in Ratings)
                {
                    total += rating.AtmosphereScore;
                }
                return total / Ratings.Count;
            }
        }
        public double AverageCleanlinessScore
        {
            get
            {
                if (Ratings.Count == 0)
                {
                    return 0;
                }
                double total = 0;
                foreach (Rating rating in Ratings)
                {
                    total += rating.CleanlinessScore;
                }
                return total / Ratings.Count;
            }
        }
    }
}