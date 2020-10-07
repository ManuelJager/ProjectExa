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
        [SerializeField] private float charTime;
        [SerializeField] private Text text;
        [SerializeField] private bool animateOnEnable = true;
        [SerializeField] private float animateOnEnableDelay = 0f;
        private float startTime;
        private SignificantCharStringReader stringReader;
        private bool animating;

        public void OnEnable()
        {
            if (!animateOnEnable) return;

            var enumerator = EnumeratorUtils.Delay(() => AnimateTo(text.text), animateOnEnableDelay);
            StartCoroutine(enumerator);
        }

        public void AnimateTo(string str)
        {
            text.text = "";
            animating = true;
            startTime = Time.time;
            stringReader = new SignificantCharStringReader(str);
        }

        public void Update()
        {
            if (!animating) return;

            var timeSinceStart = Time.time - startTime;
            var charIndex = Mathf.FloorToInt(timeSinceStart / charTime);

            try
            {
                var targetString = stringReader.GetStringToSignificantIndex(charIndex);
                
                if (targetString == stringReader.Str)
                {
                    text.text = stringReader.Str;
                    animating = false;
                    return;
                }

                text.text = (targetString + StringExtensions.GetRandomChar()).PadRight(stringReader.Str.Length);
            }
            catch (IndexOutOfRangeException)
            {
                text.text = stringReader.Str;
                animating = false;
            }
        }

        public class SignificantCharStringReader
        {
            public int SignificantSize { get; private set; }
            public string Str { get; private set; }
            private Dictionary<int, int> indices;


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
                var realIndex = indices[index];

                for (var i = 0; i <= realIndex; i++)
                {
                    builder.Append(Str[i]);
                }

                return builder.ToString();
            }

            private void BuildIndices()
            {
                indices = new Dictionary<int, int>(Str.Length);
                var significantIndex = 0;
                for (var i = 0; i < Str.Length; i++)
                {
                    var currentChar = Str[i];
                    if (currentChar == ' ' || currentChar == '\t') continue;

                    indices[significantIndex] = i;
                    significantIndex++;
                }

                SignificantSize = significantIndex;
            }
        }
    }
}
