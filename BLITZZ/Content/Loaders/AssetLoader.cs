﻿
using System.IO;
using System.Text.Json;


namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        private static string _rootFolder;

        public static string GetFullResourcePath(string relativeResPath)
        {
            string full_path = Path.Combine(_rootFolder, relativeResPath);

            if (GamePlatform.RunningPlatform == PlatformName.Windows)
            {
                full_path = full_path.Replace('\\', '/');
            }

            return full_path;
        }

        public static void SetRootFolder(string path)
        {
            _rootFolder = path;
        }

        /// <summary>
        /// Used by Game to Load Game Assets Pak
        /// </summary>
        /// <param name="pak_name"></param>
        /// <returns></returns>
        public static AssetPak LoadPak(string pakName)
        {
            var path = Path.Combine(_rootFolder,
                !pakName.Contains(".pak") ? pakName + ".pak" : pakName);

            return AssetPakLoader.Load(path);
        }

        /// <summary>
        /// Used by Game to Load Game Properties
        /// </summary>
        /// <returns>GameInfo</returns>
        public static GameInfo LoadGameInfo()
        {
            var gameInfoFileString = File.ReadAllText(Assets.GameConfigJsonFileName);

            GameInfo gameInfoFile = JsonSerializer.Deserialize<GameInfo>(gameInfoFileString);

            return gameInfoFile;
        }

        /// <summary>
        /// User by Assets Builder
        /// </summary>
        /// <param name="resources_folder"></param>
        /// <returns>GameAssetsManifest</returns>
        public static AssetsManifest LoadGameAssetsManifest(string resourcesFolder)
        {
            try
            {
                var assetsManifestText = File.ReadAllText(Path.Combine(resourcesFolder, Assets.AssetsManifestFileName));

                AssetsManifest assetsManifest = JsonSerializer.Deserialize<AssetsManifest>(assetsManifestText);

                return assetsManifest;
            }
            catch (System.Exception)
            {
            }

            return null;
           
        }
    }
}
