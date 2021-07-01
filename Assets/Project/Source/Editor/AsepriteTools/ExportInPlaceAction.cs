using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Exa.Utils;
using UnityEditor;

namespace Exa.CustomEditors {
    [Serializable]
    public class ExportInPlaceAction : IEditorAction {
        private string output;

        public void Render() {
            EditorGUILayout.HelpBox($"Execution output: {output}", MessageType.Info);
        }

        public void Execute(string asepritePath, string filePath) {
            var process = GetProcess(asepritePath, $"-b -list-layers \"{filePath}\"");
            process.Start();
            output = "";

            var layers = GetLayers(process.StandardOutput);

            output = layers.Join(" | ");

            // TODO: Do the rest
        }

        private IEnumerable<string> GetLayers(StreamReader reader) {
            while (!reader.EndOfStream) {
                yield return reader.ReadLine();
            }
        }

        private Process GetProcess(string asepritePath, string parameters) {
            return new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = asepritePath,
                    Arguments = parameters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
        }
    }
}