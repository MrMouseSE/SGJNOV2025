using UnityEngine;

public abstract class AbstractSceneHandler : MonoBehaviour
{
    public Camera SceneCamera;
    public GameObject[] RootObjects;
    
    public virtual void InitSceneHandler(GameContext gameContext)
    {
    }
    
    public virtual void SetSceneActivity(bool isActive)
    {
        SwitchActiveRootsObjects(isActive);
    }
    
    private void SwitchActiveRootsObjects(bool isActive)
    {
        foreach (var rootObject in RootObjects)
        {
            rootObject.SetActive(isActive);
        }
    }
}
