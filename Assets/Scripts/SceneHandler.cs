using System;
using System.Collections.Generic;
using DisolveEffectScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler
{
    private Dictionary<string, AbstractSceneHandler> _sceneHandlers = new Dictionary<string, AbstractSceneHandler>();
    private GameContext _gameContext;

    private string _previousSceneName;
    private string _nextSceneName;

    
    public readonly string GameSceneName = "GameScene";
    public readonly string MenuSceneName = "MenuScene";
    public readonly string SettingsSceneName = "SettingsScene";
    
    public SceneHandler(GameContext context)
    {
        _gameContext = context;
        LoadAllScenes();
    }

    public AbstractSceneHandler GetSceneHandlerByName(string sceneName)
    {
        if (!_sceneHandlers.ContainsKey(sceneName)) return null;
        
        return _sceneHandlers[sceneName];
    }
    
    private async void LoadAllScenes()
    {
        try
        {
            await LoadScene(MenuSceneName);
            await LoadScene(SettingsSceneName);
            await LoadScene(GameSceneName);
            _previousSceneName = MenuSceneName;
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
        _sceneHandlers.Add(sceneName,sceneHandler);
        sceneHandler.InitSceneHandler(_gameContext);
        sceneHandler.SetSceneActivity(sceneName == MenuSceneName);
    }
    
    public void ActivateSceneByName(string sceneName)
    {
        _nextSceneName = sceneName;
        var disolveSystem = _gameContext.GetGameSystemByType(typeof(DisolveEffectSystem)) as DisolveEffectSystem;
        disolveSystem.OnAnimationHalf += SwitchScene;
        disolveSystem.SetToCurrentCamera(_sceneHandlers[_previousSceneName].SceneCamera);
        disolveSystem.StartDisolveEffect();
    }

    private void SwitchScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextSceneName));
        var disolveSystem = _gameContext.GetGameSystemByType(typeof(DisolveEffectSystem)) as DisolveEffectSystem;
        disolveSystem.OnAnimationHalf -= SwitchScene;
        foreach (var handler in _sceneHandlers)
        {
            handler.Value.SetSceneActivity(false);
        }
        _sceneHandlers[_nextSceneName].SetSceneActivity(true);
        disolveSystem.SetToCurrentCamera(_sceneHandlers[_nextSceneName].SceneCamera);
        _previousSceneName = _nextSceneName;
    }
}