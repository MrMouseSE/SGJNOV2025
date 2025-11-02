using UnityEngine;

namespace PlayerScripts
{
    public class PlayerContainer : MonoBehaviour
    {
        [SerializeField] public Transform Transform;
        [SerializeField] public Transform BallHoldPoint;
        [SerializeField] public float Speed;
    }
}