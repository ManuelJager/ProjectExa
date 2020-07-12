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
        public bool SetActiveScene = false;
    }

    public class ExaSceneManager : MonoSingleton<ExaSceneManager>
    {
        [SerializeField] private LoadingScreen loadingScreen;

        public SceneTransition Transition(string name, TransitionArgs transitionArgs)
        {
            var operation = SceneManager.LoadSceneAsync(name, transitionArgs.loadSceneMode);
            var transition = new SceneTransition(operation);

            if (transitionArgs.loadScreenMode != LoadScreenMode.None)
            {
                loadingScreen.ShowScreen();

                if (transitionArgs.loadScreenMode == LoadScreenMode.CloseOnPrepared)
                {
                    transition.onPrepared.AddListener(loadingScreen.MarkLoaded);
                }

                if (transitionArgs.SetActiveScene)
                {
                    var scene = SceneManager.GetSceneByName(name);
                    SceneManager.SetActiveScene(scene);
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