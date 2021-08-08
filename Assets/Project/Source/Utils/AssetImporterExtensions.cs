using UnityEditor;
using UnityEngine;

namespace Exa.Utils {
    public static class AssetImporterExtensions {
        public static void ApplyDefaultBlockImportSettings(this TextureImporter importer) {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.maxTextureSize = 2048;
            importer.crunchedCompression = false;
            importer.compressionQuality = 100;
            importer.isReadable = true;
            importer.textureShape = TextureImporterShape.Texture2D;
            importer.npotScale = TextureImporterNPOTScale.None;
        }
    }
}