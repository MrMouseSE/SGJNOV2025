using System;
using UnityEngine;

namespace BallScripts
{
    [RequireComponent(typeof(Collider))]
    public class BallContainer : MonoBehaviour
    {
        public GameObject BallGameObject;
        public Transform Transform;
        public int Bounces = 5;
        public Collider Collider;
        public MeshRenderer MeshRenderer;
        public ParticleSystem BallDestroyParticleSystem;
        public Transform BallDestroyParticlesTransform;
        public event Action<Collider> E_collisionEntered;

        public void DestroyBall()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            E_collisionEntered?.Invoke(other);
        }
    }
}