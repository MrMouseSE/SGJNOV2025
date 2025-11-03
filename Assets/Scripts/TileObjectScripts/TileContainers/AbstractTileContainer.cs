using System.Collections;
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
        public bool IsGlowing;
        
        [Space]
        public AnimationCurve GlowUpAnimationCurve;
        
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
        
        private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
        
        public void Initialize(ITileModel tileModel, bool isGlowAtStart)
        {
            if (TileMeshRenderer != null) _meshRendererColors.Add(TileMeshRenderer, TileMeshRenderer.material.GetColor(EmissionColor));
            if (GlowMeshRenderer != null) _meshRendererColors.Add(GlowMeshRenderer, GlowMeshRenderer.material.GetColor(EmissionColor));
            if (AdditionalMeshRenderer != null) _meshRendererColors.Add(AdditionalMeshRenderer, AdditionalMeshRenderer.material.GetColor(EmissionColor));
            
            TileModel = tileModel;
            if (isGlowAtStart) return;
            foreach (var meshRendererColor in _meshRendererColors)
            {
                meshRendererColor.Key.material.SetColor(EmissionColor, Color.black);
            }
        }

        public void StartGlowUpTileAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(GlowUpAnimation());
        }

        private IEnumerator GlowUpAnimation()
        {
            IsGlowing = true;
            
            var currentTime = 0f;
            while (currentTime < GlowUpAnimationCurve.keys[^1].time)
            {
                currentTime += Time.deltaTime;
                ProcessGlowAnimation(currentTime, true);
                yield return _waitForEndOfFrame;
            }
        }

        public void CollectCurrentColors()
        {
            foreach (var rendererColor in _meshRendererColors)
            {
                _meshRendererCurrentColors.Add(rendererColor.Key, rendererColor.Key.material.GetColor(EmissionColor));
            }
        }

        public void ProcessGlowAnimation(float animationTime, bool forceBlack = false)
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