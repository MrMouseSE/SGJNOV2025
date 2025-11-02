using System;
using UnityEngine;

namespace BallScripts
{
    [RequireComponent(typeof(Collider))]
    public class BallContainer : MonoBehaviour
    {
        public GameObject BallGameObject;
        public Transform Transform;
        public Vector3 Position;
        public float Velocity;
        public Vector3 Direction = new Vector3(0,0,0);
        
        public event Action<Collider> E_collisionEntered;

        private void OnTriggerEnter(Collider other)
        {
            E_collisionEntered?.Invoke(other);
        }
    }
}