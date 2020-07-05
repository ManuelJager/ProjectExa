using UnityEngine;

namespace Exa
{
    public class StartUpManager : MonoBehaviour
    {
        private void Start()
        {
            ExaSceneManager.Instance.Transition("Main");
        }
    }
}