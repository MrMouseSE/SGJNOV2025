using UnityEngine;

namespace DisolveEffectScripts
{
    public class DisolveEffectContainer : MonoBehaviour
    {
        public GameObject DisolveEffectGameObject;
        public Transform DisolveTransform;
        public Vector2 FromToFirstWave;
        public AnimationCurve FirstWaveAnimation;
        public Vector2 FromToSecondWave;
        public AnimationCurve SecondWaveAnimation;
        public float AnimationDuration;
        public MeshRenderer DisolveMeshRenderer;
        public string DisolveEffectValueName;
        
        [HideInInspector]
        public int DisolveEffectProperty;

        private void Start()
        {
            DisolveEffectProperty = Shader.PropertyToID(DisolveEffectValueName);
        }
    }
}
