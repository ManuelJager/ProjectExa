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

        public BlueprintContainer(BlueprintContainerArgs args)
            : base(args.blueprint)
        {
            if (args.generateBlueprintFileHandle)
            {
                BlueprintFileHandle = new FileHandle(this,
                    (name) => IoUtils.CombineWithDirectory("blueprints", $"{name}.json"),
                    (path) => IoUtils.JsonSerializeToPath(Data, path),
                    args.generateBlueprintFileName);
            }

            var thumbnailDirectory = args.useDefaultThumbnailFolder ? "defaultThumbnails" : "thumbnails";
            ThumbnailFileHandle = new FileHandle(this,
                (name) => IoUtils.CombineWithDirectory(thumbnailDirectory, $"{name}.png"),
                (path) => IoUtils.SaveTexture2D(Data.Thumbnail, path));
        }

        public void LoadThumbnail()
        {
            if (File.Exists(ThumbnailFileHandle.TargetPath))
            {
                Data.Thumbnail = IoUtils.LoadTexture2D(ThumbnailFileHandle.TargetPath, 512, 512);
            }
            else
            {
                Systems.Thumbnails.GenerateThumbnail(Data);
                ThumbnailFileHandle.Refresh();
            }
        }
    }
}