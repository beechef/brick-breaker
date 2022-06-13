using UnityEngine;

namespace Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        protected abstract void Collect(Collider2D other);

        private void OnCollisionEnter2D(Collision2D other)
        {
            Collect(other.collider);
            Destroy(gameObject);
        }
    
    }
}