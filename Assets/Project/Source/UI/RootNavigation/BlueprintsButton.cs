using UnityEngine;

namespace Exa.UI {
    public class BlueprintsButton : MonoBehaviour {
        [SerializeField] private bool allowAnytime;
        
        private void Start() {
            if (!Application.isEditor && !allowAnytime) {
                gameObject.SetActive(false);
            }
        }
    }
}