using System;
using System.Collections.Generic;
using BallScripts;
using ClearSightScripts;
using DisolveEffectScripts;
using GameSceneScripts.TilesGeneratorScripts;
using LevelScripts;
using PlayerScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public SceneHandler SceneHandler;
    public DisolveEffectContainer DisolveContainer;
    public TilesDescription TilesDescription;
    public LevelDescription LevelDescription;
    public BallContainer BallContainer;
    public Vector3 LeftEndPoint = new Vector3(-4, 0, 0);
    public BallFactory BallFactory;
    public InputSystemActions InputSystem;
    
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

    private int _currentButtonsPressed;
    public Action AllButtonsPressed;
    
    public void Start()
    {
        CurrentDifficulty = 1;
        InitGame();
        RegenerateLevel = true;
        InputSystem = new InputSystemActions();
        InputSystem.Enable();

        GameSystems.Add(new DisolveEffectSystem(DisolveContainer));
        GameSystems.Add(new TilesGeneratorSystem(LevelDescription, TilesDescription));
        GameSystems.Add(new ClearSightSystem(LevelDescription, CurrentDifficulty));
        GameSystems.Add(new TileSystemsSystem(this));
        
        BallFactory = new BallFactory(BallContainer);
    }

    public void AddPlayerSystem(PlayerContainer playerContainer)
    {
        GameSystems.Add(new PlayerSystem(playerContainer, this));
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

    public void ButtonPressed()
    {
        _currentButtonsPressed++;
        if (_currentButtonsPressed != GetCurrentLevelData().ButtonsCount) return;
        AllButtonsPressed?.Invoke();
    }

    public LevelData GetCurrentLevelData()
    {
        return LevelDescription.LevelData.Find(x => x.LevelDifficulty == CurrentDifficulty);
    }

    public void DestroyBall(BallModel ballContainer)
    {
        //TODO: destroyBallLogic
    }
    
    private void InitGame()
    {
        SceneHandler = new SceneHandler(this);
    }
}