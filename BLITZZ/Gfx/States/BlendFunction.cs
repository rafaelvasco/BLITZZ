﻿namespace BLITZZ.Gfx
{
    /// <summary>
    /// Defines a function for color blending.
    /// </summary>
    public enum BlendFunction
    {
        /// <summary>
        /// The function will adds destination to the source. (srcColor * srcBlend) + (destColor * destBlend)
        /// </summary>
        Add,
        /// <summary>
        /// The function will subtracts destination from source. (srcColor * srcBlend) − (destColor * destBlend)
        /// </summary>
        Subtract,
        /// <summary>
        /// The function will subtracts source from destination. (destColor * destBlend) - (srcColor * srcBlend) 
        /// </summary>
        ReverseSubtract,
        /// <summary>
        /// The function will extracts minimum of the source and destination. min((srcColor * srcBlend),(destColor * destBlend))
        /// </summary>
        Min, 
        /// <summary>
        /// The function will extracts maximum of the source and destination. max((srcColor * srcBlend),(destColor * destBlend))
        /// </summary>
        Max
    }
}
