using System;
using Sumo.Core;
using TMPro;
using UnityEngine;

namespace Sumo.UI
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private float gameTime;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject timerPanel;

        private float _gameTimeCountdown;
        private bool _isGamePlaying;

        private int _minutes;
        private int _seconds;

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnGameStart += StartGameHandler;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnGameStart -= StartGameHandler;
        }

        private void Update()
        {
            UpdateMatchTimer();
        }

        private void StartGameHandler()
        {
            _isGamePlaying = true;
            _gameTimeCountdown = gameTime;
            timerPanel.SetActive(true);
        }

        private void UpdateMatchTimer()
        {
            if (!_isGamePlaying) return;

            _gameTimeCountdown -= Time.deltaTime;

            _minutes = Mathf.FloorToInt(_gameTimeCountdown / 60);
            _seconds = Mathf.FloorToInt(_gameTimeCountdown % 60);

            timerText.text = $"{_minutes:00}:{_seconds:00}";
            
            if (_gameTimeCountdown <= 0)
            {
                EndMatch();
            }
        }

        private void EndMatch()
        {
            _isGamePlaying = false;
            timerPanel.SetActive(false);
        }
    }
}
