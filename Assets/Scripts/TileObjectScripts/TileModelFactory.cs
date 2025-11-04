using TileObjectScripts.TileContainers;
using TileObjectScripts.TileModels;

namespace TileObjectScripts
{
    public static class TileModelFactory
    {
        public static ITileModel Create(TileObjectHandler handler, GameContext gameContext)
        {
            ITileModel tileModel;

            switch (handler.TilePrefab.TileType)
            {
                case TilesTypes.Angle:
                    tileModel = new DefaultTileModel(handler.TilePrefab);
                    break;
                case TilesTypes.Button:
                    tileModel = new ButtonTileModel(handler.TilePrefab);
                    break;
                case TilesTypes.Cannon:
                    tileModel = new CannonTileModel(handler.TilePrefab);
                    break;
                case TilesTypes.Destroyeble:
                    tileModel = new DestroeybleTileModel(handler.TilePrefab,gameContext);
                    break;
                case TilesTypes.Door:
                    tileModel = new DoorTileModel(handler.TilePrefab,gameContext);
                    break;
                case TilesTypes.Doubler:
                    tileModel = new DoublerTileModel(handler.TilePrefab, gameContext);
                    break;
                case TilesTypes.Cross:
                    tileModel = new CrossTileModel((CrossTileContainer)handler.TilePrefab, gameContext);
                    break;
                case TilesTypes.MoveTo:
                    tileModel = new MoveToTileModel(handler.TilePrefab);
                    break;
                case TilesTypes.ClearSight:
                    tileModel = new ClearSightTileModel((ClearSightTileContainer)handler.TilePrefab);
                    break;
                default:
                    tileModel = new DefaultTileModel(handler.TilePrefab);
                    break;
            }
            tileModel.SetMoveByPlayerAvailability(handler.IsAvailableToMoveByPlayer);
            return tileModel;
        }
    }
}