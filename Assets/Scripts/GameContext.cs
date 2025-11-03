using System;
using System.Collections.Generic;
using BallScripts;
using DisolveEffectScripts;
using LevelScripts;
using PlayerScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public SceneHandler SceneHandler;
    public DisolveEffectContainer DisolveContainer;
    public BallContainer BallContainer;
    public PlayerContainer PlayerContainer;
    public TilesDescription TilesDescription;
    public LevelDescription LevelDescription;
    public Transform LeftEndPoint;
    public BallFactory BallFactory;

    private int _currentDifficulty;
    private readonly List<IGameSystem> GameSystems = new();
    private bool _isGamePaused = true;
    private InputSystemActions _inputSystem;
    
    private List<TileObjectHandler> _tileObjectHandlers;
    
    public void Start()
    {
        InitGame();
        
        _inputSystem = new InputSystemActions();
        _inputSystem.Enable();

        GameSystems.Add(new DisolveEffectSystem(DisolveContainer));
        GameSystems.Add(new BallSystem(BallContainer));
        GameSystems.Add(new PlayerSystem(_inputSystem, PlayerContainer, this));
        
        BallFactory = new BallFactory(BallContainer);
        
        foreach (var handler in _tileObjectHandlers)
        {
            GameSystems.Add(new TileSystem(handler.AbstractTilePrefab));
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