using Exa.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa
{
    public class ExaSceneManager : MonoSingleton<ExaSceneManager>
    {
        public void Transition(string name, LoadSceneMode mode)
        {
            var operation = SceneManager.LoadSceneAsync(name, mode);
            StartCoroutine(ReportOperation(operation));
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