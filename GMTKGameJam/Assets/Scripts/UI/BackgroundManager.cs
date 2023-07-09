using System;
using UnityEngine;

namespace UI
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private Color StartColor;
        [SerializeField] private Color GameStartColor;
        [SerializeField] private Color BossColor;
        private Color _startColor;
        private Color _currentColor;
        private Color _targetColor;
        private float _value;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _startColor = StartColor;
            _targetColor = _startColor;
            _currentColor = _startColor;
        }

        private void Start()
        {
            GameManager.Instance.GameStart += StartGame;
            GameManager.Instance.BossPhase += BossAppeared;
        }

        private void Update()
        {
            if (_currentColor != _targetColor)
            {
                _value += Time.deltaTime;
                _currentColor = Color.Lerp(_startColor, _targetColor, _value);
                _renderer.color = _currentColor;
            }
            else
            {
                _startColor = _currentColor;
                _value = 0;
            }
        }
        private void StartGame()
        {
            _targetColor = GameStartColor;
        }

        private void BossAppeared()
        {
            _targetColor = BossColor;
        }
    }
}
