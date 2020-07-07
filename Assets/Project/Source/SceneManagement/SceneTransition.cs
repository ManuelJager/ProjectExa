using UnityEngine;
using UnityEngine.Events;

namespace Exa.SceneManagement
{
    public class SceneTransition : ISceneTransition
    {
        public UnityEvent onSceneLoaded { get; }
        public UnityEvent onScenePrepared { get; }

        public SceneTransition(AsyncOperation loadOperation)
        {
            onSceneLoaded = new UnityEvent();
            onScenePrepared = new UnityEvent();
            loadOperation.completed += (op) => onSceneLoaded?.Invoke();
        }

        public void MarkPrepared()
        {
            onScenePrepared.Invoke();
        }
    }
}