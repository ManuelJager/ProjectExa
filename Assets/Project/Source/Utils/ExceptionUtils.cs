using System;
using UnityEngine;

namespace Exa.Utils
{
    public static class ExceptionUtils
    {
        public static void LogToConsole(this Exception exception) {
            try {
                var output = Systems.UI.Console.output;
                output.BeginPrint("An uncaught exception was thrown");
                output.DumpExceptionLogRecursively(exception);
                output.EndPrint();
            }
            catch (Exception consoleException) {
                var message = "An exception has occurred while logging an exception to the console (seriously?)";
                Debug.LogError(new Exception(message, consoleException));
                Systems.UI.Logger.LogException(message);
            }
        }
    }
}