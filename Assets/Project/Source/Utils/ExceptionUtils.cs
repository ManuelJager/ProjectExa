using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class ExceptionUtils
    {
        public static void LogToConsole(this Exception exception)
        {
            try
            {
                Systems.UI.console.output.DumpExceptionLogRecursively(exception);
            }
            catch
            {
                var message = "An exception has occurred while logging an exception to the console (seriously?)";
                Debug.LogError(message);
                Systems.UI.logger.Log(message);
            }
        }
    }
}