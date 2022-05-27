using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class FinishTrigger : MonoBehaviour
    {
        private GameState _gameState;

        public void Init(GameState gameState)
        {
            _gameState = gameState;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                _gameState.WinLevel();
            }
        }
    }
}