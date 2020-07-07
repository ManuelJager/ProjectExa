using Exa.SceneManagement;
using UnityEngine;

namespace Exa
{
    public class StartUpManager : MonoBehaviour
    {
        private void Start()
        {
            var transition = ExaSceneManager.Instance.Transition("Main", new TransitionArgs
            {
                loadScreenMode = LoadScreenMode.CloseOnPrepared
            });

            MainManager.Prepared.AddListener(() =>
            {
                transition.onScenePrepared?.Invoke();
            });
        }
    }
}