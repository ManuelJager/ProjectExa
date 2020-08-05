using Exa.Bindings;
using Exa.Generics;
using Exa.IO;
using Newtonsoft.Json;
using System.IO;

namespace Exa.Grids.Blueprints
{
    public class BlueprintContainer : Observable<Blueprint>, ISerializableItem, IKeySelector<string>
    {
        [JsonIgnore] public string ItemName => Data.name;
        [JsonIgnore] public FileHandle BlueprintFileHandle { get; set; }
        [JsonIgnore] public FileHandle ThumbnailFileHandle { get; set; }
        [JsonIgnore] public string Key => Data.name;

        public BlueprintContainer(Blueprint blueprint, bool generateBlueprintFileHandle = true, bool generateBlueprintFileName = true)
            : base(blueprint)
        {
            if (generateBlueprintFileHandle)
            {
                BlueprintFileHandle = new FileHandle(this,
                    (name) => IOUtils.CombineWithDirectory("blueprints", $"{name}.json"),
                    (path) => IOUtils.JsonSerializeToPath(Data, path),
                    generateBlueprintFileName);
            }

            ThumbnailFileHandle = new FileHandle(this,
                (name) => IOUtils.CombineWithDirectory("thumbnails", $"{name}.png"),
                (path) => IOUtils.SaveTexture2D(Data.Thumbnail, path));
        }

        public void LoadThumbnail()
        {
            if (File.Exists(ThumbnailFileHandle.TargetPath))
            {
                Data.Thumbnail = IOUtils.LoadTexture2D(ThumbnailFileHandle.TargetPath, 512, 512);
            }
            else
            {
                Systems.ThumbnailGenerator.GenerateThumbnail(Data);
                ThumbnailFileHandle.Refresh();
            }
        }
    }
}