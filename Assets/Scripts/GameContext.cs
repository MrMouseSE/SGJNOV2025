using System;
using System.Collections.Generic;
using BallScripts;
using DisolveEffectScripts;
using GameSceneScripts.TilesGeneratorScripts;
using LevelScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public SceneHandler SceneHandler;
    public DisolveEffectContainer DisolveContainer;
    public TilesDescription TilesDescription;
    public LevelDescription LevelDescription;
    
    private readonly List<IGameSystem> GameSystems = new();
    private bool _isGamePaused;
    
    [HideInInspector]
    public int CurrentDifficulty;
    [HideInInspector]
    public bool RegenerateLevel;
    [HideInInspector]
    public List<TileObjectHandler> TileObjectHandlers;
    
    public void Start()
    {
        InitGame();
        RegenerateLevel = true;

        GameSystems.Add(new DisolveEffectSystem(DisolveContainer));
        GameSystems.Add(new BallSystem(TilesDescription.BallContainer));
        GameSystems.Add(new TilesGeneratorSystem(LevelDescription, TilesDescription));

        foreach (var handler in TileObjectHandlers)
        {
            GameSystems.Add(new TileSystem(handler.TilePrefab));
        }
    }

    public void AddGameSystem(IGameSystem gameSystem)
    {
        GameSystems.Add(gameSystem);
    }

    public void Update()
    {
        foreach (IGameSystem gameSystem in GameSystems)
        {
            gameSystem.UpdateGameSystem(Time.deltaTime, this);
        }
    }

    public void PauseGameProcess(bool paused)
    {
        _isGamePaused = paused;
    }

    public IGameSystem GetGameSystemByType(Type type)
    {
        return GameSystems.Find(x => x.GetType() == type);
    }
    
    private void InitGame()
    {
        SceneHandler = new SceneHandler(this);
    }
}