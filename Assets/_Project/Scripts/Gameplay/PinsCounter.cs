using UnityEngine;

namespace Bowling.Gameplay
{
    public class PinsCounter : MonoBehaviour
    {
        public int PinsCount = 0;

        private void FixedUpdate()
        {
            PinsCount = 0;
            foreach (var pin in GetComponentsInChildren<FinalPin>())
            {
                if (pin.Fell())
                {
                    PinsCount++;
                }
            }
        }
    }
}