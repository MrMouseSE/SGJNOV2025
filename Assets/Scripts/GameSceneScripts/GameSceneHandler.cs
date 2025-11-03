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
        
        public override void InitSceneHandler(GameContext gameContext)
        {
            gameContext.PlayerContainer = PlayerContainer;
            gameContext.InitializeSystemByType(typeof(PlayerSystem));
        }

        public override void SetSceneActivity(bool isActive)
        {
            base.SetSceneActivity(isActive);
        }
    }
}
