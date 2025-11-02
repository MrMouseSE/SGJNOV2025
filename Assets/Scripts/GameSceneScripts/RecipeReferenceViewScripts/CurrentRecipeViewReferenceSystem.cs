namespace GameSceneScripts.RecipeReferenceViewScripts
{
    public class CurrentRecipeViewReferenceSystem : IGameSystem
    {
        public CurrentRecipeViewReferenceContainer Container;
        public CurrentRecipeViewReferenceModel Model;

        public CurrentRecipeViewReferenceSystem(CurrentRecipeViewReferenceContainer container)
        {
            Container = container;
            Model = new CurrentRecipeViewReferenceModel(Container);
        }

        public void InitGameSystem()
        {
            
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}