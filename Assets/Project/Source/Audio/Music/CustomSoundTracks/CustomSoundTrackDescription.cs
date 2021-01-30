using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;

namespace Exa.Audio.Music
{
    public class CustomSoundTrackDescription : ISoundTrackDescription
    {
        public string Name { get; }
        public int SongCount { get; }

        public ISoundTrack GetSoundTrack() {
            return null;
        }

        public CustomSoundTrackDescription(string path) {
            var entries = GetZipEntries(path, out var archive);
            foreach (var entry in entries) {
                Debug.Log(entry.Name);
            }

            Name = Path.GetFileNameWithoutExtension(path);
        }

        private IEnumerable<ZipEntry> GetZipEntries(string path, out ZipFile archive) {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            archive = new ZipFile(fileStream);
            return archive.Cast<ZipEntry>()
                .Where(entry => entry.IsFile);
        }

        private IEnumerable<ZipEntry> FilterEntries(IEnumerable<ZipEntry> entries, string extension) {
            return entries.Where(entry => Path.GetExtension(entry.Name) == extension);
        }
    }
}

