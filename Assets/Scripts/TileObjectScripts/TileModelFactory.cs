using TileObjectScripts.TileContainers;

namespace TileObjectScripts
{
    public static class TileModelFactory
    {
        public static ITileModel Create(AbstractTileContainer container)
        {
            ITileModel tileContainer;

            switch (container.TileType)
            {
                case TilesTypes.Angle:
                    tileContainer = new DefaultTileModel(container);
                    break;
                case TilesTypes.Button:
                    tileContainer = new DefaultTileModel(container);
                    break;
                case TilesTypes.Cannon:
                    tileContainer = new DefaultTileModel(container);
                    break;
                case TilesTypes.Destroyeble:
                    tileContainer = new DefaultTileModel(container);
                    break;
                case TilesTypes.Door:
                    tileContainer = new DefaultTileModel(container);
                    break;
                case TilesTypes.Doubler:
                    tileContainer = new DefaultTileModel(container);
                    break;
                case TilesTypes.ClearSight:
                    tileContainer = new ClearSightTileModel(container as ClearSightTileContainer);
                    break;
                default:
                    tileContainer = new DefaultTileModel(container);
                    break;
            }

            return tileContainer;
        }
    }
}