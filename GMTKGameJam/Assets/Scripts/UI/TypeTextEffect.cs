using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TypeTextEffect : MonoBehaviour
    {
        public float LetterDelay;
        public float InitialDelay;
        private TextMeshProUGUI _textMesh;
        private string _originalText;

        private void Awake()
        {
            if (_textMesh == null)
                _textMesh = GetComponent<TextMeshProUGUI>();
            _originalText = _textMesh.text;
            
        }

        private void OnEnable()
        {
            StartCoroutine(Effect());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
        
        private IEnumerator Effect()
        {
            _textMesh.text = string.Empty;
            yield return new WaitForSeconds(InitialDelay);
            foreach (char t in _originalText)
            {
                yield return new WaitForSeconds(LetterDelay);
                _textMesh.text += t;
            }
        }
    }
}
