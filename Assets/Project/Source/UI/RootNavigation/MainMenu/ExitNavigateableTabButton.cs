using Exa.UI.Components;
using UnityEditor;

namespace Exa.UI
{
    public class ExitNavigateableTabButton : Navigateable
    {
        public override void HandleEnter(NavigationArgs args)
        {
            Exit();
        }

        public void Exit()
        {
            Systems.Quit();
        }
    }
}