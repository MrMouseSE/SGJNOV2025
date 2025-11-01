using System;
using System.Collections.Generic;
using DisolveEffectScripts;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public SceneHandler SceneHandler;
    public DisolveEffectContainer DisolveContainer;
    
    private readonly List<IGameSystem> GameSystems = new();
    private bool _isGamePaused = true;
    
    public void Start()
    {
        InitGame();
        GameSystems.Add(new DisolveEffectSystem(DisolveContainer));
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