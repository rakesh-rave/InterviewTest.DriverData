using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTest.DriverData;
using InterviewTest.DriverData.Analysers;

namespace InterviewTest.Commands
{
	public class AnalyseHistoryCommand
	{
        // BONUS: What's great about readonly?
        //Readonly prevents fields from being changed.Readonly fields can be initialized at runtime,
        //the value of the field can be assigned at the time of declaration or in constructor only not in any other methods. 
        //After that we cannot change the value

        private readonly IAnalyser _analyser;

		public AnalyseHistoryCommand(IReadOnlyCollection<string> arguments)
		{
			var analysisType = arguments.Single();
			_analyser = AnalyserLookup.GetAnalyser(analysisType);
		}

		public void Execute()
		{
			var analysis = _analyser.Analyse(CannedDrivingData.History);

			Console.Out.WriteLine($"Analysed period: {analysis.AnalysedDuration:g}");
			Console.Out.WriteLine($"Driver rating: {analysis.DriverRating:P}");
		}
	}
}
