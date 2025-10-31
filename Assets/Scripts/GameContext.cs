using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public SceneHandler SceneHandler;
    
    private readonly List<IGameController> GameSystems = new();
    private bool _isGamePaused = true;
    
    public void Start()
    {
        InitGame();
    }

    public void AddGameSystem(IGameController gameController)
    {
        GameSystems.Add(gameController);
    }

    public void Update()
    {
        foreach (IGameController gameSystem in GameSystems)
        {
            gameSystem.UpdateGameSystem(Time.deltaTime, this);
        }
    }

    public void PauseGameProcess(bool paused)
    {
        _isGamePaused = paused;
    }
    
    private void InitGame()
    {
        SceneHandler = new SceneHandler(this);
    }
}