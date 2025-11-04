using UnityEngine;

namespace TileObjectScripts
{
    public class MovedContentSupport : MonoBehaviour
    {
        public GameObject SupportGameObject;
        public Animation Animation;
        public string ShowSupportMessage;
        public string HideSupportMessage;

        private bool _wasInited;
        
        public void Hide()
        {
            SupportGameObject.SetActive(false);
        }

        public void Show()
        {
            _wasInited = true;
            SupportGameObject.SetActive(false);
            Animation.Play(ShowSupportMessage);
        }

        public void StartHideAnimation()
        {
            if (!_wasInited) return;
            Animation.Play(HideSupportMessage);
        }

        public void OnAnimationFinished()
        {
            SupportGameObject.SetActive(false);
        }
    }
}
