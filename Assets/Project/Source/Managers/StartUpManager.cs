using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa
{
    public class StartUpManager : MonoBehaviour
    {
        private void Start()
        {
            ExaSceneManager.Instance.Transition("Main", LoadSceneMode.Additive);
        }
    }
}
