using System;
using System.Collections.Generic;
using BallScripts;
using ClearSightScripts;
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
    
    [HideInInspector]
    public bool IsGamePaused;
    [HideInInspector]
    public int CurrentDifficulty;
    [HideInInspector]
    public bool RegenerateLevel;
    [HideInInspector]
    public List<TileObjectHandler> TileObjectHandlers;
    [HideInInspector]
    public int ClearSightLootedCount;
    
    public void Start()
    {
        InitGame();
        RegenerateLevel = true;

        GameSystems.Add(new DisolveEffectSystem(DisolveContainer));
        GameSystems.Add(new BallSystem(TilesDescription.BallContainer));
        GameSystems.Add(new TilesGeneratorSystem(LevelDescription, TilesDescription));
        GameSystems.Add(new ClearSightSystem(LevelDescription, CurrentDifficulty));
        GameSystems.Add(new TileSystemsSystem(this));
    }

    public void InitializeTilesSystems(List<TileObjectHandler> handlers)
    {
        TileObjectHandlers = handlers;
        ((TileSystemsSystem)GetGameSystemByType(typeof(TileSystemsSystem))).InitGameSystem();
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

    public IGameSystem GetGameSystemByType(Type type)
    {
        return GameSystems.Find(x => x.GetType() == type);
    }

    public void ResetGameValues()
    {
        IsGamePaused = false;
        ClearSightLootedCount = 0;
        RegenerateLevel = true;
        CurrentDifficulty ++;
    }

    public void DestroyBall(BallContainer ballContainer)
    {
        
    }
    
    private void InitGame()
    {
        SceneHandler = new SceneHandler(this);
    }
}