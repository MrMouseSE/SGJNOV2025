using System.Collections.Generic;

namespace TileObjectScripts
{
    public class TileSystemsModel
    {
        private List<TileSystem> _tileSystems = new List<TileSystem>();

        public void InitializeModel(List<TileObjectHandler> tileObjectHandlers)
        {
            _tileSystems.Clear();
            foreach (var handler in tileObjectHandlers)
            {
                _tileSystems.Add(new TileSystem(handler));
            }
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            foreach (var tileSystem in _tileSystems)
            {
                tileSystem.UpdateGameSystem(deltaTime, gameContext);
            }
        }
    }
}