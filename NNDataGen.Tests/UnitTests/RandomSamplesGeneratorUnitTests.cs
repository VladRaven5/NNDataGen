using FluentAssertions;
using NNDataGen.Classes;
using NSubstitute;
using NSubstitute.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace NNDataGen.Tests.UnitTests
{
    public class RandomSamplesGeneratorUnitTests
    {
        private Random _random;

        public RandomSamplesGeneratorUnitTests()
        {
            _random = Substitute.For<Random>();
        }

        [Fact]
        public void Generator_IgnoresDuplicates()
        {
            int samplesCount = 2;

            double feature1_1 = 0.25;
            double feature2_1 = 0.35;
            double feature1_2 = 0.52;
            double feature2_2 = 0.3;

            _random.Configure()
                .NextDouble()
                .Returns(
                    feature1_1, feature2_1,
                    feature1_1, feature2_1,
                    feature1_2, feature2_2);

            var generator = new RandomSamplesGenerator(samplesCount,
                new List<BaseClass> { new Class1(), new Class2() },
                _random);

            var samples = generator.GenerateSamples();

            samples.Should().HaveCount(samplesCount);
            samples[0].Feature1.Should().Be(feature1_1);
            samples[0].Feature2.Should().Be(feature2_1);
            samples[0].ClassKey.Should().Be(0);

            samples[1].Feature1.Should().Be(feature1_2);
            samples[1].Feature2.Should().Be(feature2_2);
            samples[1].ClassKey.Should().Be(1);
        }
    }
}
