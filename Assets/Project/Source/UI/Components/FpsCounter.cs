using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private Text fpsText;

        public void Update()
        {
            fpsText.text = $"{Mathf.RoundToInt(1f / Time.smoothDeltaTime)} FPS";
        }
    }
}

