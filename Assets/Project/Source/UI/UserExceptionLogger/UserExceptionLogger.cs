using Exa.Generics;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI
{
    public class UserExceptionLogger : MonoSingleton<UserExceptionLogger>
    {
        [SerializeField] private GameObject userExceptionPrefab;
        [SerializeField] private Transform container;

        public void Log(UserException exception)
        {
            Log(exception.Message);
        }

        public void Log(string message)
        {
            var exceptionView = Instantiate(userExceptionPrefab, container).GetComponent<UserExceptionView>();
            exceptionView.Message = message;
        }
    }
}