using Exa.UI;
using Exa.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa
{
    public class TransitionArgs
    {
        public LoadSceneMode loadSceneMode = LoadSceneMode.Additive;
        public bool showLoadingScreen = true;
        public bool hideLoadingScreen = true; 
    }

    public class ExaSceneManager : MonoSingleton<ExaSceneManager>
    {
        [SerializeField] private LoadingScreen loadingScreen;

        public void Transition(string name, TransitionArgs transitionArgs = null)
        {
            if (transitionArgs == null)
            {
                transitionArgs = new TransitionArgs();
            }

            var operation = SceneManager.LoadSceneAsync(name, transitionArgs.loadSceneMode);
            if (transitionArgs.showLoadingScreen)
            {
                loadingScreen.ShowScreen(transitionArgs.hideLoadingScreen);
            }
            StartCoroutine(ReportOperation(operation));
        }

        IEnumerator ReportOperation(AsyncOperation operation)
        {
            while (true)
            {
                if (operation.isDone)
                {
                    loadingScreen.MarkLoaded();
                    break;
                }

                yield return operation;
            }
        }
    }
}