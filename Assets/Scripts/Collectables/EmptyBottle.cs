using Effects;
using UnityEngine;

namespace Collectables
{
    public class EmptyBottle : Collectable
    {
        protected override void Collect(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            EffectManager.Instance.Clear();
        }
    }
}
