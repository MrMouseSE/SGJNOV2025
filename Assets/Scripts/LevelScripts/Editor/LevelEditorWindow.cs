using System;
using System.Collections.Generic;
using System.Linq;
using GameSceneScripts.TileObjectScripts;
using GameSceneScripts.TileObjectScripts.TileContainers;
using UnityEditor;
using UnityEngine;

namespace LevelScripts.Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        private static LevelDescription _levelDescription;
        private static AbstractTileContainer abstractTileContainer;
        private int _currentLevelIndex;
        private List<string> _allLevels = new List<string>();

        private LevelHandler _currentLevelHander;

        private string _newLevelName;

        private AbstractTileContainer currentSelectedAbstractTileContainer;
        
        [MenuItem("LevelEditor/Level Editor Window")]
        public static void ShowWindow()
        {
            LevelEditorWindow window = GetWindow<LevelEditorWindow>();
            window.Show();
            _levelDescription = AssetDatabase.LoadAssetAtPath<LevelDescription>("Assets/Content/GameContent/Descriptions/LevelDescriptions.asset");
            GameObject container = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Content/GameContent/Prefabs/TileContainer.prefab");
            abstractTileContainer = container.GetComponent<AbstractTileContainer>();
        }

        public void OnGUI()
        {
            var isNewLevelCreatedOrDestroyed = false;
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("AddNewLevel"))
            {
                CreateNewLevelDescription();
                isNewLevelCreatedOrDestroyed = true;
            }
            
            if (GUILayout.Button("RemoveLevel"))
            {
                RemoveCurrentLevel();
                isNewLevelCreatedOrDestroyed = true;
            }
            EditorGUILayout.EndHorizontal();
            if (isNewLevelCreatedOrDestroyed)
            {
                return;
            }

            if (_levelDescription.LevelData == null || _levelDescription.LevelData.Count == 0)
            {
                return;
            }
            
            _allLevels.Clear();
            foreach (var levelData in _levelDescription.LevelData.Where(levelData => !_allLevels.Contains(levelData.LevelName)))
            {
                _allLevels.Add(levelData.LevelName);
            }
            
            var previousLevelIndex = _currentLevelIndex;
            EditorGUI.BeginChangeCheck();
            _currentLevelIndex = EditorGUILayout.Popup(_currentLevelIndex, _allLevels.ToArray());
            if (EditorGUI.EndChangeCheck() || _currentLevelHander == null)
            {
                SaveAsset(previousLevelIndex);
                CreateNewLevelLevel();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            _newLevelName = EditorGUILayout.TextField(_levelDescription.LevelData[_currentLevelIndex].LevelName, _newLevelName);
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Fix name"))
            {
                var levelData = AssetDatabase.LoadAssetAtPath<LevelData>(
                    $"Assets/Content/GameContent/Descriptions/{_levelDescription.LevelData[_currentLevelIndex].name}.asset");
                AssetDatabase.RenameAsset(
                    $"Assets/Content/GameContent/Descriptions/{_levelDescription.LevelData[_currentLevelIndex].name}.asset", _newLevelName);
                levelData = AssetDatabase.LoadAssetAtPath<LevelData>(
                    $"Assets/Content/GameContent/Descriptions/{_newLevelName}.asset");
                levelData.name = _newLevelName;
                levelData.LevelName = _newLevelName;
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            if (GUILayout.Button("Save Level"))
            {
                SaveAsset(_currentLevelIndex);
            }
                
            DrawLevelOperating(_levelDescription.LevelData[_currentLevelIndex]);
        }

        private void CreateNewLevelLevel()
        {
            _currentLevelHander = new LevelHandler();
            _currentLevelHander.LevelTiles = new List<TileObjectHandler>();
            _levelDescription.LevelData[_currentLevelIndex].LevelTilesHandlers ??= new List<TileObjectHandler>();
            _levelDescription.LevelData[_currentLevelIndex].LevelTilesHandlers.RemoveAll(x=>x == null);
            
            foreach (var tileDescriptionHandler in _levelDescription.LevelData[_currentLevelIndex].LevelTilesHandlers)
            {
                AbstractTileContainer abstractTile = Instantiate(abstractTileContainer, tileDescriptionHandler.TilePosition, Quaternion.identity);
                var tileEditableHandler = new TileObjectHandler();
                tileEditableHandler.TilePosition = tileDescriptionHandler.TilePosition;
                tileEditableHandler.AbstractTilePrefab = abstractTile;
                _currentLevelHander.LevelTiles.Add(tileEditableHandler);
                currentSelectedAbstractTileContainer = abstractTile;
            }
        }

        private void DestroyCurrentLevel()
        {
            if (_currentLevelHander == null) return;
            foreach (var tile in _currentLevelHander.LevelTiles.Where(tile => tile.AbstractTilePrefab != null))
            {
                DestroyImmediate(tile.AbstractTilePrefab.TileGameObject);
                tile.AbstractTilePrefab = null;
            }

            _currentLevelHander = null;
        }

        private void OnDestroy()
        {
            SaveAsset(_currentLevelIndex);
        }

        private void SaveAsset(int levelIndex)
        {
            if (_currentLevelHander != null)
            {
                _levelDescription.LevelData[levelIndex].SaveLevelHandlerToLevelData(_currentLevelHander);
            }
            DestroyCurrentLevel();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void RemoveCurrentLevel()
        {
            var levelData = _levelDescription.LevelData[_currentLevelIndex];
            _levelDescription.LevelData.RemoveAt(_currentLevelIndex);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(levelData));
        }

        private void CreateNewLevelDescription()
        {
            LevelData newLevelData = CreateInstance<LevelData>();
            newLevelData.LevelName = "new Level" + (_levelDescription.LevelData.Count + 1).ToString();
            AssetDatabase.CreateAsset(newLevelData, $"Assets/Content/GameContent/Descriptions/{newLevelData.LevelName}.asset");
            LevelData loadedData = AssetDatabase.LoadAssetAtPath<LevelData>(
                $"Assets/Content/GameContent/Descriptions/{newLevelData.LevelName}.asset");
            _levelDescription.LevelData ??= new List<LevelData>();
            _levelDescription.LevelData.Add(loadedData);
        }

        private void Update()
        {
            AbstractTileContainer testContainer = null;
            if (Selection.activeGameObject == null) return;
            
            if (!Selection.activeGameObject.TryGetComponent(out testContainer))
            {
                var c = Selection.activeGameObject.GetComponentInParent<AbstractTileContainer>();
                if (c != null)
                {
                    Selection.activeGameObject = c.gameObject;
                }
            }
        }

        private void DrawLevelOperating(LevelData levelData)
        {
            _currentLevelHander.LevelName = levelData.LevelName;
            _currentLevelHander.LevelDifficulty = EditorGUILayout.IntField("Level difficulty", _currentLevelHander.LevelDifficulty);

            if (GUILayout.Button("Add tile"))
            {
                TileObjectHandler newTileObjectHandler = new TileObjectHandler();
                var tilePrefab = Instantiate(abstractTileContainer, Vector3.back, Quaternion.identity);
                tilePrefab.TileObjectHandler = newTileObjectHandler;
                newTileObjectHandler.AbstractTilePrefab = tilePrefab;
                Selection.activeObject = tilePrefab;
                currentSelectedAbstractTileContainer = tilePrefab;
                if (_currentLevelHander.LevelTiles == null) _currentLevelHander.LevelTiles = new List<TileObjectHandler>();
                _currentLevelHander.LevelTiles.Add(newTileObjectHandler);
            }

            EditorGUILayout.Space();
            TileObjectHandler selectedHandler = null;
            
            if (currentSelectedAbstractTileContainer != null)
            {
                foreach (var levelTile in _currentLevelHander.LevelTiles.Where(levelTile => levelTile.AbstractTilePrefab == currentSelectedAbstractTileContainer))
                {
                    selectedHandler = levelTile;
                }
            }
            
            if (Selection.activeGameObject != null && currentSelectedAbstractTileContainer != null && Selection.activeGameObject != currentSelectedAbstractTileContainer.TileGameObject)
            {
                foreach (var levelTile in _currentLevelHander.LevelTiles)
                {
                    if (levelTile.AbstractTilePrefab.TileGameObject == Selection.activeGameObject)
                    {
                        selectedHandler = levelTile;
                        currentSelectedAbstractTileContainer = Selection.activeGameObject.GetComponent<AbstractTileContainer>();
                    }
                }
            }

            if (selectedHandler == null) return;
            Vector3 position = currentSelectedAbstractTileContainer.TileTransform.position;
            position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
            foreach (var levelTile in _currentLevelHander.LevelTiles)
            {
                if (levelTile.AbstractTilePrefab == currentSelectedAbstractTileContainer) continue;
                if ((levelTile.TilePosition - position).magnitude < 0.1f)
                {
                    position = Vector3.back;
                }
            }
            selectedHandler.AbstractTilePrefab.transform.position = position;
            selectedHandler.TilePosition = position;
            selectedHandler.TileType = (TilesTypes)EditorGUILayout.Popup((int)selectedHandler.TileType, 
                Enum.GetNames(typeof(TilesTypes)));
        }
    }
}