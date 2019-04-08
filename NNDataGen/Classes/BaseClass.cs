using NNDataGen.Samples;

namespace NNDataGen.Classes
{
    public abstract class BaseClass
    {
        public abstract bool IsValueSatisfied(ISample sample);
        public abstract int ClassKey { get; }
    }
}
