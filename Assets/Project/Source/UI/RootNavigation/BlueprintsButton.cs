using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI
{
    public class BlueprintsButton : MonoBehaviour
    {
        private void Start() {
            if (!Application.isEditor) {
                gameObject.SetActive(false);
            }
        }
    }
}

