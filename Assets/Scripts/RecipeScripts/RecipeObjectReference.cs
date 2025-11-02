using System;
using System.Collections.Generic;
using RecipeScripts.RecipeItemsScripts;

namespace RecipeScripts
{
    [Serializable]
    public class RecipeObjectReference
    {
        public int RecipeDifficulty;
        public List<AbstractRecipeItem> Recipe;
    }
}