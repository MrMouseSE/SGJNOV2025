using System;
using UnityEngine;

namespace DisolveEffectScripts
{
    public class DisolveEffectModel
    {
        public DisolveEffectContainer Container;

        private bool _isActive;
        private bool _isHalf;
        private float _currentTime;

        public Action OnAnimationHalf;
        public Action OnAnimationEnd;
        
        public DisolveEffectModel(DisolveEffectContainer container)
        {
            Container = container;
        }
        
        public void SwitchEffectWisibility(bool isActive)
        {
            Container.DisolveEffectGameObject.SetActive(isActive);
        }

        public void StartDisolveEffect()
        {
            _isActive = true;
            _isHalf = false;
            _currentTime = 0f;
        }

        public void SetToCurrentCamera(Camera camera)
        {
            Container.DisolveTransform.position = camera.transform.position + camera.transform.forward;
            Container.DisolveTransform.rotation = camera.transform.rotation;
        }

        public void UpdateModel(float deltaTime)
        {
            if (!_isActive) return;
            _currentTime += deltaTime;
            if (_currentTime > Container.AnimationDuration)
            {
                _currentTime = 1f;
                _isActive = false;
                EffectAnimationEnd();
            }
            float fromValue = Container.FirstWaveAnimation.Evaluate(_currentTime/Container.AnimationDuration);
            if (Mathf.Approximately(fromValue, 1f) && !_isHalf)
            {
                _isHalf = true;
                OnAnimationHalf?.Invoke();
            }
            float toValue = Container.SecondWaveAnimation.Evaluate(_currentTime/Container.AnimationDuration);
            Vector4 fromToEffectVector = new Vector4(GetWaveValue(fromValue, Container.FromToFirstWave), 
                    GetWaveValue(toValue, Container.FromToSecondWave), 0f, 0f); 
            Container.DisolveMeshRenderer.material.SetVector(Container.DisolveEffectProperty, fromToEffectVector);
        }

        private float GetWaveValue(float value, Vector2 range)
        {
            return Mathf.Lerp(range.x,range.y,value);
        }

        private void EffectAnimationEnd()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}
