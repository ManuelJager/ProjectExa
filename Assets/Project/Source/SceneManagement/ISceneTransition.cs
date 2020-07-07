using UnityEngine.Events;

namespace Exa.SceneManagement
{
    public interface ISceneTransition
    {
        UnityEvent onSceneLoaded { get; }
        UnityEvent onScenePrepared { get; }

        void MarkPrepared();
    }
}