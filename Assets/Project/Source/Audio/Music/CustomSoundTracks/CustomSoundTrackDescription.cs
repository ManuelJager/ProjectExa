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
        private static readonly IEnumerable<string> SupportedAudioTypes = new [] {".wav"};
        private static readonly string ConfigName = "config.json";

        private CustomSoundTrackMetadata metadata;
        private string path;

        public string Name => metadata.Name;
        public int SongCount => metadata.Songs.Count();

        public CustomSoundTrackDescription(string path) {
            this.path = path;

            metadata = GetMetadata(path);
        }

        public ISoundTrack GetSoundTrack() {
            return null;
        }

        private CustomSoundTrackMetadata GetMetadata(string path) {
            using var zip = ZipFile.Read(path);
            zip.CompressionLevel = CompressionLevel.BestCompression;
            var configEntry = zip.Entries.FirstOrDefault(entry => entry.FileName == ConfigName);

            return configEntry == null 
                ? GenerateMetadata(zip) 
                : configEntry.ReadJson<CustomSoundTrackMetadata>(SerializationMode.Readable);
        }

        private CustomSoundTrackMetadata GenerateMetadata(ZipFile zip) {
            var name = Path.GetFileNameWithoutExtension(zip.Name);
            var songs = zip.Entries.Filter(SupportedAudioTypes).Select(entry => new CustomSongMetadata {
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
            file.AddEntry(ConfigName, Encoding.UTF8.GetBytes(json));
            file.Save();
        }
    }
}

