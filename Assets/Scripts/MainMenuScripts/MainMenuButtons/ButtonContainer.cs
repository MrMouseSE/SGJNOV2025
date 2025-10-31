using System;
using UnityEngine;

namespace MainMenuScripts.MainMenuButtons
{
    public class ButtonContainer : MonoBehaviour
    {
        public Action ButtonActionPressed;
        public ButtonAnimationContainer ButtonContainerAnimation;
        
        public void OnMouseDown()
        {
            ButtonContainerAnimation.OnPressAnimationFinished += InvokePressAction;
            ButtonContainerAnimation.PressAnimation();
        }

        public void OnMouseEnter()
        {
            ButtonContainerAnimation.EnterAnimation();
        }

        public void OnMouseExit()
        {
            ButtonContainerAnimation.ExitAnimation();
        }

        private void InvokePressAction()
        {
            ButtonContainerAnimation.OnPressAnimationFinished -= InvokePressAction;
            ButtonActionPressed?.Invoke();
        }
    }
}
