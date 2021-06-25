using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BLITZZ.Content;
using CommandLine;

namespace BLITZZ_CLI
{
    [Verb("build", HelpText = "Build Game")]
    class BuildOptions
    {
        [Option(Required = true, HelpText = "Game Folder Path")]
        public string GameFolder { get; set; }
    }

    [Verb("build_engine", HelpText = "Build Engine")]
    class BuildEngineOptions
    {
        [Option(Required = true, HelpText = "Engine Folder Path")]
        public string Path { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<BuildOptions, BuildEngineOptions>(args)
                .MapResult(
                    
                    async (BuildOptions buildOptions) => await ExecuteBuild(buildOptions),
                    async (BuildEngineOptions buildOptions) => await ExecuteBuildEngine(buildOptions),
                    errs => Task.FromResult(false)
                );
        }

        static async Task ExecuteBuild(BuildOptions options)
        {
            var gameConfigPath = Path.Combine(options.GameFolder, Assets.GameConfigJsonFileName);

            if(!File.Exists(gameConfigPath))
            {
                Console.WriteLine("Game is missing configuration file. Exiting.");
                return;
            }


            using FileStream openStream = File.OpenRead(Path.Combine(options.GameFolder, Assets.GameConfigJsonFileName));

            GameInfo gameInfo = await JsonSerializer.DeserializeAsync<GameInfo>(openStream);

            var assetsFolder = Path.Combine(options.GameFolder, gameInfo.AssetsFolder);

            AssetsManifest assetsManifest = AssetLoader.LoadGameAssetsManifest(assetsFolder);

            if (assetsManifest == null)
            {
                Console.WriteLine("Could not find assets manifest file. Exiting.");
                return;
            }

            await Builder.BuildGame(assetsFolder, assetsManifest);
        }

        static async Task ExecuteBuildEngine(BuildEngineOptions options)
        {
            var assetsFolder = Path.Combine(options.Path, "Content", "BaseAssets");

            AssetsManifest assetsManifest = AssetLoader.LoadGameAssetsManifest(assetsFolder);

            if (assetsManifest == null)
            {
                Console.WriteLine("Could not find assets manifest file. Exiting.");
                return;
            }

            await Builder.BuildGame(assetsFolder, assetsManifest);
        }
    }
}
