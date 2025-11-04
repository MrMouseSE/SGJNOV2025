using MainMenuScripts.MainMenuButtons;

namespace MainMenuScripts
{
    public class MainMenuSceneHandler : AbstractSceneHandler
    {
        public MainMenuContainer MenuContainer;
        
        private MainMenuButtonsController _mainMenuButtonsController;
        private GameContext _gameContext;

        public override void InitSceneHandler(GameContext gameContext)
        {
            _gameContext = gameContext;
            _mainMenuButtonsController = new MainMenuButtonsController(gameContext, MenuContainer);
            
            InitSceneGameSystems(gameContext);
        }

        public override void SetSceneActivity(bool isActive)
        {
            _gameContext.IsGamePaused = isActive;
            base.SetSceneActivity(isActive);
        }

        private void InitSceneGameSystems(GameContext gameContext)
        {
        }
    }
}
