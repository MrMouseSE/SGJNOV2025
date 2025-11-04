using GameSceneScripts.TilesGeneratorScripts;
using PlayerScripts;
using TileObjectScripts;
using UnityEngine;

namespace GameSceneScripts
{
    public class GameSceneHandler : AbstractSceneHandler
    {
        public StaticTilesHandler StaticTilesHandler;
        public Transform TilesHolder;
        public PlayerContainer PlayerContainer;
        private GameContext _gameContext;
        private bool _isPlayerInitialized;
        
        public override void InitSceneHandler(GameContext gameContext)
        {
            gameContext.PlayerContainer = PlayerContainer;
            _gameContext = gameContext;
        }

        public override void SetSceneActivity(bool isActive)
        {
            base.SetSceneActivity(isActive);

            if (isActive && _isPlayerInitialized == false)
            {
                _gameContext.InitializeSystemByType(typeof(PlayerSystem));
                _gameContext.InitializeSystemByType(typeof(TilesGeneratorSystem));

                _isPlayerInitialized = true;
            }
        }
    }
}
