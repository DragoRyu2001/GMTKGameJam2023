using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleSwapEffect : MonoBehaviour
    {
        [SerializeField] private Color Color1;
        [SerializeField] private Color Color2;
        private Image _background;
        private TextMeshProUGUI _text;
        private ColorTransition _backgroundColor;
        private ColorTransition _textColor;
        private Toggle _toggle;
        private void Start()
        {
            _toggle = transform.GetComponent<Toggle>();
            _background = transform.GetComponentInChildren<Image>();
            _text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            Debug.Assert(_text!=null);
            _backgroundColor = new ColorTransition(Color1, Color2, 1f);
            _textColor = new ColorTransition(Color2, Color1, 1f);
        }
        private void Update()
        {
            _backgroundColor.Flip(_toggle.isOn);
            _textColor.Flip(_toggle.isOn);
            
            _background.color = _backgroundColor.Update();
            _text.color       = _textColor.Update();
        }
    }
}
