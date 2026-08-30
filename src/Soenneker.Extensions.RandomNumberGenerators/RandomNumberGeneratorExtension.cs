using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Soenneker.Extensions.RandomNumberGenerators;

/// <summary>
/// Various helpful RandomNumberGenerator extension methods
/// </summary>
public static class RandomNumberGeneratorExtension
{
    /// <summary>
    /// Returns a non-negative random 64-bit integer that is less than the specified maximum value.
    /// </summary>
    /// <remarks>This method avoids modulo bias to ensure a uniform distribution of values in the specified
    /// range.</remarks>
    /// <param name="rng">The random number generator to use for producing the random value. Cannot be null.</param>
    /// <param name="exclusiveMax">The exclusive upper bound of the random number to be generated. Must be greater than 0.</param>
    /// <returns>A 64-bit integer greater than or equal to 0 and less than <paramref name="exclusiveMax"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="exclusiveMax"/> is less than or equal to 0.</exception>
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this RandomNumberGenerator rng, long exclusiveMax)
    {
        ArgumentNullException.ThrowIfNull(rng);

        if (exclusiveMax <= 0)
            throw new ArgumentOutOfRangeException(nameof(exclusiveMax));

        return (long)GetUInt64(rng, (ulong)exclusiveMax);
    }

    private static ulong GetUInt64(RandomNumberGenerator rng, ulong exclusiveMax)
    {

        // Rejection threshold to avoid modulo bias
        // Equivalent to: (2^64 % max)
        ulong rejectThreshold = unchecked(0UL - exclusiveMax) % exclusiveMax;

        while (true)
        {
            ulong value = rng.GetUInt64();
            if (value >= rejectThreshold)
                return value % exclusiveMax;
        }
    }

    /// <summary>
    /// Generates a random 64-bit signed integer using the specified random number generator.
    /// </summary>
    /// <param name="rng">The random number generator to use for producing the random value. Cannot be null.</param>
    /// <returns>A randomly generated 64-bit signed integer.</returns>
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this RandomNumberGenerator rng)
    => unchecked((long)rng.GetUInt64());

    /// <summary>
    /// Returns a random 64-bit signed integer that is greater than or equal to the specified minimum value and less
    /// than the specified maximum value.
    /// </summary>
    /// <param name="rng">The random number generator to use for producing the random value. Cannot be null.</param>
    /// <param name="min">The inclusive lower bound of the random number to be generated.</param>
    /// <param name="max">The exclusive upper bound of the random number to be generated. Must be greater than <paramref name="min"/>.</param>
    /// <returns>A 64-bit signed integer greater than or equal to <paramref name="min"/> and less than <paramref name="max"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than or equal to <paramref name="max"/>.</exception>
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this RandomNumberGenerator rng, long min, long max)
    {
        if (min >= max)
            throw new ArgumentOutOfRangeException();

        ulong width = unchecked((ulong)(max - min));
        ulong offset = GetUInt64(rng, width);

        return unchecked((long)((ulong)min + offset));
    }

    /// <summary>
    /// Generates a random 64-bit unsigned integer using the specified random number generator.
    /// </summary>
    /// <remarks>This method fills an 8-byte buffer with random data from the provided random number generator
    /// and interprets it as a 64-bit unsigned integer. The distribution of returned values is uniform across the entire
    /// range of UInt64. This method does not modify the state of the random number generator beyond advancing its
    /// internal state as required to produce random bytes.</remarks>
    /// <param name="rng">The random number generator to use for producing the random value. Cannot be null.</param>
    /// <returns>A random 64-bit unsigned integer sampled from the full range of possible values.</returns>
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this RandomNumberGenerator rng)
    {
        Span<byte> bytes = stackalloc byte[8];
        rng.GetBytes(bytes);

        return
              (ulong)bytes[0]
            | ((ulong)bytes[1] << 8)
            | ((ulong)bytes[2] << 16)
            | ((ulong)bytes[3] << 24)
            | ((ulong)bytes[4] << 32)
            | ((ulong)bytes[5] << 40)
            | ((ulong)bytes[6] << 48)
            | ((ulong)bytes[7] << 56);
    }
}
