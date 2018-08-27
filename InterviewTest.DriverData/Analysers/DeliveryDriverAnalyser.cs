using InterviewTest.DriverData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{

    // BONUS: Why internal?
    // Internal class members are those members which only accessed in the class and in a same assembly.
    //In our case, DeliveryDriverAnalyser is internal as it means we don’t want that code modify outside the assembly, able to access it,     
    //since  class is closed for modification interface IAnalyser is kept public which can be extended.

    internal class DeliveryDriverAnalyser : IAnalyser
    {
        #region Initialization(RatingConfiguration),Contructor(DeliveryDriverAnalyser)
        private readonly RatingConfiguration _ratingConfiguration;
        public DeliveryDriverAnalyser(RatingConfiguration ratingConfiguration)
        {
            _ratingConfiguration = ratingConfiguration;
        }
        #endregion

        #region Public Methods {Analyse}
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
            HistoryAnalysis historyAnalysis = new HistoryAnalysis();
            if (history.IsNull<Period>(Constants.HistoryNoDataMessage))
            {
                IEnumerable<Period> validPeriods = history.Where(p => CheckAnalyzerRange(p.Start) || CheckAnalyzerRange(p.End)).ToList();
                IEnumerable<PeriodAnalysis> unDocumentPeriods = GetUndocumentedPeriods(history);
                IEnumerable<PeriodAnalysis> analyzedPeriods = AnalyzeDuration(validPeriods);
                historyAnalysis.AnalysedDuration = new TimeSpan(analyzedPeriods.Select(x => (long)x.Duration).Sum());
                historyAnalysis.DriverRating = CalculateRating(analyzedPeriods, unDocumentPeriods);
            }
            return historyAnalysis;
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Get List of Un Documented Periods
        /// </summary>
        /// <param name="documentedPeriods"></param>
        /// <returns></returns>
        private IEnumerable<PeriodAnalysis> GetUndocumentedPeriods(IEnumerable<Period> documentedPeriods)
        {
            var undocumentedPeriods = new List<PeriodAnalysis>();

            var firstDocumented = documentedPeriods.FirstOrDefault();

            if (firstDocumented == null)
            {
                return undocumentedPeriods;
            }

            DateTimeOffset prevEndTime = firstDocumented.Start;
            foreach (Period period in documentedPeriods)
            {
                if (prevEndTime != period.Start)
                {
                    undocumentedPeriods.Add(new PeriodAnalysis
                    {  // Find difference between previous end time and current start time to get unDocumented Duration
                        Duration = period.Start.Subtract(prevEndTime).Ticks,
                        Rating = 0
                    });
                }
                prevEndTime = period.End;
            }
            return undocumentedPeriods;
        }

        /// <summary>
        /// Anayze durations based on Criteria Task 1.1.md
        /// </summary>
        /// <param name="documentedPeriods"></param>
        /// <param name="ratingConfiguration"></param>
        /// <returns></returns>

        private IEnumerable<PeriodAnalysis> AnalyzeDuration(IEnumerable<Period> documentedPeriods)
        {
            var analysedPeriods = documentedPeriods.Select(period => new PeriodAnalysis
            {
                Duration = GetEndTime(period, (_ratingConfiguration.endTime)).Subtract(GetStartTime(period, (_ratingConfiguration.startTime))).Ticks,
                // Rate linearly if speed is between zero and analyser max speed, otherwise evaluate to zero.
                Rating = (period.AverageSpeed > 0 && period.AverageSpeed < _ratingConfiguration.maxSpeed) ? (period.AverageSpeed) / (_ratingConfiguration.maxSpeed) : 0
            });
            return analysedPeriods;
        }

        /// <summary>
        /// Get StartTime 
        /// </summary>
        /// <param name="period"></param>
        /// <param name="analyserStartTime"></param>
        /// <returns></returns>

        private TimeSpan GetStartTime(Period period, TimeSpan analyserStartTime)
        {
            // Set period start time as analyser start time if period start time is not in the analyser time range.;
            return (period.Start.TimeOfDay < analyserStartTime) ? analyserStartTime : period.Start.TimeOfDay;
        }

        /// <summary>
        /// Get End Time
        /// </summary>
        /// <param name="period"></param>
        /// <param name="analyserEndTime"></param>
        /// <returns></returns>

        private TimeSpan GetEndTime(Period period, TimeSpan analyserEndTime)
        {
            // Set period end time as analyser end time if period end time is not in the analyser time range.
            return (period.End.TimeOfDay > analyserEndTime) ? analyserEndTime : period.End.TimeOfDay;
        }

        /// <summary>
        /// Checks value within specified range
        /// </summary>
        /// <param name="datetimeoffset"></param>
        /// <param name="ratingConfiguration"></param>
        /// <returns></returns>

        private bool CheckAnalyzerRange(DateTimeOffset datetimeoffset)
        {
            return ((datetimeoffset.TimeOfDay > _ratingConfiguration.startTime) && (datetimeoffset.TimeOfDay < _ratingConfiguration.endTime));
        }

        /// <summary>
        /// Function to Calculate rating
        /// </summary>
        /// <param name="analyzedPeriods"></param>
        /// <param name="undocumentedPeriods"></param>
        /// <returns></returns>

        private decimal CalculateRating(IEnumerable<PeriodAnalysis> analyzedPeriods, IEnumerable<PeriodAnalysis> undocumentedPeriods)
        {
            // Calculate the weighted sum by taking total of products of durations and ratings
            var weightedSum = analyzedPeriods.Select(x => x.Duration * x.Rating).Sum();

            // Calculate total duration including undocumented periods
            var totalDuration = analyzedPeriods.Sum(x => x.Duration) + undocumentedPeriods.Sum(x => x.Duration);

            //Calculate overall rating by dividing the weighted sum by total duration including undocumented periods.
            return totalDuration == 0 ? 0 : (weightedSum / totalDuration);
        }

        #endregion
    }
}