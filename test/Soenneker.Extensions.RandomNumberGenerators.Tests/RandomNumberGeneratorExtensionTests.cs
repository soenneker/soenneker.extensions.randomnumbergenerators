using System.Security.Cryptography;
using Soenneker.Tests.Unit;
using System.Threading;

namespace Soenneker.Extensions.RandomNumberGenerators.Tests;

public sealed class RandomNumberGeneratorExtensionTests : UnitTest
{
    [Test]
    public async System.Threading.Tasks.ValueTask Supports_ranges_wider_than_Int64_MaxValue(CancellationToken cancellationToken)
    {
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();

        for (var i = 0; i < 100; i++)
        {
            long value = rng.GetInt64(long.MinValue, long.MaxValue);
            await Assert.That(value).IsGreaterThanOrEqualTo(long.MinValue);
            await Assert.That(value).IsLessThan(long.MaxValue);
        }
    }
}
