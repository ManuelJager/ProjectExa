using Exa.UI;
using Exa.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.SceneManagement
{
    public enum LoadScreenMode
    {
        CloseOnPrepared,
        None
    }

    public class TransitionArgs
    {
        public LoadSceneMode loadSceneMode = LoadSceneMode.Additive;
        public LoadScreenMode loadScreenMode = LoadScreenMode.CloseOnPrepared;
        public bool setActiveScene = false;
    }

    public class ExaSceneManager : MonoBehaviour
    {
        private LoadingScreen _loadingScreen;

        private void Awake()
        {
            _loadingScreen = Systems.Ui.loadingScreen;
        }

        public SceneTransition Transition(string name, TransitionArgs transitionArgs)
        {
            var operation = SceneManager.LoadSceneAsync(name, transitionArgs.loadSceneMode);
            var transition = new SceneTransition(operation);

            if (transitionArgs.loadScreenMode != LoadScreenMode.None)
            {
                _loadingScreen.ShowScreen();

                if (transitionArgs.loadScreenMode == LoadScreenMode.CloseOnPrepared)
                {
                    transition.OnPrepared.AddListener(_loadingScreen.HideScreen);
                }

                if (transitionArgs.setActiveScene)
                {
                    transition.OnPrepared.AddListener(() =>
                    {
                        var scene = SceneManager.GetSceneByName(name);
                        SceneManager.SetActiveScene(scene);
                    });
                }
            }

            StartCoroutine(ReportOperation(operation));
            return transition;
        }

        private IEnumerator ReportOperation(AsyncOperation operation)
        {
            while (true)
            {
                if (operation.isDone)
                {
                    break;
                }

                yield return operation;
            }
        }
    }
}