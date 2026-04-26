using DatesAndStuff.Helpers;
using FluentAssertions;

namespace DatesAndStuff.Tests
{
    public sealed class SimulationTimeTests
    {
        [OneTimeSetUp]
        public void OneTimeSetupStuff()
        {
            // Setup code that runs once before all tests, if needed.
        }

        [SetUp]
        public void Setup()
        {
            // Setup code that runs before each test, if needed.
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup code that runs after each test, if needed.
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Cleanup code that runs once after all tests, if needed.
        }

        // Default time is not current time.
        public class SimulationTime_ConstructorTests
        {
            [Test]
            public void Construction_ShouldNotSetCurrentTime()
            {
                // Arrange
                var defaultTime = new SimulationTime();

                // Act
                var now = SimulationTime.Now;

                // Assert
                defaultTime.Should().NotBe(now);
            }
        }

        // equal
        // not equal
        // <
        // >
        // <= different
        // >= different 
        // <= same
        // >= same
        // max
        // min
        public class SimulationTime_OperatorTests
        {
            private readonly SimulationTime _earlier = new SimulationTime(2024, 6, 15, 10, 30, 0);
            private readonly SimulationTime _later = new SimulationTime(2024, 6, 15, 12, 30, 0);

            [Test]
            public void GivenTwoEqualTimes_WhenCompared_ThenAreEqual()
            {
                var a = new SimulationTime(2024, 6, 15, 10, 30, 0);
                var b = new SimulationTime(2024, 6, 15, 10, 30, 0);

                a.Should().Be(b);
            }

            [Test]
            public void GivenTwoDifferentTimes_WhenCompared_ThenAreNotEqual()
            {
                _earlier.Should().NotBe(_later);
            }

            [Test]
            public void GivenEarlierAndLaterTime_WhenLessThanCompared_ThenEarlierIsLess()
            {
                _earlier.Should().BeLessThan(_later);
            }

            [Test]
            public void GivenEarlierAndLaterTime_WhenGreaterThanCompared_ThenLaterIsGreater()
            {
                _later.Should().BeGreaterThan(_earlier);
            }

            [Test]
            public void GivenTwoDifferentTimes_WhenLessThanOrEqualCompared_ThenEarlierIsLessOrEqual()
            {
                _earlier.Should().BeLessThanOrEqualTo(_later);
            }

            [Test]
            public void GivenTwoDifferentTimes_WhenGreaterThanOrEqualCompared_ThenLaterIsGreaterOrEqual()
            {
                _later.Should().BeGreaterThanOrEqualTo(_earlier);
            }

            [Test]
            public void GivenTwoEqualTimes_WhenLessThanOrEqualCompared_ThenAreEqual()
            {
                var a = new SimulationTime(2024, 6, 15, 10, 30, 0);
                var b = new SimulationTime(2024, 6, 15, 10, 30, 0);

                a.Should().BeLessThanOrEqualTo(b);
            }

            [Test]
            public void GivenTwoEqualTimes_WhenGreaterThanOrEqualCompared_ThenAreEqual()
            {
                var a = new SimulationTime(2024, 6, 15, 10, 30, 0);
                var b = new SimulationTime(2024, 6, 15, 10, 30, 0);

                a.Should().BeGreaterThanOrEqualTo(b);
            }

            [Test]
            public void GivenTwoTimes_WhenMaxCalled_ThenReturnsLater()
            {
                var result = SimulationTime.Max(_earlier, _later);

                _later.Should().Be(result);
            }

            [Test]
            public void GivenMultipleTimes_WhenMinCalled_ThenReturnsEarliest()
            {
                var middle = new SimulationTime(2024, 6, 15, 11, 30, 0);
                var result = SimulationTime.Min(_earlier, _later, middle);

                _earlier.Should().Be(result);
            }
        }

        private class SimulationTime_TimeSpanArithmeticTests
        {

            [Test]
            // TimeSpanArithmetic
            // add
            // substract
            // Given_When_Then
            public void GivenSimulationTime_WhenAddingTimeSpan_ThenTimeIsShifted()
            {
                // UserSignedIn_OrderSent_OrderIsRegistered
                // DBB, specflow, cucumber, gherkin

                // Arrange
                DateTime baseDate = new DateTime(2010, 8, 23, 9, 4, 49);
                SimulationTime sut = new SimulationTime(baseDate);

                var ts = TimeSpan.FromMilliseconds(4544313);

                // Act
                var result = sut + ts;

                // Assert
                var expectedDateTime = baseDate + ts;
                expectedDateTime.Should().Be(result.ToAbsoluteDateTime());
            }

            [Test]
            //Method_Should_Then
            public void Subtraction_ShouldShiftSimulationTime_WhenTimeSpanSubtracted()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 8, 30, 0);
                var simTime = new SimulationTime(2024, 6, 15, 10, 30, 0);
                var timeSpan = TimeSpan.FromHours(2);

                // Act
                var result = simTime - timeSpan;

