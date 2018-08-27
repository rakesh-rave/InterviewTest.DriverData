using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class CannedDrivingData
	{
		private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

        // BONUS: What's so great about IReadOnlyCollections?
        //ReadOnlyCollection is an immutable collection which can read the underlying collection but cannot modify it i.e. no objects can be added or removed from the collection.

        public static readonly IReadOnlyCollection<Period> History = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 54, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(8, 54, 0),
                End = _day + new TimeSpan(9, 28, 0),
                AverageSpeed = 28m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 28, 0),
                End = _day + new TimeSpan(9, 35, 0),
                AverageSpeed = 33m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 50, 0),
                End = _day + new TimeSpan(12, 35, 0),
                AverageSpeed = 25m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 35, 0),
                End = _day + new TimeSpan(13, 30, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 30, 0),
                End = _day + new TimeSpan(19, 12, 0),
                AverageSpeed = 29m
            },
            new Period
            {
                Start = _day + new TimeSpan(19, 12, 0),
                End = _day + new TimeSpan(24, 0, 0),
                AverageSpeed = 0m
            }
        };
        public static readonly IReadOnlyCollection<Period> EmptyHistory = new Period[] { };

        public static readonly IReadOnlyCollection<Period> PeriodHistoryOutsideRange = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 54, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(19, 12, 0),
                End = _day + new TimeSpan(24, 0, 0),
                AverageSpeed = 0m
            }
        };
        public static readonly IReadOnlyCollection<Period> PeriodHistoryExceedingLimit = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 50, 0),
                AverageSpeed = 39m
            },
            new Period
            {
                Start = _day + new TimeSpan(8, 50, 0),
                End = _day + new TimeSpan(9, 28, 0),
                AverageSpeed = 34m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 28, 0),
                End = _day + new TimeSpan(9, 35, 0),
                AverageSpeed = 31m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 50, 0),
                End = _day + new TimeSpan(12, 35, 0),
                AverageSpeed = 31m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 35, 0),
                End = _day + new TimeSpan(13, 30, 0),
                AverageSpeed = 35m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 30, 0),
                End = _day + new TimeSpan(19, 12, 0),
                AverageSpeed = 36m
            },
            new Period
            {
                Start = _day + new TimeSpan(19, 12, 0),
                End = _day + new TimeSpan(24, 0, 0),
                AverageSpeed = 38m
            }
        };

    }
}
