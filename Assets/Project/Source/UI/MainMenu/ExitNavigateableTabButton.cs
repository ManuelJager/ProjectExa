using Exa.UI.Components;
using UnityEditor;

namespace Exa.UI
{
    public class ExitNavigateableTabButton : Navigateable
    {
        public override void OnNavigate(Navigateable from, bool storeFrom = true)
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