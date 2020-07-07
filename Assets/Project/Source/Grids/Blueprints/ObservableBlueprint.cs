using Exa.Bindings;
using Exa.IO;
using Exa.Utils;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(ObservableBlueprintConverter))]
    public class ObservableBlueprint : Observable<Blueprint>, ISerializableItem
    {
        public ObservableBlueprint(Blueprint blueprint)
            : base(blueprint)
        {
            BlueprintFileHandle = new FileHandle(this,
                (name) => IOUtils.CombinePath(RelativeDir.USER_BLUEPRINTS, $"{name}.json"),
                (path) => IOUtils.JsonSerializeToPath(this, path));

            ThumbnailFileHandle = new FileHandle(this,
                (name) => IOUtils.CombinePath(RelativeDir.THUMBNAILS, $"{name}.png"),
                (path) => IOUtils.SaveTexture2D(Data.Thumbnail, path));
        }

        [JsonIgnore] public string ItemName => Data.name;
        [JsonIgnore] public FileHandle BlueprintFileHandle { get; set; }
        [JsonIgnore] public FileHandle ThumbnailFileHandle { get; set; }

        public void LoadThumbnail()
        {
            if (File.Exists(ThumbnailFileHandle.TargetPath))
            {
                Data.Thumbnail = IOUtils.LoadTexture2D(ThumbnailFileHandle.TargetPath, 512, 512);
            }
            else
            {
                UpdateThumbnail();
            }
        }

        public void UpdateThumbnail()
        {
            Data.Thumbnail = GenerateThumbnail();       
            ThumbnailFileHandle.UpdatePath();
        }

        private Texture2D GenerateThumbnail()
        {
            var tex = new Texture2D(512, 512);

            foreach (var vector in MathUtils.EnumerateVectors(512, 512))
            {
                var col = (vector.x % 2 == 0 ^ vector.y % 2 == 0) ? 1 : 0;
                tex.SetPixel(vector.x, vector.y, new Color(col, col, col));
            }

            tex.Apply();

            return tex;
        }
    }
}