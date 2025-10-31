using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler
{
    private static Dictionary<string, AbstractSceneHandler> _sceneHandlers = new Dictionary<string, AbstractSceneHandler>();
    private GameContext _gameContext;
    
    public SceneHandler(GameContext context)
    {
        _gameContext = context;
        LoadAllScenes();
    }
    
    private async void LoadAllScenes()
    {
        try
        {
            await LoadScene("MenuScene");
            await LoadScene("SettingsScene");
            await LoadScene("GameScene");
        }
        catch (Exception e)
        {
            Debug.Log("Cant load scenes");
        }
    }
    
    private AsyncOperation LoadScene(string sceneName)
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, parameters);
        loadSceneAsync.completed += _ => AddHandlerToDictionary(sceneName);
        return loadSceneAsync;
    }

    private void AddHandlerToDictionary(string sceneName)
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        var rootObjects = scene.GetRootGameObjects()[0];
        var sceneHandler = rootObjects.GetComponent<AbstractSceneHandler>();
        sceneHandler.InitSceneHandler(_gameContext);
        sceneHandler.SetSceneActivity(sceneName == "MenuScene");
        _sceneHandlers.Add(scene.name,sceneHandler);
    }
    
    public static void ActivateSceneByName(string sceneName)
    {
        foreach (var handler in _sceneHandlers)
        {
            handler.Value.SetSceneActivity(false);
        }
        _sceneHandlers[sceneName].SetSceneActivity(true);
    }
}