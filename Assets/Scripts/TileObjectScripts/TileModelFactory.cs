using TileObjectScripts.TileContainers;

namespace TileObjectScripts
{
    public static class TileModelFactory
    {
        public static TileNormalModel Create(AbstractTileContainer container)
        {
            return container.TileType switch
            {
                TilesTypes.Normal => new TileNormalModel(container)
            };
        }
    }
}