using System.Collections.Generic;
using UnityEngine;

namespace RecipeScripts
{
    [CreateAssetMenu(menuName = "Create RecipesDescription", fileName = "RecipesDescription", order = 0)]
    public class RecipesDescription : ScriptableObject
    {
        public List<RecipeObjectReference> Recipes;
    }
}