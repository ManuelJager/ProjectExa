using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Exa.IO;
using Exa.Utils;
using Ionic.Zip;
using Newtonsoft.Json;
using UnityEngine;
using CompressionLevel = Ionic.Zlib.CompressionLevel;

namespace Exa.Audio.Music
{
    public class CustomSoundTrackDescription : ISoundTrackDescription
    {
        private CustomSoundTrackMetadata metadata;

        public string Name => metadata.Name;
        public int SongCount => metadata.Songs.Count();

        public CustomSoundTrackDescription(string path) {
            metadata = GetMetadata(path);
        }

        public ISoundTrack GetSoundTrack() {
            return null;
        }

        private CustomSoundTrackMetadata GetMetadata(string path) {
            using var zip = ZipFile.Read(path);
            zip.CompressionLevel = CompressionLevel.BestCompression;
            var configEntry = zip.Entries.FirstOrDefault(entry => entry.FileName == "config.json");

            return configEntry == null 
                ? GenerateMetadata(zip) 
                : null;
        }

        private CustomSoundTrackMetadata GenerateMetadata(ZipFile zip) {
            var name = Path.GetFileNameWithoutExtension(zip.Name);
            var songs = FilterEntries(zip.Entries, ".mp3").Select(entry => new CustomSongMetadata {
                Name = Path.GetFileNameWithoutExtension(entry.FileName),
                FileName = Path.GetFileName(entry.FileName),
                Atmospheres = Atmosphere.All,
            });

            var metadata = new CustomSoundTrackMetadata {
                Name = name,
                Songs = songs.ToList()
            };

            AppendMetadata(zip, metadata);

            return metadata;
        }

        private void AppendMetadata(ZipFile file, CustomSoundTrackMetadata metadata) {
            var json = IOUtils.JsonSerializeWithSettings(metadata, SerializationMode.Readable);
            file.AddEntry("config.json", Encoding.UTF8.GetBytes(json));
            file.Save();
        }

        private IEnumerable<ZipEntry> FilterEntries(IEnumerable<ZipEntry> entries, string extension) {
            return entries.Where(entry => Path.GetExtension(entry.FileName) == extension);
        }
    }
}

