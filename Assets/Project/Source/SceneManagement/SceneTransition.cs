using UnityEngine;
using UnityEngine.Events;

namespace Exa.SceneManagement
{
    public class SceneTransition : ISceneTransition
    {
        public UnityEvent onPrepared { get; }

        public SceneTransition(AsyncOperation loadOperation)
        {
            onPrepared = new UnityEvent();
            loadOperation.completed += op => onPrepared?.Invoke();
        }

        public void MarkPrepared()
        {
            onPrepared.Invoke();
        }
    }
}