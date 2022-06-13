using Effects;
using UnityEngine;

namespace Collectables
{
    public class Gear : Collectable
    {
        public float value = 2f;
        public float times = 10f;

        protected override void Collect(Collider2D other)
        {
            Paddle paddle = other.GetComponent<Paddle>();
            if (paddle == null) return;
            EffectManager.Instance.AddEffect(new ExpandPaddleEffect(value, times, paddle), EffectType.Replace);
        }
    }
}