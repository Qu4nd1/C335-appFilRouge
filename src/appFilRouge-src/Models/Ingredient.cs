using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace appFilRouge.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CaloriesPer100g { get; set; }
        public double ProteinsPer100g { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore] public string Initials => Name[..2].ToUpper();
        public Ingredient() 
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
    
}
