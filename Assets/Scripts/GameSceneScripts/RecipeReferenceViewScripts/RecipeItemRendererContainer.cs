using UnityEngine;

namespace GameSceneScripts.RecipeReferenceViewScripts
{
    public class RecipeItemRendererContainer : MonoBehaviour
    {
        public GameObject RecipeRendererGameObject;
        public Transform RecipeRendererTransform;
        public Vector3 NormalPositionOffset;
        public Vector3 SelectedPositionOffset;

        [Space]
        public Animation RendererAnimation;
        public string NormalAnimationName;
        public string SelectedAnimationName;
        
        [Space]
        public SpriteRenderer RecipeItemRenderer;
    }
}