using PlayerScripts;
using UnityEngine;

namespace GameSceneScripts
{
    public class GameSceneHandler : AbstractSceneHandler
    {
        public Transform TilesHolder;
        public PlayerContainer PlayerContainer;
        
        public override void InitSceneHandler(GameContext gameContext)
        {
            gameContext.AddPlayerSystem(PlayerContainer);
            gameContext.AddGameSystem(new PlayerSystem(PlayerContainer, gameContext));
        }

        public override void SetSceneActivity(bool isActive)
        {
            base.SetSceneActivity(isActive);
        }
    }
}
