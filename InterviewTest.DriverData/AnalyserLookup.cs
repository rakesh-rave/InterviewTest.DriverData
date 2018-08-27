using System;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData;
namespace InterviewTest.DriverData
{
    public static class AnalyserLookup
    {
        public static IAnalyser GetAnalyser(string type)
        {
            switch (type)
            {
                case "friendly":
                    return new FriendlyAnalyser();

                case "deliverydriver":
                    {
                        var ratingConfiguration = new RatingConfiguration()
                        {
                            startTime = RatingConfigSettings.StartTime.ToString().ReadConfig().ToTimeSpan(),
                            endTime = RatingConfigSettings.EndTime.ToString().ReadConfig().ToTimeSpan(),
                            maxSpeed = RatingConfigSettings.MaxSpeed.ToString().ReadConfig().ToDecimal()
                        };

                        return new DeliveryDriverAnalyser(ratingConfiguration);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
            }
        }

    }
}
