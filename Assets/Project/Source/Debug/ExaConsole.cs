using UCommandConsole;
using UnityEngine;
using Exa.Utils;

namespace Exa.Debugging
{
    public class ExaConsole : MonoBehaviour
    {
        [SerializeField] private Console console;

        private void OnEnable()
        {
            this.DelayOneFrame(console.input.Focus);
        }
    }
}