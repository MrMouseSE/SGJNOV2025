using System;
using UnityEngine;

namespace MainMenuScripts.MainMenuButtons
{
    public class ButtonAnimationContainer : MonoBehaviour
    {
        public Animation Animation;
        public string EnterAnimationName;
        public string ExitAnimationName;
        public string PressAnimationName;

        public Action OnPressAnimationFinished;

        public void AnimationFinished()
        {
            OnPressAnimationFinished?.Invoke();
        }
        
        public void PressAnimation()
        {
            Animation.Play(PressAnimationName);
        }

        public void EnterAnimation()
        {
            Animation.Play(EnterAnimationName);
        }

        public void ExitAnimation()
        {
            Animation.Play(ExitAnimationName);
        }
        
    }
}
