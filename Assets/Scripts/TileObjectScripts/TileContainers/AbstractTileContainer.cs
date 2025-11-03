using System.Collections.Generic;
using UnityEngine;

namespace TileObjectScripts.TileContainers
{
    [RequireComponent(typeof(Collider))]
    public class AbstractTileContainer : MonoBehaviour
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        public GameObject TileGameObject;
        public Transform TileTransform;
        public TilesTypes TileType;
        public MeshRenderer TileMeshRenderer;
        public MeshRenderer GlowMeshRenderer;
        public MeshRenderer AdditionalMeshRenderer;
        [Space]
        public AnimationCurve FinalAnimationCurve;
        
        [Space]
        public List<Collider> Colliders;
        
        [HideInInspector]
        public ITileModel TileModel;
        [HideInInspector]
        public TileObjectHandler TileObjectHandler;

        private Dictionary<MeshRenderer, Color> _meshRendererColors = new Dictionary<MeshRenderer, Color>();
        private Dictionary<MeshRenderer, Color> _meshRendererCurrentColors = new Dictionary<MeshRenderer, Color>();
        
        public void Initialize(ITileModel tileModel)
        {
            if (TileMeshRenderer != null) _meshRendererColors.Add(TileMeshRenderer, TileMeshRenderer.material.GetColor(EmissionColor));
            if (GlowMeshRenderer != null) _meshRendererColors.Add(GlowMeshRenderer, GlowMeshRenderer.material.GetColor(EmissionColor));
            if (AdditionalMeshRenderer != null) _meshRendererColors.Add(AdditionalMeshRenderer, AdditionalMeshRenderer.material.GetColor(EmissionColor));
            
            TileModel = tileModel;
            foreach (var meshRendererColor in _meshRendererColors)
            {
                meshRendererColor.Key.material.SetColor(EmissionColor, Color.black);
            }
        }

        public void CollectCurrentColors()
        {
            foreach (var rendererColor in _meshRendererColors)
            {
                _meshRendererCurrentColors.Add(rendererColor.Key, rendererColor.Key.material.GetColor(EmissionColor));
            }
        }

        public void ProcessFinalAnimation(float animationTime, bool forceBlack = false)
        {
            float evaluatedValue = FinalAnimationCurve.Evaluate(animationTime);
            foreach (var meshRendererColor in _meshRendererColors)
            {
                Color fromColor = forceBlack? Color.black : _meshRendererCurrentColors[meshRendererColor.Key];
                var currentColor = Color.Lerp(fromColor, meshRendererColor.Value, evaluatedValue);
                meshRendererColor.Key.material.SetColor(EmissionColor, currentColor);
            }
        }
    }
}