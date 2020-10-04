using Exa.UI.Components;
using UnityEditor;

namespace Exa.UI
{
    public class ExitNavigateableTabButton : Navigateable
    {
        public override void HandleEnter(Navigateable from, NavigationArgs args = default)
        {
            Exit();
        }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}