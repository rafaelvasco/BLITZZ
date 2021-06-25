
using FreeTypeSharp.Native;

namespace BLITZZ.Content.Font
{
    public enum KerningMode
    {
        Default = FT_Kerning_Mode.FT_KERNING_DEFAULT,
        Unfitted = FT_Kerning_Mode.FT_KERNING_UNFITTED,
        Unscaled = FT_Kerning_Mode.FT_KERNING_UNSCALED
    }
}