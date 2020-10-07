using Exa.Generics;
using Exa.Utils;
using UnityEngine;
using Exa.Misc;

namespace Exa.UI
{
    public class UserExceptionLogger : MonoBehaviour
    {
        [SerializeField] private GameObject _userExceptionPrefab;
        [SerializeField] private Transform _container;

        public void Log(UserException exception)
        {
            Log(exception.Message);
        }

        public void Log(string message)
        {
            var exceptionView = Instantiate(_userExceptionPrefab, _container).GetComponent<UserExceptionView>();
            exceptionView.Message = message;
        }
    }
}