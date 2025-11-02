using UnityEngine;

namespace TileScript
{
    [RequireComponent(typeof(Collider))]
    public class TileContainer : MonoBehaviour
    {
        public Collider Collider;
        public TileMode Mode;
        public ITileModel TileModel { get; private set; }

        public void Initialize(ITileModel tileModel)
        {
            TileModel = tileModel;
        }
    }
}