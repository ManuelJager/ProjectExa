using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Exa.Misc;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class TextAnimator : MonoBehaviour
    {
        [SerializeField] private float _charTime;
        [SerializeField] private Text _text;
        [SerializeField] private bool _animateOnEnable = true;
        [SerializeField] private float _animateOnEnableDelay = 0f;
        private float _startTime;
        private SignificantCharStringReader _stringReader;
        private bool _animating;

        public void OnEnable()
        {
            if (!_animateOnEnable) return;

            var enumerator = EnumeratorUtils.Delay(() => AnimateTo(_text.text), _animateOnEnableDelay);
            StartCoroutine(enumerator);
        }

        public void AnimateTo(string str)
        {
            _text.text = "";
            _animating = true;
            _startTime = Time.time;
            _stringReader = new SignificantCharStringReader(str);
        }

        public void Update()
        {
            if (!_animating) return;

            var timeSinceStart = Time.time - _startTime;
            var charIndex = Mathf.FloorToInt(timeSinceStart / _charTime);

            try
            {
                var targetString = _stringReader.GetStringToSignificantIndex(charIndex);
                
                if (targetString == _stringReader.Str)
                {
                    _text.text = _stringReader.Str;
                    _animating = false;
                    return;
                }

                _text.text = (targetString + StringExtensions.GetRandomChar()).PadRight(_stringReader.Str.Length);
            }
            catch (IndexOutOfRangeException)
            {
                _text.text = _stringReader.Str;
                _animating = false;
            }
        }

        public class SignificantCharStringReader
        {
            public int SignificantSize { get; private set; }
            public string Str { get; private set; }
            private Dictionary<int, int> _indices;


            public SignificantCharStringReader(string str)
            {
                this.Str = str;
                BuildIndices();
            }

            public string GetStringToSignificantIndex(int index)
            {
                if (index < 0 || index >= SignificantSize)
                {
                    throw new IndexOutOfRangeException();
                }

                var builder = new StringBuilder();
                var realIndex = _indices[index];

                for (var i = 0; i <= realIndex; i++)
                {
                    builder.Append(Str[i]);
                }

                return builder.ToString();
            }

            private void BuildIndices()
            {
                _indices = new Dictionary<int, int>(Str.Length);
                var significantIndex = 0;
                for (var i = 0; i < Str.Length; i++)
                {
                    var currentChar = Str[i];
                    if (currentChar == ' ' || currentChar == '\t') continue;

                    _indices[significantIndex] = i;
                    significantIndex++;
                }

                SignificantSize = significantIndex;
            }
        }
    }
}
