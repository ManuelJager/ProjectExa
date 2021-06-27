using System.IO;
using Exa.IO;
using Exa.Types.Binding;
using Exa.Types.Generics;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints {
    public class BlueprintContainer : Observable<Blueprint>, ISerializableItem, IKeySelector<string> {
        public BlueprintContainer(BlueprintContainerArgs args)
            : base(args.blueprint) {
            if (args.generateBlueprintFileHandle) {
                BlueprintFileHandle = new FileHandle(
                    this,
                    name => Tree.Root.Blueprints.CombineWith($"{name}.json"),
                    path => IOUtils.JsonSerializeToPath(Data, path),
                    args.generateBlueprintFileName
                );
            }

            var thumbnailDirectory = args.useDefaultThumbnailFolder
                ? Tree.Root.DefaultThumbnails
                : Tree.Root.Thumbnails;

            ThumbnailFileHandle = new FileHandle(
                this,
                name => thumbnailDirectory.CombineWith($"{name}.png"),
                path => IOUtils.SaveTexture2D(Data.Thumbnail, path)
            );
        }

        [JsonIgnore] public FileHandle BlueprintFileHandle { get; set; }
        [JsonIgnore] public FileHandle ThumbnailFileHandle { get; set; }

        [JsonIgnore] public string Key {
            get => Data.name;
        }

        [JsonIgnore] public string ItemName {
            get => Data.name;
        }

        public override void SetData(Blueprint data, bool notify = true) {
            if (notify) {
                Systems.Thumbnails.GenerateThumbnail(data);
                ThumbnailFileHandle.Refresh();
                BlueprintFileHandle.Refresh();
            }

            base.SetData(data, notify);
        }

        public void LoadThumbnail() {
            if (File.Exists(ThumbnailFileHandle.TargetPath)) {
                Data.Thumbnail = IOUtils.LoadTexture2D(ThumbnailFileHandle.TargetPath, 512, 512);
            } else {
                Systems.Thumbnails.GenerateThumbnail(Data);
                ThumbnailFileHandle.Refresh();
            }
        }
    }
}