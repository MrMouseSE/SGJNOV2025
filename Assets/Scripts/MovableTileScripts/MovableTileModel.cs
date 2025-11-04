using System.Collections.Generic;
using GameSceneScripts;
using MouseCorsourScripts;
using MouseCursorScripts;
using TileObjectScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovableTileScripts
{
    public class MovableTileModel
    {
        private List<TileObjectHandler> _tileObjectHandlers;
        private List<TileObjectHandler> _movableTileObjectHandlers;
        private MouseCursorSystem _mouseCursorSystem;
        private GameContext _gameContext;
        
        private float _impactRadius;
        private bool _isObjectCatched = false;
        private TileObjectHandler _catchedTileObjectHandler;
        private bool _isTileNearest;
        private TileObjectHandler _tileObjectHandlerInRadius;

        public MovableTileModel(GameContext gameContext)
        {
            _mouseCursorSystem = (MouseCursorSystem)gameContext.GetGameSystemByType(typeof(MouseCursorSystem));
            _gameContext = gameContext;
            _impactRadius = _gameContext.LevelDescription.HandleImpactRadius;
        }

        public void InitModel()
        {
            _tileObjectHandlers = _gameContext.TileObjectHandlers;
            _mouseCursorSystem = (MouseCursorSystem)_gameContext.GetGameSystemByType(typeof(MouseCursorSystem));
            _movableTileObjectHandlers = _tileObjectHandlers.FindAll(x=>x.IsAvailableToMoveByPlayer == true);
        }

        private bool _isSupportShowed;

        public void UpdateModel()
        {
            if (!_isObjectCatched)
            {
                _isTileNearest = TryCatchTileObjectHandlerInRadius(out _tileObjectHandlerInRadius);
                _mouseCursorSystem.Model.UpdateCursorType(_isTileNearest?CursorTypes.Hand:CursorTypes.Normal);
                if (!_isTileNearest) return;
                if (!Mouse.current.leftButton.wasPressedThisFrame) return;
                _isObjectCatched = true;
                _isTileNearest = false;
                _catchedTileObjectHandler = _tileObjectHandlerInRadius;
                _tileObjectHandlerInRadius = null;
                _catchedTileObjectHandler.TilePrefab.TileModel.StartHandledAnimation(
                    _gameContext.LevelDescription.TilesHandledOffset, _gameContext.LevelDescription.HandleDirectAnimationCurve);
            }

            if (_isObjectCatched)
            {
                if (!_isSupportShowed)
                {
                    ((GameSceneHandler)_gameContext.SceneHandler.GetSceneHandlerByName(_gameContext.SceneHandler.GameSceneName)).MovableSupportMessage.StartHideAnimation();
                }
                _catchedTileObjectHandler.TilePrefab.TileModel.SetPrefabPosition(_mouseCursorSystem.Model.GetMousePositionOnGround());
                _isTileNearest = TryCatchTileObjectHandlerInRadius(out _tileObjectHandlerInRadius);
                _mouseCursorSystem.Model.UpdateCursorType(CursorTypes.Fist);
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    if (_isTileNearest)
                    {
                        SwapTileObjectHandlers(_catchedTileObjectHandler, _tileObjectHandlerInRadius);
                        
                        _isTileNearest = false;
                        _tileObjectHandlerInRadius = null;
                    }
                    else
                    {
                        _catchedTileObjectHandler.TilePrefab.TileModel.SetPrefabPosition(_catchedTileObjectHandler.TilePosition);
                        _catchedTileObjectHandler.TilePrefab.TileModel.StartHandledAnimation(
                            Vector3.zero, _gameContext.LevelDescription.HandleDirectAnimationCurve);
                    }
                    _isObjectCatched = false;
                    _catchedTileObjectHandler = null;
                }
            }
        }

        private void SwapTileObjectHandlers(TileObjectHandler source, TileObjectHandler destination)
        {
            Vector3 sourcePosition = source.TilePosition;
            Vector3 destinationPosition = destination.TilePosition;
            source.TilePosition = destinationPosition;
            source.TilePrefab.TileModel.SetPrefabPosition(destinationPosition);
            source.TilePrefab.TileModel.StartHandledAnimation(
                _gameContext.LevelDescription.TilesHandledOffset, _gameContext.LevelDescription.HandleArcAnimationCurve);
            destination.TilePosition = sourcePosition;
            destination.TilePrefab.TileModel.SetPrefabPosition(sourcePosition);
            destination.TilePrefab.TileModel.StartHandledAnimation(
                _gameContext.LevelDescription.TilesHandledOffset, _gameContext.LevelDescription.HandleArcAnimationCurve);
            _mouseCursorSystem.Model.UpdateCursorType(CursorTypes.Normal);
        }

        private bool TryCatchTileObjectHandlerInRadius(out TileObjectHandler tileObjectHandler)
        {
            tileObjectHandler = null;
            Vector3 rayHitPoint = _mouseCursorSystem.Model.GetMousePositionOnGround();
            foreach (var movableTileObjectHandler in _movableTileObjectHandlers)
            {
                if (!movableTileObjectHandler.TilePrefab.IsGlowing) continue;
                float distanceToObject = Vector3.Distance(movableTileObjectHandler.TilePosition, rayHitPoint);
                if (distanceToObject > _impactRadius) continue;
                if (tileObjectHandler == movableTileObjectHandler) return false;
                tileObjectHandler = movableTileObjectHandler;
                return true;
            }
            return false;
        }
    }
}