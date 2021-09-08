using System;
using System.Collections.Generic;
using System.Numerics;
using BLITZZ.Content;
using BLITZZ.Gfx;
using BLITZZ.Native.BGFX;
using ImGuiNET;

namespace BLITZZ.Gui
{
    internal class ImGuiRenderer
    {
        private Texture _fontAtlas;

        private readonly Dictionary<string, ImFontPtr> _fonts = new();
        private readonly Dictionary<IntPtr, Texture> _textures = new();

        private ShaderProgram _imguiProgram;
        private ShaderProgram _imguiTextureProgram;

        private IntPtr _imguiContext;

        private readonly ushort _viewId;

        private VertexLayout _vertexLayout;

        public ImGuiRenderer(ushort viewId)
        {
            _viewId = viewId;

            _imguiContext = ImGui.CreateContext();

            var io = ImGui.GetIO();

            _imguiProgram = Assets.Get<ShaderProgram>("imgui_program");
            _imguiTextureProgram = Assets.Get<ShaderProgram>("imgui_image_program");

            _vertexLayout = new VertexLayout();

            _vertexLayout
                .Begin()
                .Add(Attrib.Position, 2, AttribType.Float)
                .Add(Attrib.TexCoord0, 2, AttribType.Float)
                .Add(Attrib.Color0, 4, AttribType.Uint8, normalized: true)
                .End();

            _fonts.Add("default", io.Fonts.AddFontDefault());

            io.Fonts.GetTexDataAsRGBA32(out IntPtr data, out var width, out var height, out var bytesPerPixel);

            var pixmap = new Pixmap(data, width, height, bytesPerPixel);

            _fontAtlas = Texture.Create(pixmap);

            io.Fonts.SetTexID((IntPtr) _fontAtlas.GetHashCode());
        }

        public void StartFrame()
        {
            ImGui.NewFrame();
        }

        public void EndFrame(float elapsedInSeconds, Vector2 resolution)
        {
            var io = ImGui.GetIO();

            io.DeltaTime = elapsedInSeconds;
            io.DisplaySize = resolution;

            ImGui.EndFrame();
            ImGui.Render();

            RenderGui(ImGui.GetDrawData());
        }

        private void RenderGui(ImDrawDataPtr drawData)
        {

        }

    }
}
