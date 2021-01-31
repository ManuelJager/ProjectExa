using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using UnityEngine;

namespace Exa.IO
{
    public static class ZipUtils
    {
        public static IEnumerable<ZipEntry> Filter(this IEnumerable<ZipEntry> entries, string filter) {
            return entries.Where(entry => Path.GetExtension(entry.FileName) == filter);
        }

        public static IEnumerable<ZipEntry> Filter(this IEnumerable<ZipEntry> entries, IEnumerable<string> filters) {
            return entries.Where(entry => {
                var extension = Path.GetExtension(entry.FileName);
                return filters.Any(filter => extension == filter);
            });
        }

        public static byte[] GetBytes(this ZipEntry entry) {
            using var ms = new MemoryStream();
            entry.Extract(ms);
            return ms.ToArray();
        }

        public static T ReadJson<T>(this ZipEntry entry, SerializationMode mode) {
            var json = Encoding.UTF8.GetString(entry.GetBytes());
            return IOUtils.JsonDeserializeWithSettings<T>(json, mode);
        }

        public static AudioClip ReadAudioClip(this ZipEntry entry) {

        }
    }
}