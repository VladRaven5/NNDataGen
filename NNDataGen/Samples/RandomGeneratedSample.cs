using NNDataGen.Classes;
using System;
using System.Collections.Generic;

namespace NNDataGen.Samples
{
    public class RandomGeneratedSample : ISample
    {
        public double Feature1 { get; }
        public double Feature2 { get; }
        public int ClassKey { get; private set; }

        public RandomGeneratedSample(Random random)
        {
            Feature1 = random.NextDouble();
            Feature2 = random.NextDouble();
        }

        public void SetSatisfiedClass(IEnumerable<BaseClass> possibleClasses)
        {
            foreach (var possibleClass in possibleClasses)
            {
                if (!possibleClass.IsValueSatisfied(this))
                    continue;

                this.ClassKey = possibleClass.ClassKey;
                return;
            }

            throw new Exception($"No class found for value ({Feature1}, {Feature2})");
        }
    }
}
