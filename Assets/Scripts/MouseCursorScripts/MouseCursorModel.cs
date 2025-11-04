using MouseCorsourScripts;
using MovableTileScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MouseCursorScripts
{
    public class MouseCursorModel
    {
        public Vector2 MousePosition;
        public CursorTypes CurrentCursorType = CursorTypes.Normal;
        
        private Camera _sceneCamera;
        private GameContext _gameContext;
        private Plane _groundPlane = new Plane(Vector3.up, Vector3.zero);

        public MouseCursorModel(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void InitModel()
        {
            _sceneCamera = _gameContext.SceneHandler.GetSceneHandlerByName(_gameContext.SceneHandler.GameSceneName)
                .SceneCamera;
            UpdateCursorType(CursorTypes.Normal);
        }

        public void UpdateModel(float deltaTime, GameContext gameContext)
        {
            MousePosition = Mouse.current.position.ReadValue();
        }
        
        public void UpdateCursorType(CursorTypes cursorType)
        {
            Texture2D cursorTexture;
            switch (cursorType)
            {
                case CursorTypes.Hand:
                    cursorTexture = _gameContext.LevelDescription.HandCursor;
                    break;
                case CursorTypes.Fist:
                    cursorTexture = _gameContext.LevelDescription.FistCursor;
                    break;
                default:
                    cursorTexture = _gameContext.LevelDescription.NormalCursor;
                    break;
            }
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public Vector3 GetMousePositionOnGround()
        {
            Ray ray = _sceneCamera.ScreenPointToRay(MousePosition);
            float distance;
            if (!_groundPlane.Raycast(ray, out distance)) return Vector3.zero;
            return ray.GetPoint(distance);
        }
    }
}