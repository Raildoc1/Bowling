using System.Linq;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class PinsCounter : MonoBehaviour
    {
        public int FellPinsCount() => GetComponentsInChildren<FinalPin>().Count(pin => pin.Fell());
        public int PinsCount() => GetComponentsInChildren<FinalPin>().Length;
    }
}