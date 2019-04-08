using FluentAssertions;
using NNDataGen.Classes;
using NNDataGen.Samples;
using NSubstitute;
using NSubstitute.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NNDataGen.Tests.UnitTests
{
    public class RandomGeneratedSampleUnitTests
    {

        private Random _randomSubstitute;
        private readonly Class1 _class1;
        private readonly Class2 _class2;
        private List<BaseClass> _classes;

        public RandomGeneratedSampleUnitTests()
        {
            _randomSubstitute = Substitute.For<Random>();
            _class1 = new Class1();
            _class2 = new Class2();

            _classes = new List<BaseClass> { _class1, _class2 };
        }

        [InlineData(0.0001, 0.001, 0)]
        [InlineData(0.45, 0.001, 0)]
        [InlineData(0.0001, 0.45, 0)]
        [InlineData(0.45, 0.45, 0)]
        [InlineData(0.55, 0.55, 0)]
        [InlineData(0.99, 0.55, 0)]
        [InlineData(0.55, 0.99, 0)]
        [InlineData(0.99, 0.99, 0)]

        [InlineData(0.0001, 0.99, 1)]
        [InlineData(0.0001, 0.55, 1)]
        [InlineData(0.45, 0.99, 1)]
        [InlineData(0.45, 0.55, 1)]
        [InlineData(0.99, 0.001, 1)]
        [InlineData(0.99, 0.45, 1)]
        [InlineData(0.55, 0.45, 1)]
        [InlineData(0.55, 0.001, 1)]
        [Theory]
        public void Sample_CorrectSelectClass(double feature1, double feature2, int classKey)
        {
            _randomSubstitute.Configure().NextDouble().Returns(feature1, feature2);
            var sample = new RandomGeneratedSample(_randomSubstitute);

            //check classes was not changed
            var satisfiedClass = _classes.First(c => c.ClassKey == classKey);
            satisfiedClass.IsValueSatisfied(sample).Should().BeTrue();

            sample.SetSatisfiedClass(_classes);
            sample.ClassKey.Should().Be(classKey);
        }
    }
}
