using System;
using UnityEngine;

namespace BallScripts
{
    [RequireComponent(typeof(Collider))]
    public class BallContainer : MonoBehaviour
    {
        public Transform Transform;
        
        public event Action<Collider> E_collisionEntered;

        private void OnTriggerEnter(Collider other)
        {
            E_collisionEntered?.Invoke(other);
        }
    }
}