using Exa.Generics;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI
{
    public class UserExceptionLogger : MonoSingleton<UserExceptionLogger>
    {
        [SerializeField] private GameObject userExceptionPrefab;

        public void Log(UserException exception)
        {
            var exceptionView = Instantiate(userExceptionPrefab, transform).GetComponent<UserExceptionView>();
            exceptionView.Message = exception.Message;
        }

        public void Log(string message)
        {
            var exceptionView = Instantiate(userExceptionPrefab, transform).GetComponent<UserExceptionView>();
            exceptionView.Message = message;
        }
    }
}