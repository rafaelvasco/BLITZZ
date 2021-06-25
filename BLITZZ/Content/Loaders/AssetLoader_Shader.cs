using BLITZZ.Native.BGFX;

namespace BLITZZ.Content
{
    public static partial class AssetLoader
    {
        public static ShaderProgram LoadShader(ShaderProgramData shaderData)
        {
            var shader_program =
                Bgfx.CreateShaderProgram(
                    shaderData.VertexShader,
            shaderData.FragmentShader,
                    shaderData.Samplers,
                    shaderData.Params);

            shader_program.Id = shaderData.Id;

            return shader_program;
        }

      
    }
}
