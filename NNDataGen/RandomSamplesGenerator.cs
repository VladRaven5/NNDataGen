using NNDataGen.Classes;
using NNDataGen.Samples;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NNDataGen
{
    public class RandomSamplesGenerator
    {
        private readonly int _samplesCount;
        private readonly IEnumerable<BaseClass> _classes;
        private readonly Random _random;

        public RandomSamplesGenerator(int samplesCount,
            IEnumerable<BaseClass> classes, Random random)
        {
            _samplesCount = samplesCount;
            _classes = classes;
            _random = random;
        }

        public List<RandomGeneratedSample> GenerateSamples()
        {
            var uniqueSamplesDict = new Dictionary<(double, double), RandomGeneratedSample>();
            for (int i = 0; i < _samplesCount;)
            {
                var sample = new RandomGeneratedSample(_random);
                sample.SetSatisfiedClass(_classes);
                if (uniqueSamplesDict.ContainsKey((sample.Feature1, sample.Feature2)))
                    continue;

                uniqueSamplesDict.Add((sample.Feature1, sample.Feature2), sample);
                i++;
            }

            return uniqueSamplesDict.Values.ToList();
        }
    }
}
