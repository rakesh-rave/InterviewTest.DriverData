using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
        // BONUS: What's the difference between DateTime and DateTimeOffset?
        //DateTime: DateTime Represents a Date and time of day with limited time zone information
        //DateTimeOffset: DateTimeOffset Represents a point in time, typically expressed as a date and time of day, relative to Coordinated Universal Time (UTC) it provides a greater degree of time zone awareness than the DateTime structure
        public DateTimeOffset Start;
		public DateTimeOffset End;

        // BONUS: What's the difference between decimal and double?
        //Precision is the main difference.
        //Decimals have much higher precision and are usually used within financial applications that require a high degree of accuracy. 
        //Decimals are much slower (up to 20X times in some tests) than a double/float it has 128-bit .
        //The double keyword signifies a simple type that stores 64-bit floating-point values.
        public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}
