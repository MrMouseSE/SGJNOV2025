using TileObjectScripts.TileContainers;
using TileObjectScripts.TileModels;

namespace TileObjectScripts
{
    public static class TileModelFactory
    {
        public static ITileModel Create(AbstractTileContainer container, GameContext gameContext)
        {
            ITileModel tileModel;

            switch (container.TileType)
            {
                case TilesTypes.Angle:
                    tileModel = new DefaultTileModel(container);
                    break;
                case TilesTypes.Button:
                    tileModel = new DefaultTileModel(container);
                    break;
                case TilesTypes.Cannon:
                    tileModel = new DefaultTileModel(container);
                    break;
                case TilesTypes.Destroyeble:
                    tileModel = new DefaultTileModel(container);
                    break;
                case TilesTypes.Door:
                    tileModel = new DefaultTileModel(container);
                    break;
                case TilesTypes.Doubler:
                    tileModel = new DefaultTileModel(container);
                    break;
                case TilesTypes.Cross:
                    tileModel = new CrossTileModel((CrossTileContainer)container, gameContext);
                    break;
                case TilesTypes.ClearSight:
                    tileModel = new ClearSightTileModel((ClearSightTileContainer)container);
                    break;
                default:
                    tileModel = new DefaultTileModel(container);
                    break;
            }

            return tileModel;
        }
    }
}