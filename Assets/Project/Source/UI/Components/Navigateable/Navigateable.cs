using UnityEngine;

namespace Exa.UI.Components
{
    public class Navigateable : MonoBehaviour, IInteractableGroup
    {
        public bool Interactable { get; set; } = true;

        public virtual void OnExit()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnNavigate(Navigateable from, bool storeFrom = true)
        {
            gameObject.SetActive(true);
        }

        public virtual void NavigateTo(Navigateable to, bool storeFrom = true)
        {
            OnExit();
            to.OnNavigate(this, storeFrom);
        }
    }
}