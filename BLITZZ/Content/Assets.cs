using System;
using System.Collections.Generic;

namespace BLITZZ.Content
{
    public static class Assets
    {
        private static Dictionary<string, Asset> _loadedAssets;
        private static Dictionary<string, string[]> _pakMap;
        private static List<DisposableResource> _runtimeResources;

        public const string GameConfigJsonFileName = "game.json";

        public const string AssetsManifestFileName = "assets.json";

        internal static void Initialize(GameInfo info)
        {
            AssetLoader.SetRootPath(info.AssetsFolder);

            _loadedAssets = new Dictionary<string, Asset>();
            _runtimeResources = new List<DisposableResource>();
            _pakMap = new Dictionary<string, string[]>();

            LoadContentPack("base");

            if (info.PreloadPaks != null)
            {
                foreach (var pak_name in info.PreloadPaks)
                {
                    LoadContentPack(pak_name);
                }
            }
        }


        public static T Get<T>(string resourceId) where T : Asset
        {
            if (_loadedAssets.TryGetValue(resourceId, out var resource))
            {
                return (T)resource;
            }

            throw new Exception($"Can't find resource with ID: {resourceId}");
        }
        public static void LoadContentPack(string pakName)
        {
            ResourcePak pak = AssetLoader.LoadPak(pakName);

            if (pak.TotalResourcesCount == 0)
            {
                return;
            }

            int res_name_map_idx = 0;

            _pakMap.Add(pakName, new string[pak.TotalResourcesCount]);

            if (pak.Images != null)
            {
                foreach (var (imageKey, imageData) in pak.Images)
                {
                    Texture texture = AssetLoader.LoadTexture(imageData);
                    _loadedAssets.Add(texture.Id, texture);
                    _pakMap[pakName][res_name_map_idx++] = imageKey;
                }
            }

            if (pak.Atlases != null)
            {
                foreach (var (atlasKey, atlasData) in pak.Atlases)
                {
                    TextureAtlas atlas = AssetLoader.LoadAtlas(atlasData);
                    _loadedAssets.Add(atlas.Id, atlas);
                    _pakMap[pakName][res_name_map_idx++] = atlasKey;
                }
            }

            if (pak.Fonts != null)
            {
                foreach (var (fontKey, fontData) in pak.Fonts)
                {
                    TextureFont font = AssetLoader.LoadFont(fontData);
                    _loadedAssets.Add(font.Id, font);
                    _pakMap[pakName][res_name_map_idx++] = fontKey;
                }
            }

            if (pak.Shaders != null)
            {
                foreach (var (shaderKey, shaderProgramData) in pak.Shaders)
                {
                    ShaderProgram shader = AssetLoader.LoadShader(shaderProgramData);
                    _loadedAssets.Add(shader.Id, shader);
                    _pakMap[pakName][res_name_map_idx++] = shaderKey;
                }
            }

            if (pak.TextFiles != null)
            {
                foreach (var (txtKey, textFileData) in pak.TextFiles)
                {
                    TextFile text_file = AssetLoader.LoadTextFile(textFileData);
                    _loadedAssets.Add(text_file.Id, text_file);
                    _pakMap[pakName][res_name_map_idx++] = txtKey;
                }
            }

            //foreach (var sfx_res in pak.Sfx)
            //{
            //    Effect effect = _loader.LoadEffect(sfx_res.Value);

            //    _loaded_resources.Add(effect.Id, effect);
            //}

            //foreach (var song_res in pak.Songs)
            //{
            //    Song song = _loader.LoadSong(song_res.Value);

            //    _loaded_resources.Add(song.Id, song);
            //}

        }

        internal static void RegisterRuntimeLoaded(DisposableResource resource)
        {
            _runtimeResources.Add(resource);
        }

        public static void FreePack(string packName)
        {
            Console.WriteLine($" > Diposing resources from Pack: {packName}");

            var res_ids = _pakMap[packName];

            for (int i = 0; i < res_ids.Length; ++i)
            {
                var res_id = res_ids[i];
                _loadedAssets[res_id].Dispose();
                _loadedAssets.Remove(res_id);
                Console.WriteLine($" > Disposed resource: {res_id}");
            }
        }


        internal static void FreeEverything()
        {
            Console.WriteLine($" > Diposing {_loadedAssets.Count} loaded resources.");

            foreach (var (resKey, resource) in _loadedAssets)
            {
                Console.WriteLine($" > Diposing {resKey}.");
                resource.Dispose();
            }

            Console.WriteLine($" > Disposing {_runtimeResources.Count} runtime resources.");

            foreach (var resource in _runtimeResources)
            {
                resource.Dispose();
            }

            _loadedAssets.Clear();
            _runtimeResources.Clear();

            _loadedAssets = null;
            _runtimeResources = null;
        }
    }
}
