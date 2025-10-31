namespace MainMenuScripts.MainMenuButtons
{
    public class MainMenuButtonsModel
    {
        public MainMenuButtonsController MenuController;
        public MainMenuContainer MenuContainer;

        private GameContext _context;
        
        public MainMenuButtonsModel(MainMenuContainer menuContainer, GameContext context)
        {
            _context = context;
            MenuContainer = menuContainer;
            MenuContainer.GameStartButton.ButtonActionPressed += OnStartGameButtonClicked;
            MenuContainer.SettingsButton.ButtonActionPressed += OnSettingsButtonClicked;
            MenuContainer.QuitButton.ButtonActionPressed += OnExitGameButtonClicked;
        }

        private void OnStartGameButtonClicked()
        {
            SceneHandler.ActivateSceneByName("GameScene");
        }

        private void OnSettingsButtonClicked()
        {
            SceneHandler.ActivateSceneByName("SettingsScene");
        }

        private void OnExitGameButtonClicked()
        {
            MenuContainer.GameStartButton.ButtonActionPressed -= OnStartGameButtonClicked;
            MenuContainer.SettingsButton.ButtonActionPressed -= OnSettingsButtonClicked;
            MenuContainer.QuitButton.ButtonActionPressed -= OnExitGameButtonClicked;
            ApplicationHandler.QuitApplication(_context);
        }
    }
}
