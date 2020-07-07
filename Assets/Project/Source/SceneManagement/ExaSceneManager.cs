using Exa.UI;
using Exa.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.SceneManagement
{
    public enum LoadScreenMode
    {
        CloseOnLoad,
        CloseOnPrepared,
        None
    }

    public class TransitionArgs
    {
        public LoadSceneMode loadSceneMode = LoadSceneMode.Additive;
        public LoadScreenMode loadScreenMode = LoadScreenMode.CloseOnLoad;
    }

    public class ExaSceneManager : MonoSingleton<ExaSceneManager>
    {
        [SerializeField] private LoadingScreen loadingScreen;

        public ISceneTransition Transition(string name, TransitionArgs transitionArgs = null)
        {
            if (transitionArgs == null)
            {
                transitionArgs = new TransitionArgs();
            }

            var operation = SceneManager.LoadSceneAsync(name, transitionArgs.loadSceneMode);
            var transition = new SceneTransition(operation);

            if (transitionArgs.loadScreenMode != LoadScreenMode.None)
            {
                loadingScreen.ShowScreen();

                if (transitionArgs.loadScreenMode == LoadScreenMode.CloseOnLoad)
                {
                    transition.onSceneLoaded.AddListener(loadingScreen.MarkLoaded);
                }

                if (transitionArgs.loadScreenMode == LoadScreenMode.CloseOnPrepared)
                {
                    transition.onScenePrepared.AddListener(loadingScreen.MarkLoaded);
                }
            }

            StartCoroutine(ReportOperation(operation));
            return transition;
        }

        IEnumerator ReportOperation(AsyncOperation operation)
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