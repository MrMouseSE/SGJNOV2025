using UnityEngine;

namespace TileObjectScripts
{
    public class SupportMessageMono : MonoBehaviour
    {
        public GameObject messageObject;
        public Animation HideAnimation;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartHideAnimation();
            }
        }

        private void StartHideAnimation()
        {
            HideAnimation.Play();
        }

        public void OnAnimationEnd()
        {
            Destroy(messageObject);
        }
    }
}