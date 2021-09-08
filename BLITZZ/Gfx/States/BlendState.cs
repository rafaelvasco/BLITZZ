using BLITZZ.Native.BGFX;

namespace BLITZZ.Gfx
{
    public class BlendState
    {
        private StateFlags _srcColor;
        private StateFlags _dstColor;
        private StateFlags _srcAlpha;
        private StateFlags _dstAlpha;

        public Blend ColorSourceBlend
        {

            get => ConvertStateFlagsToBlendEnum(_srcColor);
            set
            {
                _srcColor = ConvertBlendEnumToStateFlags(value);
                UpdateStateFlags();
            }
        }

        public Blend ColorDestinationBlend
        {

            get => ConvertStateFlagsToBlendEnum(_dstColor);
            set
            {
                _dstColor = ConvertBlendEnumToStateFlags(value);
                UpdateStateFlags();
            }
        }

        public Blend AlphaSourceBlend
        {

            get => ConvertStateFlagsToBlendEnum(_srcAlpha);
            set
            {
                _srcAlpha = ConvertBlendEnumToStateFlags(value);
                UpdateStateFlags();
            }
        }

        public Blend AlphaDestinationBlend
        {

            get => ConvertStateFlagsToBlendEnum(_dstAlpha);
            set
            {
                _dstAlpha = ConvertBlendEnumToStateFlags(value);
                UpdateStateFlags();
            }
        }

        internal StateFlags StateFlags { get; private set; }

        
        public static readonly BlendState Alpha;
        public static readonly BlendState AlphaPre;
        public static readonly BlendState Additive;
        public static readonly BlendState Light;
        public static readonly BlendState Multiply;
        public static readonly BlendState Invert;
        public static readonly BlendState Mask;
        public static readonly BlendState Opaque;

        private static Blend ConvertStateFlagsToBlendEnum(StateFlags state)
        {
            return state switch
            {
                StateFlags.BlendZero => Blend.Zero,
                StateFlags.BlendOne => Blend.One,
                StateFlags.BlendSrcColor => Blend.SourceColor,
                StateFlags.BlendInvSrcColor => Blend.InverseSourceColor,
                StateFlags.BlendSrcAlpha => Blend.SourceAlpha,
                StateFlags.BlendInvSrcAlpha => Blend.InverseSourceAlpha,
                StateFlags.BlendDstColor => Blend.DestinationColor,
                StateFlags.BlendInvDstColor => Blend.InverseDestinationColor,
                StateFlags.BlendDstAlpha => Blend.DestinationAlpha,
                StateFlags.BlendInvDstAlpha => Blend.InverseDestinationAlpha,
                StateFlags.BlendFactor => Blend.BlendFactor,
                StateFlags.BlendInvFactor => Blend.InverseBlendFactor,
                StateFlags.BlendSrcAlphaSat => Blend.SourceAlphaSaturation,
                _ => Blend.Zero
            };
        }

        private static StateFlags ConvertBlendEnumToStateFlags(Blend blend)
        {
            return blend switch
            {
                Blend.One => StateFlags.BlendOne,
                Blend.Zero => StateFlags.BlendZero,
                Blend.SourceColor => StateFlags.BlendSrcColor,
                Blend.InverseSourceColor => StateFlags.BlendInvSrcColor,
                Blend.SourceAlpha => StateFlags.BlendSrcAlpha,
                Blend.InverseSourceAlpha => StateFlags.BlendInvSrcAlpha,
                Blend.DestinationColor => StateFlags.BlendDstColor,
                Blend.InverseDestinationColor => StateFlags.BlendInvDstColor,
                Blend.DestinationAlpha => StateFlags.BlendDstAlpha,
                Blend.InverseDestinationAlpha => StateFlags.BlendInvDstAlpha,
                Blend.BlendFactor => StateFlags.BlendFactor,
                Blend.InverseBlendFactor => StateFlags.BlendInvFactor,
                Blend.SourceAlphaSaturation => StateFlags.BlendSrcAlphaSat,
                _ => StateFlags.None
            };
        }

        private BlendState(StateFlags srcColor, StateFlags dstColor, StateFlags srcAlpha, StateFlags dstAlpha)
        {
            _srcColor = srcColor;
            _dstColor = dstColor;
            _srcAlpha = srcAlpha;
            _dstAlpha = dstAlpha;

            UpdateStateFlags();
        }

        static BlendState()
        {
            Alpha = new BlendState(StateFlags.BlendSrcAlpha, StateFlags.BlendInvSrcAlpha, StateFlags.BlendOne, StateFlags.BlendInvSrcAlpha);
            AlphaPre = new BlendState(StateFlags.BlendOne, StateFlags.BlendInvSrcAlpha, StateFlags.BlendOne,
                StateFlags.BlendInvSrcAlpha);
            Mask = new BlendState(StateFlags.BlendAlphaToCoverage, StateFlags.BlendAlphaToCoverage,
                StateFlags.BlendAlphaToCoverage, StateFlags.BlendAlphaToCoverage);
            Additive = new BlendState(StateFlags.BlendSrcAlpha, StateFlags.BlendOne, StateFlags.BlendOne, StateFlags.BlendOne);
            Light = new BlendState(StateFlags.BlendDstColor, StateFlags.BlendOne, StateFlags.BlendZero,
                StateFlags.BlendOne);
            Multiply = new BlendState(StateFlags.BlendDstColor, StateFlags.BlendZero, StateFlags.BlendDstColor,
                StateFlags.BlendZero);
            Invert = new BlendState(StateFlags.BlendInvDstColor, StateFlags.BlendInvSrcColor,
                StateFlags.BlendInvDstColor, StateFlags.BlendInvSrcColor);
            Opaque = new BlendState(StateFlags.None, StateFlags.None, StateFlags.None, StateFlags.None);
            
        }

        private void UpdateStateFlags()
        {
            StateFlags = Bgfx.STATE_BLEND_FUNC_SEPARATE(_srcColor, _dstColor, _srcAlpha, _dstAlpha);
        }
    }
}

