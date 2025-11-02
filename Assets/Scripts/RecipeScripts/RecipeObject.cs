using System;
using System.Collections.Generic;
using RecipeScripts.RecipeItemsScripts;

namespace RecipeScripts
{
    public class RecipeObject
    {
        public List<AbstractRecipeItem> ReferenceRecipeItems;

        public Action OnRecipeComplete;
        public bool IsRecipeCompleted;
        
        public int CurrentItemIndex;

        public RecipeObject(List<AbstractRecipeItem> referenceRecipeItems)
        {
            this.ReferenceRecipeItems = referenceRecipeItems;
        }

        public bool CheckNextRecipeItem(AbstractRecipeItem abstractRecipeItem)
        {
            return ReferenceRecipeItems[CurrentItemIndex].GetType() == abstractRecipeItem.GetType();
        }

        public void AddRecipeItem()
        {
            CurrentItemIndex++;
        }
    }
}