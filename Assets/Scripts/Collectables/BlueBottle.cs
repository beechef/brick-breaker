using Effects;
using UnityEngine;

    namespace Collectables
    {
        public class BlueBottle : Collectable
        {
            public float value;
            public float times;
            protected override void Collect(Collider2D other)
            {
                Paddle paddle = other.GetComponent<Paddle>();
                if (paddle == null) return;
                EffectManager.Instance.AddEffect(new SpeedUpEffect(value, times, paddle), EffectType.Adding);
            }
        }
    }
