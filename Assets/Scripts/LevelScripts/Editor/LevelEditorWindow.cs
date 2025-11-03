using System;
using System.Collections.Generic;
using System.Linq;
using GameSceneScripts.TilesGeneratorScripts;
using TileObjectScripts;
using TileObjectScripts.TileContainers;
using UnityEditor;
using UnityEngine;

namespace LevelScripts.Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        private static LevelDescription _levelDescription;
        private static TilesDescription _tilesDescription;
        private int _currentLevelIndex;
        private List<string> _allLevels = new List<string>();

        private LevelHandler _currentLevelHander;

        private string _newLevelName;

        private AbstractTileContainer _currentSelectedAbstractTileContainer;
        private string _levelDescriptionsLocation = "Assets/Content/GameContent/Descriptions/LevelsDescriptions/";
        
        [MenuItem("LevelEditor/Level Editor Window")]
        public static void ShowWindow()
        {
            LevelEditorWindow window = GetWindow<LevelEditorWindow>();
            window.Show();
            _levelDescription = AssetDatabase.LoadAssetAtPath<LevelDescription>("Assets/Content/GameContent/Descriptions/LevelDescription.asset");
            _tilesDescription = AssetDatabase.LoadAssetAtPath<TilesDescription>("Assets/Content/GameContent/Descriptions/TilesDescription.asset");
            
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
                    $"{_levelDescriptionsLocation}{_levelDescription.LevelData[_currentLevelIndex].name}.asset");
                AssetDatabase.RenameAsset(
                    $"{_levelDescriptionsLocation}{_levelDescription.LevelData[_currentLevelIndex].name}.asset", _newLevelName);
                levelData = AssetDatabase.LoadAssetAtPath<LevelData>(
                    $"{_levelDescriptionsLocation}{_newLevelName}.asset");
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
                return;
            }
                
            DrawLevelOperating(_levelDescription.LevelData[_currentLevelIndex]);
        }

        private void CreateNewLevelLevel()
        {
            _currentLevelHander = new LevelHandler();
            _currentLevelHander.LevelTilesObjectHandler = new List<TileObjectHandler>();
            _levelDescription.LevelData[_currentLevelIndex].LevelTilesHandlers ??= new List<TileObjectHandler>();
            _levelDescription.LevelData[_currentLevelIndex].LevelTilesHandlers.RemoveAll(x=>x == null);
            
            _currentLevelHander.LevelDifficulty = _levelDescription.LevelData[_currentLevelIndex].LevelDifficulty;
            _currentLevelHander.LevelTilesObjectHandler = TilesGeneratorStaticFactory.GenerateTilesToWorld(_levelDescription.LevelData[_currentLevelIndex].LevelTilesHandlers,
                _tilesDescription);
            foreach (var objectHandler in _currentLevelHander.LevelTilesObjectHandler)
            {
                Color additionalColor = objectHandler.IsAvailableToMoveByPlayer ? _levelDescription.AvailableToMoveColor : _levelDescription.NotAvailableToMoveColor;
                ChangeMovableObjectColor(objectHandler.TilePrefab, additionalColor);
            }
            _currentSelectedAbstractTileContainer = _currentLevelHander.LevelTilesObjectHandler.Count == 0 ? null : _currentLevelHander.LevelTilesObjectHandler[^1].TilePrefab;
        }

        private void DestroyCurrentLevel()
        {
            if (_currentLevelHander == null) return;
            foreach (var tile in _currentLevelHander.LevelTilesObjectHandler.Where(tile => tile.TilePrefab != null))
            {
                DestroyImmediate(tile.TilePrefab.TileGameObject);
                tile.TilePrefab = null;
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
                var levelData =
                    AssetDatabase.LoadAssetAtPath<LevelData>(
                        AssetDatabase.GetAssetPath(_levelDescription.LevelData[levelIndex]));
                levelData.SaveLevelHandlerToLevelData(_currentLevelHander);
                EditorUtility.SetDirty(levelData);
                EditorUtility.SetDirty(_levelDescription);
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
            AssetDatabase.CreateAsset(newLevelData, $"{_levelDescriptionsLocation}{newLevelData.LevelName}.asset");
            LevelData loadedData = AssetDatabase.LoadAssetAtPath<LevelData>(
                $"{_levelDescriptionsLocation}{newLevelData.LevelName}.asset");
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
            _currentLevelHander.WallHitCount = EditorGUILayout.IntField("Level wall hits", _currentLevelHander.WallHitCount);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add tile"))
            {
                TileObjectHandler newTileObjectHandler = new TileObjectHandler();
                var tilePrefab = Instantiate(_tilesDescription.TileContainers.Find(x=> x.TileType == TilesTypes.Default), Vector3.back * 5, Quaternion.identity);
                tilePrefab.TileObjectHandler = newTileObjectHandler;
                newTileObjectHandler.TilePrefab = tilePrefab;
                Selection.activeObject = tilePrefab;
                _currentSelectedAbstractTileContainer = tilePrefab;
                ChangeMovableObjectColor(tilePrefab, _levelDescription.NotAvailableToMoveColor);
                if (_currentLevelHander.LevelTilesObjectHandler == null) _currentLevelHander.LevelTilesObjectHandler = new List<TileObjectHandler>();
                _currentLevelHander.LevelTilesObjectHandler.Add(newTileObjectHandler);
            }

            if (GUILayout.Button("Remove tile") && Selection.activeObject != null)
            {
                AbstractTileContainer container = Selection.activeGameObject.GetComponent<AbstractTileContainer>();
                var handler = _currentLevelHander.LevelTilesObjectHandler.Find(x=>x.TilePrefab == container);
                _currentLevelHander.LevelTilesObjectHandler.Remove(handler);
                DestroyImmediate(container.TileGameObject);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            TileObjectHandler selectedHandler = null;
            
            if (_currentSelectedAbstractTileContainer != null)
            {
                foreach (var levelTile in _currentLevelHander.LevelTilesObjectHandler.Where(levelTile => levelTile.TilePrefab == _currentSelectedAbstractTileContainer))
                {
                    selectedHandler = levelTile;
                }
            }
            
            if (Selection.activeGameObject != null && _currentSelectedAbstractTileContainer != null && Selection.activeGameObject != _currentSelectedAbstractTileContainer.TileGameObject)
            {
                foreach (var levelTile in _currentLevelHander.LevelTilesObjectHandler)
                {
                    if (levelTile.TilePrefab.TileGameObject == Selection.activeGameObject)
                    {
                        selectedHandler = levelTile;
                        _currentSelectedAbstractTileContainer = Selection.activeGameObject.GetComponent<AbstractTileContainer>();
                    }
                }
            }

            if (selectedHandler == null) return;
            Vector3 position = _currentSelectedAbstractTileContainer.TileTransform.position;
            position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
            foreach (var levelTile in _currentLevelHander.LevelTilesObjectHandler)
            {
                if (levelTile.TilePrefab == _currentSelectedAbstractTileContainer) continue;
                if ((levelTile.TilePosition - position).magnitude < 0.1f)
                {
                    position = Vector3.back;
                }
            }
            selectedHandler.TilePrefab.transform.position = position;
            selectedHandler.TilePosition = position;
            
            selectedHandler.IsTileGlowAtStart = EditorGUILayout.Toggle("Tile glow at start", selectedHandler.IsTileGlowAtStart);
            selectedHandler.IsAvailableToMoveByPlayer = EditorGUILayout.Toggle("Movable", selectedHandler.IsAvailableToMoveByPlayer);
            ChangeMovableObjectColor(selectedHandler.TilePrefab, 
                selectedHandler.IsAvailableToMoveByPlayer ? _levelDescription.AvailableToMoveColor : _levelDescription.NotAvailableToMoveColor);
            
            EditorGUI.BeginChangeCheck();
            selectedHandler.TileType = (TilesTypes)EditorGUILayout.Popup((int)selectedHandler.TileType, 
                Enum.GetNames(typeof(TilesTypes)));
            if (EditorGUI.EndChangeCheck())
            {
                ChangeTilePrefab(selectedHandler);
            }
        }

        private void ChangeMovableObjectColor(AbstractTileContainer prefab, Color color)
        {
            if (prefab.AdditionalMeshRenderer == null) return;
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            prefab.AdditionalMeshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(prefab.EmissionColor, color);
            prefab.AdditionalMeshRenderer.SetPropertyBlock(propertyBlock);
        }

        private void ChangeTilePrefab(TileObjectHandler selectedHandler)
        {
            Vector3 position = selectedHandler.TilePosition;
            DestroyImmediate(selectedHandler.TilePrefab.TileGameObject);
            var tilePrefab = Instantiate(_tilesDescription.TileContainers.Find(x=> x.TileType == selectedHandler.TileType), Vector3.back, Quaternion.identity);
            tilePrefab.TileTransform.position = position;
            Selection.activeObject = tilePrefab;
            _currentSelectedAbstractTileContainer = tilePrefab;
            selectedHandler.TilePrefab = tilePrefab;
        }
    }
}