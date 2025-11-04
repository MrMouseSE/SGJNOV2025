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
            if (_wasInited) return;
            _wasInited = true;
            SupportGameObject.SetActive(false);
        }

        public void Show()
        {
            SupportGameObject.SetActive(true);
            Animation.Play(ShowSupportMessage);
        }

        public void StartHideAnimation()
        {
            Animation.Play(HideSupportMessage);
        }

        public void OnAnimationFinished()
        {
            SupportGameObject.SetActive(false);
        }
    }
}
