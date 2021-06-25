
using System;

namespace BLITZZ.Content
{
    public static class ShaderBuilder
    {
        public static ShaderProgramData Build(string id, string relativeVsPath, string relativeFsPath)
        {
            Console.WriteLine($"Compiling Shader: {id}");

            var result = ShaderCompiler.Compile(AssetLoader.GetFullResourcePath(relativeVsPath), AssetLoader.GetFullResourcePath(relativeFsPath));

            var shader_program_data = new ShaderProgramData()
            {
                Id = id,
                VertexShader = result.VsBytes,
                FragmentShader = result.FsBytes,
                Samplers = result.Samplers,
                Params = result.Params
            };

            return shader_program_data;
        }
    }
}
