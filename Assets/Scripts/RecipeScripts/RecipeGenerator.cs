using System.Collections.Generic;
using RecipeScripts.RecipeItemsScripts;

namespace RecipeScripts
{
    public class RecipeGenerator
    {
        public RecipesDescription Description;
        
        public RecipeGenerator(RecipesDescription description)
        {
            description = description;
        }

        public RecipeObject GenerateAndReturnRecipeObject(int difficulty)
        {
            List<AbstractRecipeItem> recipeItems = Description.Recipes.Find(x=>x.RecipeDifficulty == difficulty).Recipe;
            RecipeObject recipeObject = new RecipeObject(recipeItems);
            return recipeObject;
        }
    }
}
