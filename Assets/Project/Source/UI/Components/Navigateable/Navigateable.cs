using UnityEngine;

namespace Exa.UI.Components
{
    public struct NavigationArgs
    {
        public bool isReturning;
    }

    // TODO: Add a manager that implements a command pattern to store user Navigation actions
    public class Navigateable : MonoBehaviour, IUIGroup
    {
        public bool Interactable { get; set; } = true;

        public virtual void HandleExit()
        {
            gameObject.SetActive(false);
        }

        public virtual void HandleEnter(Navigateable from, NavigationArgs args = default)
        {
            gameObject.SetActive(true);
        }

        public virtual void NavigateTo(Navigateable to, NavigationArgs args = default)
        {
            HandleExit();
            to.HandleEnter(this, args);
        }
    }
}