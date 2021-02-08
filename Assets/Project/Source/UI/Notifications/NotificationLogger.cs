using UnityEngine;
using Exa.Misc;
using Exa.Utils;

namespace Exa.UI
{
    public class NotificationLogger : MonoBehaviour
    {
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private Transform container;

        public void NotifyNowPlaying(string name) {
            notificationPrefab.Create<NotificationView>(container).Setup("Now playing", name);
        }
        
        public void LogException(UserException exception) {
            LogException(exception.Message, true);
        }

        public void LogException(string message, bool unhandled = true) {
            var header = unhandled ? "Unhandled exception" : "Error";
            notificationPrefab.Create<NotificationView>(container).Setup(header, message);
        }
    }
}