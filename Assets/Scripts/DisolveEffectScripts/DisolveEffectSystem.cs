using System;
using UnityEngine;

namespace DisolveEffectScripts
{
    public class DisolveEffectSystem : IGameSystem
    {
        public DisolveEffectContainer Container;
        public DisolveEffectModel Model;

        public Action OnAnimationHalf;
        public Action OnAnimationEnd;
        
        public DisolveEffectSystem(DisolveEffectContainer container)
        {
            Container = container;
            Model = new DisolveEffectModel(Container);
        }
        
        public void InitGameSystem()
        {
            Model.SwitchEffectWisibility(false);
        }

        public void SetToCurrentCamera(Camera camera)
        {
            Model.SetToCurrentCamera(camera);
        }

        public void UpdateGameSystem(float deltaTime, GameContext gameContext)
        {
            Model.UpdateModel(deltaTime);
        }

        public void StartDisolveEffect()
        {
            Model.SwitchEffectWisibility(true);
            Model.OnAnimationEnd += AnimationEnd;
            Model.OnAnimationHalf += AnimationHalf;
            Model.StartDisolveEffect();
        }

        private void AnimationHalf()
        {
            Model.OnAnimationHalf -= AnimationHalf;
            OnAnimationHalf?.Invoke();
        }
        
        private void AnimationEnd()
        {
            Model.OnAnimationEnd -= OnAnimationEnd;
            OnAnimationEnd?.Invoke();
            Model.SwitchEffectWisibility(false);
        }
    }
}