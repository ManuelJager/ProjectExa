using Exa.Bindings;
using Exa.Generics;
using Exa.IO;
using Newtonsoft.Json;
using System.IO;

namespace Exa.Grids.Blueprints
{
    [JsonConverter(typeof(ObservableBlueprintConverter))]
    public class ObservableBlueprint : Observable<Blueprint>, ISerializableItem, IKeySelector<string>
    {
        public ObservableBlueprint(Blueprint blueprint)
            : this(blueprint, true)
        {
        }

        public ObservableBlueprint(Blueprint blueprint, bool generateBlueprintFileHandle = true)
            : base(blueprint)
        {
            if (generateBlueprintFileHandle)
            {
                BlueprintFileHandle = new FileHandle(this,
                    (name) => IOUtils.CombinePath(IOUtils.GetPath("blueprints"), $"{name}.json"),
                    (path) => IOUtils.JsonSerializeToPath(Data, path));
            }

            ThumbnailFileHandle = new FileHandle(this,
                (name) => IOUtils.CombinePath(IOUtils.GetPath("thumbnails"), $"{name}.png"),
                (path) => IOUtils.SaveTexture2D(Data.Thumbnail, path));
        }

        [JsonIgnore] public string ItemName => Data.name;
        [JsonIgnore] public FileHandle BlueprintFileHandle { get; set; }
        [JsonIgnore] public FileHandle ThumbnailFileHandle { get; set; }
        [JsonIgnore] public string Key => Data.name;

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
            Data.Thumbnail = Systems.ThumbnailGenerator.GenerateThumbnail(Data);
            ThumbnailFileHandle.UpdatePath();
        }
    }
}