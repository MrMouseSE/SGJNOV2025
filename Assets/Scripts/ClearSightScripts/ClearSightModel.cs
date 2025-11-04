using BallScripts;
using LevelScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClearSightScripts
{
    public class ClearSightModel
    {
        private LevelDescription _levelDescription;

        private float _currentAnimationTime;
        private float _currentAnimationStartDelay;
        private int _currentTileToStartAnimation;
        private bool _isDarkAnimationWaiting;
        private bool _isDarkAnimationStart;
        private bool _isAnimating;
        
        public ClearSightModel(LevelDescription levelDescription)
        {
            _levelDescription = levelDescription;
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                gameContext.ClearSightLootedCount++;
                if (gameContext.ClearSightLootedCount > gameContext.CurrentLevelClearSightCount)
                    gameContext.ClearSightLootedCount = gameContext.CurrentLevelClearSightCount;
            }
            if (gameContext.ClearSightLootedCount != gameContext.CurrentLevelClearSightCount) return;
            var ballsSystem = (BallsSystems)gameContext.GetGameSystemByType(typeof(BallsSystems));
            ballsSystem.Model.DestroyAllBalls();
            _currentAnimationTime += deltaTime;
            gameContext.IsGamePaused = true;
            if (!_isAnimating) 
            {
                _isAnimating = true;
                foreach (var handler in gameContext.TileObjectHandlers)
                {
                    handler.TilePrefab.CollectCurrentColors();
                }
            }
            if (!_isDarkAnimationWaiting && !_isDarkAnimationStart)
            {
                ProcessTileFinalAnimation(gameContext);
                _currentAnimationStartDelay += deltaTime;
                if (_currentAnimationStartDelay>_levelDescription.FinalAnimationNextTileDelay)
                {
                    _currentTileToStartAnimation++;
                    _currentAnimationStartDelay = 0f;
                    if (_currentTileToStartAnimation >= gameContext.TileObjectHandlers.Count)
                    {
                        _currentAnimationTime = 0;
                        _isDarkAnimationWaiting = true;
                    }
                }
            }

            if (_isDarkAnimationWaiting && _currentAnimationTime < _levelDescription.BeforDartAnimationDelay)
            {
                _isDarkAnimationWaiting = false;
                _currentAnimationTime = 0f;
                _isDarkAnimationStart = true;
            }

            if (!_isDarkAnimationStart) return;
            if (_currentAnimationTime < _levelDescription.ToDartAnimationTime)
            {
                PlayFinalAnimation(gameContext, 1-_currentAnimationTime/_levelDescription.ToDartAnimationTime);
            }
            else
            {
                _isDarkAnimationStart = false;
                _currentTileToStartAnimation = 0;
                gameContext.ResetGameValues();
                _isAnimating = false;
                _currentAnimationTime = 0f;
            }

        }

        private void ProcessTileFinalAnimation(GameContext gameContext)
        {
            for (var index = 0; index < gameContext.TileObjectHandlers.Count; index++)
            {
                var handler = gameContext.TileObjectHandlers[index];
                handler.TilePrefab.ProcessGlowAnimation(handler.TilePrefab.FinalAnimationCurve,
                    Mathf.Clamp01(_currentAnimationTime - index * _levelDescription.FinalAnimationNextTileDelay));
            }
        }

        private void PlayFinalAnimation(GameContext gameContext, float animationTime)
        {
            foreach (var handler in gameContext.TileObjectHandlers)
            {
                handler.TilePrefab.ProcessGlowAnimation(handler.TilePrefab.FinalAnimationCurve, animationTime, true);
            }
        }
    }
}