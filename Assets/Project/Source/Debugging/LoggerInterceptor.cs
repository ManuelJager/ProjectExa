using Exa.Generics;
using Exa.UI;
using Exa.Utils;
using System;
using UnityEngine;
using Exa.Misc;

namespace Exa.Debugging
{
    public class LoggerInterceptor : MonoBehaviour, ILogHandler
    {
        private ILogHandler defaultLogHandler;
        private UserExceptionLogger userExceptionLogger;

        private void Awake()
        {
            userExceptionLogger = Systems.MainUI.userExceptionLogger;
            defaultLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            // Missing reference exceptions when the application is quitting should be ignored
            if (exception is MissingReferenceException && Systems.IsQuitting)
            {
                return;
            }

            defaultLogHandler.LogException(exception, context);
            if (exception is UserException)
            {
                LogUserException(exception as UserException);
            }
        }

        [System.Diagnostics.DebuggerHidden]
        public void LogUserException(UserException exception)
        {
            userExceptionLogger.Log(exception);
        }

        [System.Diagnostics.DebuggerHidden]
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            defaultLogHandler.LogFormat(logType, context, format, args);
        }
    }
}