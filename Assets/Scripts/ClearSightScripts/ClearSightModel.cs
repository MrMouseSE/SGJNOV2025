using LevelScripts;
using UnityEngine;

namespace ClearSightScripts
{
    public class ClearSightModel
    {
        private LevelDescription _levelDescription;
        private int _currentDifficulty;
        private int _currentClearSightCount;

        private float _currentAnimationTime;
        private float _currentAnimationStartDelay;
        private int _currentTileToStartAnimation;
        private bool _isAnimating;
        
        public ClearSightModel(LevelDescription levelDescription, int currentDifficulty)
        {
            _levelDescription = levelDescription;
            _currentDifficulty = currentDifficulty;
            _currentClearSightCount =
                _levelDescription.LevelData.Find(x=>x.LevelDifficulty == _currentDifficulty).ClearSightCount;
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gameContext.ClearSightLootedCount++;
                if (gameContext.ClearSightLootedCount > _currentClearSightCount)
                    gameContext.ClearSightLootedCount = _currentClearSightCount;
            }
            if (gameContext.ClearSightLootedCount != _currentClearSightCount) return;
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
            if (_currentTileToStartAnimation < gameContext.TileObjectHandlers.Count)
            {
                ProcessTileFinalAnimation(gameContext);
                _currentAnimationStartDelay += deltaTime;
                if (_currentAnimationStartDelay>_levelDescription.FinalAnimationNextTileDelay)
                {
                    _currentTileToStartAnimation++;
                    _currentAnimationStartDelay = 0f;
                    if (_currentTileToStartAnimation >= gameContext.TileObjectHandlers.Count)
                        _currentAnimationTime = -_levelDescription.ToDartAnimationTime;
                }
            }
            else if(_currentAnimationTime < 0f)
            {
                PlayFinalAnimation(gameContext, -_currentAnimationTime/_levelDescription.ToDartAnimationTime);
            }
            else
            {
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
                handler.TilePrefab.ProcessGlowAnimation(
                    Mathf.Clamp01(_currentAnimationTime - index * _levelDescription.FinalAnimationNextTileDelay));
            }
        }

        private void PlayFinalAnimation(GameContext gameContext, float animationTime)
        {
            foreach (var handler in gameContext.TileObjectHandlers)
            {
                handler.TilePrefab.ProcessGlowAnimation(animationTime, true);
            }
        }
    }
}