using BLITZZ.Content;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BLITZZ.Content.Font;

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

                    AssetLoader.SetRootFolder(assetsFolder);

                    List<AssetPak> resource_paks =
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

        private static List<AssetPak> BuildAssets(AssetsManifest manifest)
        {
            var resource_groups = manifest.Assets;

            var results = new List<AssetPak>();

            foreach (var (groupKey, group) in resource_groups)
            {
                var pak = new AssetPak(groupKey);

                Console.WriteLine($"Creating Asset Pak: {pak.Name}");

                if (group.Images != null)
                {
                    foreach (var imageAssetInfo in group.Images)
                    {
                        Console.WriteLine($"Building Image: {imageAssetInfo.Id}");

                        var pixmap_data = ImageBuilder.Build(imageAssetInfo.Id, imageAssetInfo.Path);

                        pak.Images.Add(imageAssetInfo.Id, pixmap_data);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }
                }

                if (group.Shaders != null)
                {
                    foreach (var shaderAssetInfo in group.Shaders)
                    {
                        Console.WriteLine($"Building Shader: {shaderAssetInfo.Id}");

                        var shader_data = ShaderBuilder.Build(shaderAssetInfo.Id, shaderAssetInfo.VsPath, shaderAssetInfo.FsPath);

                        pak.Shaders.Add(shaderAssetInfo.Id, shader_data);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }
                }

                if (group.Fonts != null)
                {
                    foreach (var fontAssetInfo in group.Fonts)
                    {
                        Console.WriteLine($"Building Font: {fontAssetInfo.Id}");

                        var buildProps = new FontBuildProps()
                        {
                            Size = fontAssetInfo.Size,
                            UseHinting = fontAssetInfo.UseHinting,
                            UseAutoHinting = fontAssetInfo.UseAutoHinting,
                            KerningMode = fontAssetInfo.KerningMode,
                            HintMode = fontAssetInfo.HintMode,
                            LineSpace = fontAssetInfo.LineSpace
                        };

                        if (fontAssetInfo.CharRanges is {Length: > 0})
                        {
                            buildProps.CharRanges = new CharRange[fontAssetInfo.CharRanges.Length];

                            for (int i = 0; i < fontAssetInfo.CharRanges.Length; ++i)
                            {
                                buildProps.CharRanges[i] = CharRange.GetFromKey(fontAssetInfo.CharRanges[i]);
                            }
                        }

                        var fontData = TrueTypeFontBuilder.Build(fontAssetInfo.Id, fontAssetInfo.Path, buildProps);

                        pak.Fonts.Add(fontAssetInfo.Id, fontData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }

                }

                if (group.BitmapFonts != null)
                {
                    foreach (var bitmapFontAssetInfo in group.BitmapFonts)
                    {
                        Console.WriteLine($"Building BitmapFont: {bitmapFontAssetInfo.Id}");

                        var bitmapFontData = BitmapFontBuilder.Build(bitmapFontAssetInfo.Id, bitmapFontAssetInfo.Path);

                        pak.BitmapFonts.Add(bitmapFontAssetInfo.Id, bitmapFontData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }
                }

                if (group.Atlases != null)
                {
                    foreach (var atlasAssetInfo in group.Atlases)
                    {
                        Console.WriteLine($"Building Atlas: {atlasAssetInfo.Id}");

                        var atlas_data = AtlasBuilder.Build(atlasAssetInfo.Id, atlasAssetInfo.Path, atlasAssetInfo.Regions);

                        pak.Atlases.Add(atlas_data.Id, atlas_data);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }
                }

                if (group.TextFiles != null)
                {
                    foreach (var textFileAssetInfo in group.TextFiles)
                    {
                        Console.WriteLine($"Building TextFile: {textFileAssetInfo.Id}");

                        var textFileData = TextBuilder.Build(textFileAssetInfo.Id, textFileAssetInfo.Path);
                        pak.TextFiles.Add(textFileAssetInfo.Id, textFileData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }
                }

                if (group.AudioFiles != null)
                {
                    foreach (var audioFileAssetInfo in group.AudioFiles)
                    {
                        Console.WriteLine($"Building Audio File: {audioFileAssetInfo.Id}");

                        var audioFileData = AudioFileBuilder.Build(audioFileAssetInfo.Id, audioFileAssetInfo.Path, audioFileAssetInfo.Streamed);

                        pak.AudioFiles.Add(audioFileAssetInfo.Id, audioFileData);

                        pak.TotalResourcesCount++;

                        Console.WriteLine("Done.");
                    }
                }

                results.Add(pak);

                Console.WriteLine($"PAK {groupKey} built successfully with {pak.TotalResourcesCount} assets.");
            }

            return results;
        }
    }
}
