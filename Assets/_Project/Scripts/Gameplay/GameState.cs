using System;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class GameState : MonoBehaviour
    {
        public enum State
        {
            WaitingForTap,
            Playing,
            Lost,
            Final
        }

        public State CurrentState
        {
            get => _currentState;
            private set
            {
                _currentState = value;
                GameStateChanged?.Invoke(value);
            }
        }

        public event Action<State> GameStateChanged;

        private State _currentState = State.WaitingForTap;

        public void StartGame()
        {
            CurrentState = State.Playing;
        }

        public void LoseGame()
        {
            CurrentState = State.Lost;
        }

        public void WinLevel()
        {
            CurrentState = State.Final;
        }
    }
}