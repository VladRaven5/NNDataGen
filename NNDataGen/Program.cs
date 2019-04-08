using CommandLine;
using CsvHelper;
using CsvHelper.Configuration;
using NNDataGen.Classes;
using NNDataGen.Samples;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NNDataGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Options opts = default(Options);
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opt => opts = opt)
                .WithNotParsed(e =>
                {
                    Console.WriteLine("Wrong args");
                    return;
                });

            var samplesCount = opts.SamplesCount;
            Console.WriteLine($"Samples count: {samplesCount}");

            var classes = new List<BaseClass>
            {
                new Class1(),
                new Class2()
            };

            var samplesGenerator = new RandomSamplesGenerator(samplesCount, classes, new Random());
            IEnumerable<ISample> samples = samplesGenerator.GenerateSamples();

            using (var writer = new StreamWriter("file.csv"))
            using (var csv = new CsvWriter(writer, new Configuration
            {
                CultureInfo = CultureInfo.InvariantCulture,
                Delimiter = ","
            }))
            {
                csv.WriteRecords(samples);
            }
        }
    }

    public class Options
    {
        [Option('c', "count", Required = true)]
        public int SamplesCount { get; set; }
    }
}
