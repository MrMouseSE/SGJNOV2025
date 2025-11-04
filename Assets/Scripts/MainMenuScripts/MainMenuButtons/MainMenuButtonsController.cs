namespace MainMenuScripts.MainMenuButtons
{
    public class MainMenuButtonsController
    {
        private MainMenuContainer _menuContainer;
        private MainMenuButtonsModel _menuModel; 
        
        public MainMenuButtonsController(GameContext context, MainMenuContainer container)
        {
            _menuContainer = container;
            _menuModel = new MainMenuButtonsModel(_menuContainer, context);
        }
    }
}
