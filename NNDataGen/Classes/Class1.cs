using NNDataGen.Samples;

namespace NNDataGen.Classes
{
    public class Class1 : BaseClass
    {
        public override bool IsValueSatisfied(ISample sample)
        {
            if (sample.Feature1 < 0.5 && sample.Feature2 > 0.5)
                return true;

            if (sample.Feature1 > 0.5 && sample.Feature2 < 0.5)
                return true;

            return false;
        }

        public override int ClassKey { get; } = 1;
    }
}
