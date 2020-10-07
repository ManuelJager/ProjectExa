using UnityEngine.Events;

namespace Exa.SceneManagement
{
    public interface ISceneTransition
    {
        UnityEvent OnPrepared { get; }

        void MarkPrepared();
    }
}