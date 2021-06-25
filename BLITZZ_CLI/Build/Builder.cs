using BLITZZ.Content;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BLITZZ_CLI
{
    public static class Builder
    {
        private static int VerifyManifestValid(AssetsManifest manifest)
        {
            if (manifest.Assets == null || manifest.Assets.Count == 0)
            {
                return 0;
            }

            int total = 0;

            foreach (var assetGroup in manifest.Assets)
            {
                total += assetGroup.Value.AssetsCount();
            }

            return total;
        }

        public static async Task BuildGame(string assetsFolder, AssetsManifest manifest)
        {
            int totalAssets = VerifyManifestValid(manifest);

            if (totalAssets == 0)
            {
                Console.WriteLine("Nothing to build. No assets registered on manifest.");
                return;
            }

            await Task.Run(() => {

                try
                {
                    Console.WriteLine($"Building a total of {totalAssets} asssets into {manifest.Assets.Count} asset paks:");

                    AssetLoader.SetRootPath(assetsFolder);

                    List<ResourcePak> resource_paks =
                        BuildAssets(manifest);

                    foreach (var pak in resource_paks)
                    {
                        using var pak_file = File.Create(Path.Combine(assetsFolder, pak.Name + ".pak"));

                        MessagePackSerializer.Serialize(pak_file, pak);
                    }

                    if (resource_paks.Count > 0)
                    {
                        Console.WriteLine("Assets built successfully! Bye.");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while building game assets: {e.Message}");
                    
                }
                
            });
        }

        //private static void BuildAppConfigFile(string root_path, AppProject project)
        //{
        //    GameProperties props = new GameProperties()
        //    {
        //        Title = project.Title,
        //        FrameRate = project.FrameRate,
        //        CanvasWidth = project.CanvasWidth,
        //        CanvasHeight = project.CanvasHeight,
        //        Fullscreen = project.StartFullscreen,
        //        PreloadResourcePaks = project.PreloadPaks
        //    };

        //    File.WriteAllBytes(Path.Combine(root_path, "Config.json"), 
        //        JsonSerializer.PrettyPrintByteArray(JsonSerializer.Serialize(props)));

        //}

        private static List<ResourcePak> BuildAssets(AssetsManifest manifest)
        {
            var resource_groups = manifest.Assets;

            var results = new List<ResourcePak>();

            foreach (var (groupKey, group) in resource_groups)
            {
                var pak = new ResourcePak(groupKey);

                Console.WriteLine($"Creating Asset Pak: {pak.Name}");

                if (group.Images != null)
                {
                    pak.Images = new Dictionary<string, ImageData>();

                    foreach (var imageAssetInfo in group.Images)
                    {
                        var pixmap_data = ImageBuilder.Build(imageAssetInfo.Id, imageAssetInfo.Path);

                        pak.Images.Add(imageAssetInfo.Id, pixmap_data);

                        pak.TotalResourcesCount++;

                        Console.WriteLine($"Added Image: {pixmap_data.Id}");
                    }
                }

                if (group.Shaders != null)
                {
                    pak.Shaders = new Dictionary<string, ShaderProgramData>();

                    foreach (var shaderAssetInfo in group.Shaders)
                    {
                        var shader_data = ShaderBuilder.Build(shaderAssetInfo.Id, shaderAssetInfo.VsPath, shaderAssetInfo.FsPath);

                        pak.Shaders.Add(shaderAssetInfo.Id, shader_data);

                        pak.TotalResourcesCount++;

                        Console.WriteLine($"Added Shader: {shader_data.Id}");
                    }
                }

                if (group.Fonts != null)
                {
                    //foreach (var font_info in group.Fonts)
                    //{
                    //    var build_params = new FontBuildParams()
                    //    {
                    //        Id = font_info.Id,
                    //        LineSpacing = font_info.LineSpacing,
                    //        Spacing = font_info.Spacing,
                    //        DefaultChar = font_info.DefaultChar,
                    //        Faces = font_info.Faces.Select(f => new FontFace()
                    //        {
                    //            CharRanges = f.CharRanges.Select(CharRange.GetFromKey).ToList(),
                    //            Path = f.Path,
                    //            Size = f.Size,
                    //        }).ToList()
                    //    };

                    //    var font_data = FontBuilder.Build(build_params);

                    //    pak.Fonts.Add(font_info.Id, font_data);

                    //    pak.TotalResourcesCount++;

                    //    Console.WriteLine($"Added Font: {font_data.Id}");

                    //}
                }

                if (group.Atlases != null)
                {
                    pak.Atlases = new Dictionary<string, TextureAtlasData>();

                    foreach (var atlasAssetInfo in group.Atlases)
                    {
                        var atlas_data = AtlasBuilder.Build(atlasAssetInfo.Id, atlasAssetInfo.Path, atlasAssetInfo.Regions);

                        pak.Atlases.Add(atlas_data.Id, atlas_data);

                        pak.TotalResourcesCount++;

                        Console.WriteLine($"Added Atlas: {atlas_data.Id}");
                    }
                }

                if (group.TextFiles != null)
                {
                    pak.TextFiles = new Dictionary<string, TextFileData>();

                    foreach (var textFileAssetInfo in group.TextFiles)
                    {
                        var textFileData = TextBuilder.Build(textFileAssetInfo.Id, textFileAssetInfo.Path);
                        pak.TextFiles.Add(textFileAssetInfo.Id, textFileData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine($"Added TextFile: {textFileData.Id}");
                    }
                }

                if (group.AudioFiles != null)
                {
                    pak.AudioFiles = new Dictionary<string, AudioFileData>();

                    foreach (var audioFileAssetInfo in group.AudioFiles)
                    {
                        var audioFileData = AudioFileBuilder.Build(audioFileAssetInfo.Id, audioFileAssetInfo.Path, audioFileAssetInfo.Streamed);

                        pak.AudioFiles.Add(audioFileAssetInfo.Id, audioFileData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine($"Added Audio File : {audioFileData.Id}");
                    }
                }

                results.Add(pak);

                Console.WriteLine($"PAK {groupKey} with {pak.TotalResourcesCount} assets.");
            }

            return results;
        }
    }
}
