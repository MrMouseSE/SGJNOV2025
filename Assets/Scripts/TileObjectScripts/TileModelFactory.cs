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
                    tileContainer = new TileDefaultModel(container);
                    break;
                case TilesTypes.Button:
                    tileContainer = new TileDefaultModel(container);
                    break;
                case TilesTypes.Cannon:
                    tileContainer = new TileDefaultModel(container);
                    break;
                case TilesTypes.Destroyeble:
                    tileContainer = new TileDefaultModel(container);
                    break;
                case TilesTypes.Door:
                    tileContainer = new TileDefaultModel(container);
                    break;
                case TilesTypes.Doubler:
                    tileContainer = new TileDefaultModel(container);
                    break;
                default:
                    tileContainer = new TileDefaultModel(container);
                    break;
            }

            return tileContainer;
        }
    }
}