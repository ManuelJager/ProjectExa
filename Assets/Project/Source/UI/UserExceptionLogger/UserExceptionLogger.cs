using Exa.Generics;
using UnityEngine;

namespace Exa.UI
{
    public class UserExceptionLogger : MonoBehaviour
    {
        [SerializeField] private GameObject userExceptionPrefab;

        public void Log(UserException exception)
        {
            var exceptionView = Instantiate(userExceptionPrefab, transform).GetComponent<UserExceptionView>();
            exceptionView.Message = exception.Message;
        }
    }
}