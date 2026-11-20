using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;



namespace appFilRouge.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore] public double TotalCalories => Items.Sum(i => i.Calories);
        [JsonIgnore] public double TotalProteins => Items.Sum(i => i.Proteins);
        public List<MealItem> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; }

        public Meal()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
