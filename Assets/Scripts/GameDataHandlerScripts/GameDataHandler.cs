using UnityEngine;

namespace GameDataHandlerScripts
{
    public static class GameDataHandler
    {
        public static void SaveProgressToPrefs(GameData gameData)
        {
            PlayerPrefs.SetInt("CurrentDifficulty", gameData.CurrentDifficulty);
        }

        public static GameData LoadProgressToPrefs()
        {
            GameData gameData = new GameData();
            gameData.CurrentDifficulty = PlayerPrefs.GetInt("CurrentDifficulty");
            return gameData;
        }
    }
}