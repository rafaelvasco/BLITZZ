using BLITZZ.Native.BGFX;
using System.Collections.Generic;
using System.Numerics;

namespace BLITZZ.Content
{
    public class ShaderParameter
    {
        internal UniformHandle Uniform;

        public bool Constant { get; set; } = false;

        internal bool SubmitedOnce;

        public Vector4 Value => _value;

        private Vector4 _value;


        internal ShaderParameter(string name)
        {
            this.Uniform = Bgfx.CreateUniform(name, UniformType.Vec4, 4);
        }

        public void SetValue(float v)
        {
            _value.X = v;
        }

        public void SetValue(Vector2 v)
        {
            _value.X = v.X;
            _value.Y = v.Y;
        }

        public void SetValue(Vector3 v)
        {
            _value.X = v.X;
            _value.Y = v.Y;
            _value.Z = v.Z;
        }

        public void SetValue(Vector4 v)
        {
            _value = v;
        }

        public void SetValue(Color color)
        {
            _value.X = color.Rf;
            _value.Y = color.Gf;
            _value.Z = color.Bf;
            _value.W = color.Af;
        }
    }

    public class ShaderProgram : Asset
    {
        internal ProgramHandle Program;

        private ShaderParameter[] _parameters;

        private Dictionary<string, int> _parametersMap;

        private readonly Texture[] _textures;

        private int _textureIndex;

        private UniformHandle[] _samplers;

        internal ShaderProgram(ProgramHandle program, IReadOnlyList<string> samplers, IReadOnlyList<string> @params)
        {
            Program = program;

            _textures = new Texture[samplers.Count];

            BuildSamplersList(samplers);

            BuildParametersList(@params);
        }

        internal void SetTexture(int slot, Texture texture)
        {
            slot = Calc.Clamp(slot, 0, 2);

            _textures[slot] = texture;

            if (slot > _textureIndex)
            {
                _textureIndex = slot;
            }
        }

        public ShaderParameter GetParameter(string name)
        {
            return _parametersMap.TryGetValue(name, out var index) ? _parameters[index] : null;
        }

        internal void ApplyTextures()
        {
            if (_textureIndex == 0)
            {
                Bgfx.SetTexture(0, _samplers[0], _textures[0].Handle, _textures[0].SamplerFlags);
            }
            else
            {
                for (int i = 0; i <= _textureIndex; ++i)
                {
                    Bgfx.SetTexture((byte)i, _samplers[i], _textures[i].Handle, _textures[i].SamplerFlags);
                }
            }
        }

        internal void ApplyParameters()
        {
            if (_parameters == null)
            {
                return;
            }

            for (int i = 0; i < _parameters.Length; ++i)
            {
                var p = _parameters[i];

                if (p.Constant)
                {
                    if (p.SubmitedOnce)
                    {
                        continue;
                    }

                    p.SubmitedOnce = true;

                }

                var val = p.Value;

                Bgfx.SetUniform(p.Uniform, ref val);
            }
        }

        private void BuildSamplersList(IReadOnlyList<string> samplers)
        {
            if (samplers == null)
            {
                return;
            }

            _samplers = new UniformHandle[samplers.Count];

            for (int i = 0; i < samplers.Count; ++i)
            {
                _samplers[i] = Bgfx.CreateUniform(samplers[i], UniformType.Sampler, 1);
            }
        }

        private void BuildParametersList(IReadOnlyList<string> @params)
        {
            if (@params == null)
            {
                return;
            }

            _parameters = new ShaderParameter[@params.Count];
            _parametersMap = new Dictionary<string, int>();

            for (int i = 0; i < @params.Count; ++i)
            {
                _parameters[i] = new ShaderParameter(@params[i]);
                _parametersMap.Add(@params[i], i);
            }
        }

        protected override void FreeNativeResources()
        {
            if (!Program.Valid)
            {
                return;
            }

            if (_samplers != null)
            {
                for (int i = 0; i < _samplers.Length; ++i)
                {
                    Bgfx.DestroyUniform(_samplers[i]);
                }
            }

            if (_parameters != null)
            {
                for (int i = 0; i < _parameters.Length; ++i)
                {
                    Bgfx.DestroyUniform(_parameters[i].Uniform);
                }
            }

            Bgfx.DestroyProgram(Program);
        }
    }
}
