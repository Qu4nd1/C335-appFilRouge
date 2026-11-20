using appFilRouge.Models;

namespace appFilRouge.Services
{
    public interface IDataService
    {
        /**
         * Ingredient Methods
         */
        // Ingredient list screen
        Task<List<Ingredient>> GetIngredientsAsync();

        // Swipe to delete: true = deleted, false = refused (ingredient used in a meal)
        Task<bool> DeleteIngredientAsync(Guid id);

        // Used to build the message "Used in: Lunch, Dinner" (empty list = unused)
        Task<List<Meal>> GetMealsUsingIngredientAsync(Guid ingredientId);

        // New ingredient form
        Task SaveIngredientAsync(Ingredient ingredient);

        /**
         * Meal Methods
         */
        // Meal list screen
        Task<List<Meal>> GetMealsAsync();

        // New meal form
        Task SaveMealAsync(Meal meal);

        // Swipe to delete meal
        Task DeleteMealAsync(Guid id);
    }
}
