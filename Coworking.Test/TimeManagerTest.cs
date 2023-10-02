namespace Coworking.Test;

[UsesVerify]
public class TimeManagerTest
{
    [Theory]
    [MemberData(nameof(IntersectTimePeriodTestData))]
    public async Task TestIntersectTimePeriod(int testNumber, int count, List<TimePeriod> response)
    {
        var result = TimeManager.IntersectTimePeriods(response, count);
        await Verify(result.Select(period => period.ToString())).UseParameters(testNumber);
    }

    public static IEnumerable<object[]> IntersectTimePeriodTestData()
    {
        yield return new object[]
        {
            0,
            2,
            new List<TimePeriod>()
            {
                new TimePeriod(new TimeOnly(9,30), new TimeOnly(11, 0)),
                new TimePeriod(new TimeOnly(10,00), new TimeOnly(12, 0)),
            }
        };
        
        yield return new object[]
        {
            1,
            1,
            new List<TimePeriod>()
            {
                new TimePeriod(new TimeOnly(9,30), new TimeOnly(11, 0)),
                new TimePeriod(new TimeOnly(12,00), new TimeOnly(13, 0)),
            }
        };
        yield return new object[]
        {
            2,
            3,
            new List<TimePeriod>()
            {
                new TimePeriod(new TimeOnly(9, 30), new TimeOnly(11, 0)),
                new TimePeriod(new TimeOnly(12, 00), new TimeOnly(13, 0)),
            }
        };
    }

    [Theory]
    [MemberData(nameof(ReverseIntersectTimePeriodTestData))]
    public async Task TestReverseIntersectTimePeriod(int testNumber, int count, TimeOnly startTime, TimeOnly endTime,
        List<TimePeriod> response)
    {
        var result = TimeManager.ReverseIntersectTimePeriods(response, startTime, endTime, count);
        await Verify(result.Select(res => res.ToString())).UseParameters(testNumber);
    }
    
    public static IEnumerable<object[]> ReverseIntersectTimePeriodTestData()
    {
        yield return new object[]
        {
            0,
            1,
            new TimeOnly(10,0),
            new TimeOnly(13,0),
            new List<TimePeriod>()
            {
                new TimePeriod(new TimeOnly(11, 0), new TimeOnly(12, 0))
            }
        };
        
        yield return new object[]
        {
            1,
            2,
            new TimeOnly(10,0),
            new TimeOnly(15,0),
            new List<TimePeriod>()
            {
                new TimePeriod(new TimeOnly(11, 0), new TimeOnly(13, 0)),
                new TimePeriod(new TimeOnly(12, 0), new TimeOnly(14, 0)),
            }
        };
    }

    [Theory]
    [MemberData(nameof(GetSimplePeriodsTestData))]
    public async Task TestGetSimplePeriods(int testNumber, List<(int reservation, TimePeriod period)> response)
    {
        var result = TimeManager.GetSimplePeriods(response);
        await Verify(result.Select(x => $"{x.period.ToString()}: {string.Join(" ", x.reservations)}"))
            .UseParameters(testNumber);
    }

    public static IEnumerable<object[]> GetSimplePeriodsTestData()
    {
        yield return new object[]
        { 0,
            new List<(int reservation, TimePeriod period)>()
            {
                (0, new TimePeriod(new TimeOnly(10, 0), new TimeOnly(12, 0))),
                (1, new TimePeriod(new TimeOnly(11, 0), new TimeOnly(13, 0))),
            }
        };
        yield return new object[]
        { 1,
            new List<(int reservation, TimePeriod period)>()
            {
                (0, new TimePeriod(new TimeOnly(10, 0), new TimeOnly(16, 0))),
                (1, new TimePeriod(new TimeOnly(11, 0), new TimeOnly(12, 0))),
                (2, new TimePeriod(new TimeOnly(11, 0), new TimeOnly(12, 0))),
            }
        };
    }
}