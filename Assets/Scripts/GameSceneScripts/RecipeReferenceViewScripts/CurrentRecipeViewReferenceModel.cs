using RecipeScripts;
using UnityEngine;

namespace GameSceneScripts.RecipeReferenceViewScripts
{
    public class CurrentRecipeViewReferenceModel
    {
        public CurrentRecipeViewReferenceContainer Container;

        public CurrentRecipeViewReferenceModel(CurrentRecipeViewReferenceContainer container)
        {
            Container = container;
        }

        public void UpdateView(RecipeObject recipeObject)
        {
            if (!IsRenderersEnought(recipeObject))
                GenerateRenderers(recipeObject.ReferenceRecipeItems.Count - Container.RecipeItemsRenderers.Count);
            SortRecipeItemsRenderers(recipeObject);
            SetRecipeItemsRenderersCurrentView(recipeObject);

            SetRecipeItemsRendererCurrentItem(recipeObject.CurrentItemIndex);
        }

        private void SetRecipeItemsRendererCurrentItem(int recipeObjectCurrentItemIndex)
        {
            for (int i = 0; i < Container.RecipeItemsRenderers.Count; i++)
            {
                var containerRecipeItemsRenderer = Container.RecipeItemsRenderers[i];
                string animationName = i==recipeObjectCurrentItemIndex ? 
                    containerRecipeItemsRenderer.SelectedAnimationName : containerRecipeItemsRenderer.NormalAnimationName;
                containerRecipeItemsRenderer.RendererAnimation.Play(animationName);
            }
        }

        private void SetRecipeItemsRenderersCurrentView(RecipeObject recipeObject)
        {
            for (var index = 0; index < recipeObject.ReferenceRecipeItems.Count; index++)
            {
                var referenceRecipeItem = recipeObject.ReferenceRecipeItems[index];
                Container.RecipeItemsRenderers[index].RecipeItemRenderer.sprite = referenceRecipeItem.RecipeItemSprite;
            }
        }

        private void SortRecipeItemsRenderers(RecipeObject recipeObject)
        {
            var position = Container.RecipeRenderersAnchor.position;
            for (int i = 0; i < Container.RecipeItemsRenderers.Count; i++)
            {
                var containerRecipeItemsRenderer = Container.RecipeItemsRenderers[i];
                containerRecipeItemsRenderer.RecipeItemRenderer.enabled = i < recipeObject.ReferenceRecipeItems.Count;
                var offset = i == recipeObject.CurrentItemIndex ? 
                    containerRecipeItemsRenderer.SelectedPositionOffset : containerRecipeItemsRenderer.NormalPositionOffset;
                containerRecipeItemsRenderer.RecipeRendererTransform.position = position;
                position += offset;
            }
        }
        
        private void GenerateRenderers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var recipeItemRenderer = Object.Instantiate(Container.ReferenceItemRendererPrefab);
                Container.RecipeItemsRenderers.Add(recipeItemRenderer);
            }
        }

        private bool IsRenderersEnought(RecipeObject recipeObject)
        {
            return Container.RecipeItemsRenderers.Count == recipeObject.ReferenceRecipeItems.Count;
        }
    }
}