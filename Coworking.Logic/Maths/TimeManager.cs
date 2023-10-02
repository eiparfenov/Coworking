namespace Coworking.Logic.Maths;

public static class TimeManager
{
    public static List<TimePeriod> ReverseIntersectTimePeriods(
        List<TimePeriod> timePeriods,
        TimeOnly startTime, 
        TimeOnly endTime,
        int count = 1
        )
    {
        var result = new List<TimePeriod>(){new TimePeriod(startTime,default)};
        var timeEvents = timePeriods
            .SelectMany(period => new TimeEvent[]
            {
                new TimeEvent(period.StartTime!.Value, -1),
                new TimeEvent(period.EndTime!.Value, 1)
            })
            .OrderBy(timeEvent => timeEvent.Time)
            .ToList();

        foreach (var timeEvent in timeEvents)
        {
            var prevCount = count;
            count += timeEvent.Score;

            if (count == 0 && prevCount > 0)
            {
                result.Last().EndTime = timeEvent.Time;
            }
            else if (prevCount == 0 && count > 0)
            {
                result.Add(new TimePeriod(timeEvent.Time, default));
            }
        }

        if (result.Any())
        {
            result.Last().EndTime ??= endTime;
        }
        
        return result;
    }

    public static List<TimePeriod> IntersectTimePeriods(List<TimePeriod> periods, int count)
    {
        var result = new List<TimePeriod>();
        var timeEvents = periods
            .SelectMany(period => new TimeEvent[]
            {
                new TimeEvent(period.StartTime!.Value, -1),
                new TimeEvent(period.EndTime!.Value, 1)
            })
            .OrderBy(timeEvent => timeEvent.Time)
            .ToList();

        foreach (var timeEvent in timeEvents)
        {
            var prevCount = count;
            count += timeEvent.Score;
            
            if (prevCount == 0 && count > 0)
            {
                result.Last().EndTime = timeEvent.Time;
            }
            else if (count == 0 && prevCount > 0)
            {
                result.Add(new TimePeriod(timeEvent.Time, default));
            }
        }

        return result;
    }

    public static List<(TimePeriod period, int[] reservations)> GetSimplePeriods(List<(int reservation, TimePeriod period)> reservations)
    {
        var timeEvents = reservations
            .SelectMany(reservation => new[]
            {
                new ReservationTimeEvent()
                {
                    Time = reservation.period.StartTime!.Value,
                    ReservationId = reservation.reservation,
                    IsStart = true
                },
                new ReservationTimeEvent()
                {
                    Time = reservation.period.EndTime!.Value,
                    ReservationId = reservation.reservation,
                    IsStart = false
                },
            })
            .OrderBy(timeEvent => timeEvent.Time)
            .ToList();

        var currentReservations = new List<int>();
        var result = new List<(TimePeriod period, int[] reservations)>();
        TimeOnly? prevTime = null;
        foreach (var timeEvent in timeEvents)
        {
            if (prevTime.HasValue)
            {
                result.Add((new TimePeriod(prevTime, timeEvent.Time), currentReservations.ToArray()));
            }
            prevTime = timeEvent.Time;
            
            if (timeEvent.IsStart)
            {
                currentReservations.Add(timeEvent.ReservationId);
            }
            else
            {
                currentReservations.Remove(timeEvent.ReservationId);
            }
        }

        result = result.Where(x => x.period.StartTime!.Value != x.period.EndTime!.Value).ToList();
        
        return result;
    }
}

public class TimeEvent
{
    public TimeOnly Time { get; set; }
    public int Score { get; set; }

    public TimeEvent(TimeOnly time, int score)
    {
        Time = time;
        Score = score;
    }
}

public class ReservationTimeEvent
{
    public TimeOnly Time { get; set; }
    public int ReservationId { get; set; }
    public bool IsStart { get; set; }
}

public class TimePeriod
{
    public TimeOnly? StartTime { get; set; } 
    public TimeOnly? EndTime { get; set; }
    

    public TimePeriod(TimeOnly? startTime, TimeOnly? endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public bool Contains(TimePeriod other) => StartTime <= other.StartTime && other.EndTime <= EndTime;

    public bool Intersects(TimePeriod other) => other.StartTime < StartTime && StartTime < other.EndTime ||
                                                other.StartTime < EndTime && EndTime < other.EndTime;

    public override string ToString()
    {
        return $"{StartTime} - {EndTime}";
    }
}

public class NotEnoughItemsException: Exception
{}