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
                (name) => IOUtils.CombinePath(IOUtils.GetPath("blueprints"), $"{name}.json"),
                (path) => IOUtils.JsonSerializeToPath(this, path));

            ThumbnailFileHandle = new FileHandle(this,
                (name) => IOUtils.CombinePath(IOUtils.GetPath("thumbnails"), $"{name}.png"),
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
                GenerateThumbnail();
            }
        }

        public void GenerateThumbnail()
        {
            Data.Thumbnail = MainManager.Instance.thumbnailGenerator.GenerateThumbnail(Data);
            ThumbnailFileHandle.UpdatePath();
        }
    }
}