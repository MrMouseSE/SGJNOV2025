using RecipeScripts;

namespace GameSceneScripts
{
    public class GameSceneHandler : AbstractSceneHandler
    {
        private RecipeHandler _recipeHandler;
        private RecipeGenerator _recipeGenerator;

        public override void InitSceneHandler(GameContext gameContext)
        {
            _recipeGenerator = new RecipeGenerator(gameContext.RecipesDescription);
            RecipeObject currentRecipe = _recipeGenerator.GenerateAndReturnRecipeObject(gameContext.CurrentDifficulty);
            _recipeHandler = new RecipeHandler();
            _recipeHandler.CurrentRecipe = currentRecipe;
        }
    }
}