using Exa.UI;
using System;
using UnityEngine;
using Exa.Misc;
using Exa.Utils;

namespace Exa.Debugging
{
    public class LoggerInterceptor : MonoBehaviour, ILogHandler
    {
        private ILogHandler defaultLogHandler;
        private NotificationLogger notificationLogger;

        private void Awake() {
            notificationLogger = Systems.UI.logger;
            defaultLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }

        public void LogException(Exception exception, UnityEngine.Object context) {
            // Missing reference exceptions when the application is quitting should be ignored
            if (exception is MissingReferenceException && Systems.IsQuitting)
                return;

            if (DebugMode.ConsoleDump.IsEnabled())
                exception.LogToConsole();

            defaultLogHandler.LogException(exception, context);
            if (exception is UserException userException)
                LogUserException(userException);
        }

        [System.Diagnostics.DebuggerHidden]
        public void LogUserException(UserException exception) {
            notificationLogger.LogException(exception);
        }

        [System.Diagnostics.DebuggerHidden]
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args) {
            defaultLogHandler.LogFormat(logType, context, format, args);
        }
    }
}