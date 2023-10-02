namespace Coworking.Test;

[UsesVerify]
public class MatchmakerTest
{
    [Theory]
    [MemberData(nameof(MakePairsTestData))]
    public async Task TestMakePairs(int testNumber, int maxn, List<int>[] resToWp)
    {
        var result = Matchmaker.MakePairs(resToWp, maxn);
        await Verify(result).UseParameters(testNumber);
    }
    
    public static IEnumerable<object[]> MakePairsTestData()
    {
        yield return new object[]
        {0,
            3,
            new List<int>[]
            {
                new List<int>() { 0, 2 },
                new List<int>(),
                new List<int>(),
            },
        };
        yield return new object[]
        {1,
            3,
            new List<int>[]
            {
                new List<int>() { 0, 2 },
                new List<int>() { 0, 1 },
                new List<int>(),
            },
        };
        yield return new object[]
        {2,
            3,
            new List<int>[]
            {
                new List<int>() { 0, 2 },
                new List<int>() { 0, 1 },
                new List<int>() { 0, 1 },
            },
        };
        yield return new object[]
        {3,
            3,
            new List<int>[]
            {
                new List<int>() { 0, 2 },
                new List<int>() { 1 },
                new List<int>() { 1 },
            },
        };
    }
}