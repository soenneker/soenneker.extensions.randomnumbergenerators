[![](https://img.shields.io/nuget/v/soenneker.extensions.randomnumbergenerators.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.randomnumbergenerators/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.randomnumbergenerators/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.randomnumbergenerators/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.randomnumbergenerators.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.randomnumbergenerators/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.randomnumbergenerators/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.randomnumbergenerators/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.RandomNumberGenerators
Various helpful RandomNumberGenerator extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.RandomNumberGenerators
```

## Quick start

```csharp
using Soenneker.Extensions.RandomNumberGenerators;

// Given an existing RandomNumberGenerator named rng:
var result = rng.GetInt64(exclusiveMax);
```

## Common operations

- `GetInt64()` - Returns a non-negative random 64-bit integer that is less than the specified maximum value.
- `GetUInt64()` - Generates a random 64-bit unsigned integer using the specified random number generator. Returns a random 64-bit unsigned integer sampled from the full range of possible values.