                // Assert
                result.ToAbsoluteDateTime().Should().Be(dateTime);
            }
        }


        public class SimulationTime_TimeShiftTests
        {
            [Test]
            // Simulation difference timespane and datetimetimespan is the same
            public void GivenTwoSimulationTimes_WhenSubtracted_ThenResultEqualsDateTimeDifference()
            {
                // Arrange
                var dateTimeA = new DateTime(2024, 6, 15, 10, 30, 0);
                var dateTimeB = new DateTime(2024, 6, 14, 8, 15, 0);

                var simTimeA = new SimulationTime(dateTimeA);
                var simTimeB = new SimulationTime(dateTimeB);

                var dateTimeDifference = dateTimeA - dateTimeB;
                
                // Act
                var simDifference = simTimeA - simTimeB;

                // Assert
                simDifference.Should().Be(dateTimeDifference);
            }

            [Test]
            // Millisecond representation works
            public void GivenSimulationTime_WhenAddingMilliseconds_TimeIsShiftedInMilliseconds()
            {
                // Arrange
                var simTime = new SimulationTime();
                var originalMilliseconds = simTime.TotalMilliseconds;

                // Act
                var result = simTime.AddMilliseconds(10);

                // Assert
                result.TotalMilliseconds.Should().Be(originalMilliseconds + 10);
            }

            [Test]
            // Next millisec calculation works
            public void NextMillisec_ShouldAddOneMillisecond()
            {
                // Arrange
                var simTime = new SimulationTime();

                // Act
                var simTimeNextMillisec = simTime.NextMillisec;

                // Assert
                simTimeNextMillisec.TotalMilliseconds.Should().Be(simTime.TotalMilliseconds + 1);
            }

            [Test]
            // Create a SimulationTime from a DateTime, add the same milliseconds to both and check if they are still equal
            public void GivenSimulationTime_WhenAddedMilliseconds_ThenTimeIsShiftedLikeInDatetime()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 10, 30, 0);
                var simTime = new SimulationTime(dateTime);

                var resultDateTime = dateTime.AddMilliseconds(10);
                
                // Act
                var resultsSimTime = simTime.AddMilliseconds(10);

                // Assert
                resultsSimTime.ToAbsoluteDateTime().Should().Be(resultDateTime);
            }

            [Test]
            // The same as before just with seconds
            public void GivenSimulationTime_WhenAddedSeconds_ThenTimeIsShiftedLikeInDatetime()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 10, 30, 0);
                var simTime = new SimulationTime(dateTime);

                var resultDateTime = dateTime.AddSeconds(10);
                
                // Act
                var resultsSimTime = simTime.AddSeconds(10);

                // Assert
                resultsSimTime.ToAbsoluteDateTime().Should().Be(resultDateTime);
            }

            [Test]
            // Same as before just with timespan
            public void GivenSimulationTime_WhenAddedTimespan_ThenTimeIsShiftedLikeInDatetime()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 10, 30, 0);
                var simTime = new SimulationTime(dateTime);
                var timeSpan = new TimeSpan(0, 0, 10, 10);

                var resultDateTime = dateTime + timeSpan;
                
                // Act
                var resultsSimTime = simTime.AddTimeSpan(timeSpan);

                // Assert
                resultsSimTime.ToAbsoluteDateTime().Should().Be(resultDateTime);
            }
            
            [Test]
            // Same as before just with days
            public void GivenSimulationTime_WhenAddedDays_ThenTimeIsShiftedLikeInDatetime()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 10, 30, 0);
                var simTime = new SimulationTime(dateTime);

                var resultDateTime = dateTime.AddDays(2);
                
                // Act
                var resultsSimTime = simTime.AddDays(2);

                // Assert
                resultsSimTime.ToAbsoluteDateTime().Should().Be(resultDateTime);
            }
            
            [Test]
            // Same as before just with days
            public void GivenSimulationTime_WhenAddedHours_ThenTimeIsShiftedLikeInDatetime()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 10, 30, 0);
                var simTime = new SimulationTime(dateTime);

                var resultDateTime = dateTime.AddHours(2);
                
                // Act
                var resultsSimTime = simTime.AddHours(2);

                // Assert
                resultsSimTime.ToAbsoluteDateTime().Should().Be(resultDateTime);
            }

            [Test]
            public void LastMidnight_ShouldShiftTimeBackToLastMidnight()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 0,0,0);
                var simTime = new SimulationTime(2024, 6, 15, 10, 30, 0);

                // Act
                var result = simTime.LastMidnight();

                // Assert
                result.ToAbsoluteDateTime().Should().Be(dateTime);
            }
        }

        public class SimulationTime_RepresentationTests
        {
            [Test]
            public void GivenSimulationTime_WhenConvertedToDateOnly_ThenShouldNotHaveTime()
            {
                // Arrange
                var dateTime = new DateOnly(2024, 6, 15);
                var simTime = new SimulationTime(2024, 6, 15, 10, 30, 0);

                // Act
                var result = simTime.ToAbsoluteDateOnly();

                // Assert
                result.Should().Be(dateTime);
            }
            
        }

        public class SimulationTime_StringTests
        {
            [Test]
            // Check string representation given by ToString
            public void ToString_ShouldConvertToString()
            {
                // Arrange
                var dateTime = new DateTime(2024, 6, 15, 10, 30, 0);
                var simTime = new SimulationTime(dateTime);

                // Act
                var result = simTime.ToString();

                // Assert
                result.Should().Be(dateTime.ToIsoStringFast());
            }
        }
    }
}