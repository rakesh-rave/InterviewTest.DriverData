using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class DeliveryDriverAnalyserTests
	{
        private RatingConfiguration ratingConfiguration;
        [SetUp]
        public void Initialize()
        {
            ratingConfiguration = new RatingConfiguration
            {
                startTime = new TimeSpan(9, 0, 0),
                endTime = new TimeSpan(17, 0, 0),
                maxSpeed = 30m
            };
        }

        [Test]
        public void ShouldYieldCorrectValues()
        {
            //arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            //act
            var actualResult = new DeliveryDriverAnalyser(ratingConfiguration).Analyse(CannedDrivingData.History);

            //assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }


        [Test]
        public void NoHistoryPeriods_ThrowsArgumentNullException()
        {
            //arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };
            //act
            //assert
            Assert.Throws(typeof(ArgumentNullException), delegate { new DeliveryDriverAnalyser(ratingConfiguration).Analyse(CannedDrivingData.EmptyHistory); });
        }



        [Test]
        public void HistoryPeriodWithOutsideAnalyzerRange_ReturnsZero()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };
            //Act
            var actualResult = new DeliveryDriverAnalyser(ratingConfiguration).Analyse(CannedDrivingData.PeriodHistoryOutsideRange);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void HistoryPeriodExceedingLimit_ReturnsZero()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new DeliveryDriverAnalyser(ratingConfiguration).Analyse(CannedDrivingData.PeriodHistoryExceedingLimit);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
