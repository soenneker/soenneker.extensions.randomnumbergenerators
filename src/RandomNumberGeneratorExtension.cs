using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Soenneker.Extensions.RandomNumberGenerators;

/// <summary>
/// Various helpful RandomNumberGenerator extension methods
/// </summary>
public static class RandomNumberGeneratorExtension
{
    /// <summary>
    /// Generates a non-negative random integer that is less than the specified maximum value using the provided random
    /// number generator.
    /// </summary>
    /// <remarks>This method avoids modulo bias by using rejection sampling, ensuring a uniform distribution
    /// of values in the specified range.</remarks>
    /// <param name="rng">The random number generator to use for producing the random integer. Cannot be null.</param>
    /// <param name="exclusiveMax">The exclusive upper bound of the random number to be generated. Must be greater than 0.</param>
    /// <returns>A non-negative integer that is greater than or equal to 0 and less than <paramref name="exclusiveMax"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="rng"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="exclusiveMax"/> is less than or equal to 0.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this RandomNumberGenerator rng, int exclusiveMax)
    {
        if (rng is null)
            throw new ArgumentNullException(nameof(rng));

        if (exclusiveMax <= 0)
            throw new ArgumentOutOfRangeException(nameof(exclusiveMax));

        var max = (uint)exclusiveMax;

        // Rejection sampling to avoid modulo bias
        uint limit = uint.MaxValue / max * max;

        uint value = 0;

        do
        {
            // Create a span pointing to 'value'
            Span<uint> valueSpan = MemoryMarshal.CreateSpan(ref value, 1);

            // Interpret that span as bytes and fill with random data
            Span<byte> bytesSpan = MemoryMarshal.AsBytes(valueSpan);

            rng.GetBytes(bytesSpan);
        }
        while (value >= limit);

        return (int)(value % max);
    }
}
