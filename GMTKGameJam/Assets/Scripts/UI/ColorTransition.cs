using UnityEngine;

namespace UI
{
    public class ColorTransition
    {
        private bool _flip;
        private readonly Color _a;
        private readonly Color _b;
        private Color _currentColor;
        private float _value;
        private float _speed;
        public ColorTransition(Color a, Color b, float speed)
        {
            _a = a;
            _b = b;
            _speed = speed;
        }

        public void Flip(bool flip)
        {
            if (flip == _flip) return;
            _flip = flip;
            _value = 0f;
        }
        public Color Update()
        {
            _value += Time.deltaTime;
            _currentColor = Color.Lerp(_flip ? _a : _b, _flip ? _b : _a, _value);
            return _currentColor;
        }
    }
}