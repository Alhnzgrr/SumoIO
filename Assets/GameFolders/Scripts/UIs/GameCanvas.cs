using System;
using Sumo.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sumo.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        private void OnEnable()
        {
            playButton.onClick?.AddListener(PlayGame);
        }

        private void OnDisable()
        {
            playButton.onClick?.RemoveListener(PlayGame);
        }

        private void PlayGame()
        {
            DataManager.Instance.EventData.OnGameStart?.Invoke();
            playButton.gameObject.SetActive(false);
        }
    }
}
