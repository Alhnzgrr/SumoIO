using System;
using UnityEngine;

namespace Sumo.Data
{
    [CreateAssetMenu(menuName = "Data/Event Data", fileName = "Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnGameStart { get; set; }
        public Action OnGameEnd { get; set; }
    }
}
