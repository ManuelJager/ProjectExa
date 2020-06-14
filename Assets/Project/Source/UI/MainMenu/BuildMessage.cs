using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI
{
    [CreateAssetMenu(fileName = "buildMessage", menuName = "UI/MainMenu/BuildMessage")]
    public class BuildMessage : ScriptableObject
    {
        [TextAreaAttribute]
        public string buildMessage;
    }
}

