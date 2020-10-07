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
        private ILogHandler _defaultLogHandler;
        private UserExceptionLogger _userExceptionLogger;

        private void Awake()
        {
            _userExceptionLogger = Systems.Ui.logger;
            _defaultLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            // Missing reference exceptions when the application is quitting should be ignored
            if (exception is MissingReferenceException && Systems.IsQuitting)
            {
                return;
            }

            _defaultLogHandler.LogException(exception, context);
            if (exception is UserException userException)
            {
                LogUserException(userException);
            }
        }

        [System.Diagnostics.DebuggerHidden]
        public void LogUserException(UserException exception)
        {
            _userExceptionLogger.Log(exception);
        }

        [System.Diagnostics.DebuggerHidden]
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            _defaultLogHandler.LogFormat(logType, context, format, args);
        }
    }
}