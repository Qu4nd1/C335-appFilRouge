using appFilRouge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace appFilRouge.Services
{
    public class JsonDataService : IDataService
    {
        // Shape of data.json
        private class Store
        {
            public List<Ingredient> Ingredients { get; set; } = new();
            public List<Meal> Meals { get; set; } = new();
        }

        // Readable file while you debug
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        // Where the file lives on the device
        private readonly string _path =
            Path.Combine(FileSystem.AppDataDirectory, "data.json");

        // Allows only one operation at a time on the data
        private readonly SemaphoreSlim _lock = new(1, 1);

        // In-memory cache, null until the first load
        private Store? _store;

        private async Task<Store> LoadAsync()
        {
            if (_store is not null) return _store;
            if (File.Exists(_path))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(_path);
                    _store = JsonSerializer.Deserialize<Store>(json);
                }
                catch (JsonException)
                {
                    // Use of System.Diagnostics.Debug.WriteLine for the message to appear on IOS or Android
                    System.Diagnostics.Debug.WriteLine("Corrupted File: The Json file seems to have an issue !");

                    // Backup of the corrupted file
                    File.Copy(_path, _path + ".bak", overwrite: true);
                }
            }

            _store ??= new Store();
            return _store;
        }

        private async Task SaveAsync()
        {
            // Transforms _store object to Json string
            string content = JsonSerializer.Serialize(_store!, Options);
            
            // Temporary file path
            string tmp = _path + ".tmp";

            // Write the content to the tmp file
            await File.WriteAllTextAsync(tmp, content);

            // Renames data.json.tmp to data.json & overwrite : true because data.json already exsits
            File.Move(tmp, _path, overwrite: true);
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            await _lock.WaitAsync();
            try
            {
                var s = await LoadAsync();
                return s.Ingredients.OrderBy(i => i.Name).ToList();
            }
            finally { _lock.Release(); }
        }

        public async Task SaveIngredientAsync(Ingredient ingredient)
        {
            await _lock.WaitAsync();
            try
            {
                var s = await LoadAsync();
                var index = s.Ingredients.FindIndex(i => i.Id == ingredient.Id);
                if (index >= 0)
                {
                    s.Ingredients[index] = ingredient;
                }
                else { s.Ingredients.Add(ingredient); }
                await SaveAsync();
            }
            finally { _lock.Release(); }
        }

        public async Task<bool> DeleteIngredientAsync(Guid id)
        {
            await _lock.WaitAsync();
            try
            {
                var s = await LoadAsync();
                // Check if any meals has in its items the id corresponding to the ingredient to be deleted
                bool inUse = s.Meals.Any(m => m.Items.Any(item => item.IngredientId == id));
                if (inUse) { return false; }
                s.Ingredients.RemoveAll(i => i.Id == id);
                await SaveAsync();
                return true;
            }
            finally { _lock.Release(); }
        }

        Task IDataService.DeleteMealAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<List<Meal>> IDataService.GetMealsAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<Meal>> IDataService.GetMealsUsingIngredientAsync(Guid ingredientId)
        {
            throw new NotImplementedException();
        }

        

        Task IDataService.SaveMealAsync(Meal meal)
        {
            throw new NotImplementedException();
        }
    }
}
