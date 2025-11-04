using System;
using System.Collections.Generic;
using BallScripts;
using ClearSightScripts;
using DisolveEffectScripts;
using GameSceneScripts.TilesGeneratorScripts;
using LevelScripts;
using MouseCursorScripts;
using MovableTileScripts;
using PlayerScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameContext : MonoBehaviour, IDisposable
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
    public bool CanSpawnBall;
    [HideInInspector]
    public List<TileObjectHandler> StaticTileObjectHandlers;
    [HideInInspector]
    public List<TileObjectHandler> TileObjectHandlers;
    [HideInInspector]
    public int ClearSightLootedCount;
    [HideInInspector]
    public int CurrentLevelClearSightCount;
    [HideInInspector]
    public PlayerContainer PlayerContainer;
    
    private int _currentButtonsPressed;
    public Action AllButtonsPressed;
    

    public void Start()
    {
        CurrentDifficulty = 1;
        InitGame();
        RegenerateLevel = true;
        CanSpawnBall = true;
        BallFactory = new BallFactory(BallContainer, this);
        InputSystem = new InputSystemActions();
        InputSystem.Enable();
        InputSystem.Player.Exit.started += ExitGame;

        GameSystems.Add(new DisolveEffectSystem(DisolveContainer));
        GameSystems.Add(new TilesGeneratorSystem(LevelDescription, TilesDescription, this));
        GameSystems.Add(new ClearSightSystem(LevelDescription));
        GameSystems.Add(new TileSystemsSystem(this));
        GameSystems.Add(new PlayerSystem(this));
        GameSystems.Add(new BallsSystems());
        GameSystems.Add(new MouseCursorSystem(this));
        GameSystems.Add(new MovableTileSystem(this));

        SetCurrentClearSightCount();
    }

    private void ExitGame(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void SetCurrentClearSightCount()
    {
        CurrentLevelClearSightCount = LevelDescription.LevelData.Find(x => x.LevelDifficulty == CurrentDifficulty)
            .ClearSightCount;
    }

    public void InitializeSystemByType(Type systemType)
    {
        GetGameSystemByType(systemType).InitGameSystem();
    }

    public void Update()
    {
        foreach (IGameSystem gameSystem in GameSystems)
        {
            gameSystem.UpdateGameSystem(Time.deltaTime, this);
        }
    }

    public void Dispose()
    {
        InputSystem.Player.Exit.started -= ExitGame;
    }

    public IGameSystem GetGameSystemByType(Type type)
    {
        return GameSystems.Find(x => x.GetType() == type);
    }

    public void ResetGameValues()
    {
        IsGamePaused = false;
        CanSpawnBall = true;
        ClearSightLootedCount = 0;
        RegenerateLevel = true;
        CurrentDifficulty ++;
        SetCurrentClearSightCount();
    }

    public void ButtonPressed()
    {
        _currentButtonsPressed++;
        if (_currentButtonsPressed != GetCurrentLevelData().ButtonsCount) return;
        AllButtonsPressed?.Invoke();
        _currentButtonsPressed = 0;
    }

    public LevelData GetCurrentLevelData()
    {
        return LevelDescription.LevelData.Find(x => x.LevelDifficulty == CurrentDifficulty);
    }

    private void InitGame()
    {
        SceneHandler = new SceneHandler(this);
    }
}