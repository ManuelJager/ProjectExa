using UnityEngine;
using UnityEngine.Events;

namespace Exa.SceneManagement
{
    public class SceneTransition : ISceneTransition
    {
        public UnityEvent OnPrepared { get; }

        public SceneTransition(AsyncOperation loadOperation)
        {
            OnPrepared = new UnityEvent();
            loadOperation.completed += (op) => OnPrepared?.Invoke();
        }

        public void MarkPrepared()
        {
            OnPrepared.Invoke();
        }
    }
}