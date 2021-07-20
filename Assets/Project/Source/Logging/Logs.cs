using System;
using System.Collections.Generic;
using System.Text;
using Exa.IO;
using Exa.Utils;
using UnityEngine;

namespace Exa.Logging {
    public class Logs : MonoSingleton<Logs> {
        private static List<object> currentStackContext = new List<object>();

        private static void LogImpl(LogType logType, string message) {
            Debug.unityLogger.Log(logType, $"{message} info: {{\n{StringifyContext()}}}");
        }

        public static IDisposable Context(object obj) {
            currentStackContext.Add(obj);

            return new DisposableAction(() => currentStackContext.Remove(obj));
        }

        public static void Log(string message) {
            LogImpl(LogType.Log, message);
        }

        public static void Warn(string message) {
            LogImpl(LogType.Warning, message);
        }

        public static void Error(string message) {
            LogImpl(LogType.Error, message);
        }

        private static string StringifyContext() {
            var builder = new StringBuilder();

            foreach (var ctx in currentStackContext) {
                foreach (var (prop, value) in new PropertyAdapter(ctx).GetNamesAndValues()) {
                    builder.AppendLine($"  {prop}: {value},");
                }
            }
            
            return builder.ToString();
        }
    }
}