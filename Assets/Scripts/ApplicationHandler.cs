using UnityEngine;

public static class ApplicationHandler
{
    public static void QuitApplication(GameContext context)
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
