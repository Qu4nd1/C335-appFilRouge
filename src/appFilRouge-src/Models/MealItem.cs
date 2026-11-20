using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace appFilRouge.Models
{
    public class MealItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MealId { get; set; }
        public Guid IngredientId { get; set; }
        public double QuantityInGrams { get; set; }
        [JsonIgnore] public Ingredient? Ingredient { get; set; }
        [JsonIgnore] public double Calories => (Ingredient?.CaloriesPer100g ?? 0) * QuantityInGrams / 100;
        [JsonIgnore] public double Proteins => (Ingredient?.ProteinsPer100g ?? 0) * QuantityInGrams / 100;
    }
}
