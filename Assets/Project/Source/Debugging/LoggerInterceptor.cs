using Exa.Generics;
using Exa.UI;
using Exa.Utils;
using System;
using UnityEngine;

namespace Exa.Debugging
{
    public class LoggerInterceptor : MonoBehaviour, ILogHandler
    {
        [SerializeField] private UserExceptionLogger userExceptionLogger;
        private ILogHandler defaultLogHandler;

        private void Awake()
        {
            defaultLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            // Missing reference exceptions when the application is quitting should be ignored
            if (exception is MissingReferenceException && MainManager.IsQuitting)
            {
                return;
            }

            defaultLogHandler.LogException(exception, context);
            exception.OnAssignableFrom<UserException>(LogUserException);
        }

        public void LogUserException(UserException exception)
        {
            userExceptionLogger.Log(exception);
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            defaultLogHandler.LogFormat(logType, context, format, args);
        }
    }
}