using Exa.Generics;
using Exa.Utils;
using System;
using UnityEngine;

namespace Exa.Debugging
{
    public class UnityLoggerInterceptor : MonoBehaviour, ILogHandler
    {
        private ILogHandler defaultLogHandler;

        public void Awake()
        {
            defaultLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            defaultLogHandler.LogException(exception, context);
            TypeUtils.OnAssignableFrom<UserException>(exception, LogUserException);
        }

        private void LogUserException(UserException exception)
        {
            Debug.Log("Intercepted user exception");
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            defaultLogHandler.LogFormat(logType, context, format, args);
        }
    }
}