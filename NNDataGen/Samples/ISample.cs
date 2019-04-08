using CsvHelper.Configuration.Attributes;
using NNDataGen.Classes;
using System.Collections.Generic;

namespace NNDataGen.Samples
{
    public interface ISample
    {
        [Name("p1")]
        double Feature1 { get; }
        [Name("p2")]
        double Feature2 { get; }
        [Name("res")]
        int ClassKey { get; }

        void SetSatisfiedClass(IEnumerable<BaseClass> possibleClasses);
    }
}
