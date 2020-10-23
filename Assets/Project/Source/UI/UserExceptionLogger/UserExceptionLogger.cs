using UnityEngine;
using Exa.Misc;

#pragma warning disable CS0649

namespace Exa.UI
{
    public class UserExceptionLogger : MonoBehaviour
    {
        [SerializeField] private GameObject userExceptionPrefab;
        [SerializeField] private Transform container;

        public void Log(UserException exception) {
            Log(exception.Message);
        }

        public void Log(string message) {
            var exceptionView = Instantiate(userExceptionPrefab, container).GetComponent<UserExceptionView>();
            exceptionView.Message = message;
        }
    }
}