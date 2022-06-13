using UnityEngine;

namespace Collectables
{
    public class Heart : Collectable
    {
        private GameSession _gameSession;
        public int value;
    

        protected override void Collect(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _gameSession = GameSession.Instance;
            if (_gameSession.PlayerLives + value > _gameSession.PlayerMaxLives) return;
            _gameSession.PlayerLives += value;
        }
    }
}