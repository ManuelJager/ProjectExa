using Exa.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.SceneManagement
{
    public enum LoadScreenMode
    {
        CloseOnPrepared,
        ManuallyClose,
        None
    }

    public class TransitionArgs
    {
        public LoadSceneMode loadSceneMode;
        public LoadScreenMode loadScreenMode;
        public bool setActiveScene = false;
        public bool reportProgress = false;
    }

    public class ExaSceneManager : MonoBehaviour
    {
        private LoadingScreen loadingScreen;

        private void Awake()
        {
            loadingScreen = Systems.UI.loadingScreen;
        }

        public SceneTransition Transition(string name, TransitionArgs transitionArgs)
        {
            var operation = SceneManager.LoadSceneAsync(name, transitionArgs.loadSceneMode);
            var transition = new SceneTransition(operation);

            if (transitionArgs.loadScreenMode != LoadScreenMode.None)
            {
                loadingScreen.ShowScreen();

                if (transitionArgs.loadScreenMode == LoadScreenMode.CloseOnPrepared)
                    transition.onPrepared.AddListener(loadingScreen.HideScreen);

                StartCoroutine(ReportOperation(operation));
            }

            if (transitionArgs.setActiveScene)
            {
                transition.onPrepared.AddListener(() =>
                {
                    var scene = SceneManager.GetSceneByName(name);
                    SceneManager.SetActiveScene(scene);
                });
            }

            return transition;
        }

        private IEnumerator ReportOperation(AsyncOperation operation)
        {
            while (true)
            {
                loadingScreen.ShowMessage($"Loading scene ({Mathf.RoundToInt(operation.progress * 100)}% complete) ...");

                if (operation.isDone) 
                    break;
                
                yield return operation;
            }
        }
    }
}