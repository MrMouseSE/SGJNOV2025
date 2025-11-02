using System.Collections.Generic;
using UnityEngine;

namespace GameSceneScripts.RecipeReferenceViewScripts
{
    public class CurrentRecipeViewReferenceContainer : MonoBehaviour
    {
        public GameObject ContainerGameObject;
        public Transform ContainerTransform;

        [Space]
        public Transform RecipeRenderersAnchor;
        public Animation RecipeContainerAnimation;

        [Space]
        public RecipeItemRendererContainer ReferenceItemRendererPrefab;
        
        [HideInInspector]
        public List<RecipeItemRendererContainer> RecipeItemsRenderers;
    }
}