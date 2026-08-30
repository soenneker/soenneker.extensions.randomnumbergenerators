[![](https://img.shields.io/nuget/v/soenneker.extensions.randomnumbergenerators.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.randomnumbergenerators/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.randomnumbergenerators/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.randomnumbergenerators/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.randomnumbergenerators.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.randomnumbergenerators/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.randomnumbergenerators/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.randomnumbergenerators/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.RandomNumberGenerators
Uniform 64-bit signed, unsigned, and bounded sampling from a cryptographic `RandomNumberGenerator`.

## Installation

```bash
dotnet add package Soenneker.Extensions.RandomNumberGenerators
```

## Sample a bounded signed integer

```csharp
using Soenneker.Extensions.RandomNumberGenerators;

using RandomNumberGenerator rng = RandomNumberGenerator.Create();

long index = rng.GetInt64(exclusiveMax: 1_000); // 0 through 999
long signed = rng.GetInt64(-500, 500);           // -500 through 499
```

Both bounded overloads use rejection sampling rather than `%` alone, so every value in the requested interval has the same probability.

- `GetInt64(exclusiveMax)` returns a value in `[0, exclusiveMax)` and requires a positive maximum.
- `GetInt64(min, max)` returns a value in `[min, max)` and supports intervals spanning the negative and positive halves of the complete signed range. It requires `min < max`.

Invalid bounds throw `ArgumentOutOfRangeException`. The generator must be non-null.

## Sample the full 64-bit domain

```csharp
long anySigned = rng.GetInt64();
ulong anyUnsigned = rng.GetUInt64();
```

The parameterless methods sample uniformly across all bit patterns of `Int64` or `UInt64`, respectively. The signed result can be negative. `GetUInt64()` fills eight bytes and interprets them in little-endian order; byte order does not affect uniformity.

Randomness quality and disposal behavior come from the supplied `RandomNumberGenerator`. These methods are suitable for cryptographic sampling when the supplied generator is suitable, but converting a random value to text does not by itself provide enough entropy for every token format—choose the range/bit count according to the security requirement.
