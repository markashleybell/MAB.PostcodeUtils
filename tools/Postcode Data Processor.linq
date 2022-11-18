<Query Kind="Program">
  <Reference Relative="..\src\MAB.PostcodeUtils\bin\Debug\netstandard2.0\MAB.PostcodeUtils.dll">D:\Src\Personal\MAB.PostcodeUtils\src\MAB.PostcodeUtils\bin\Debug\netstandard2.0\MAB.PostcodeUtils.dll</Reference>
  <NuGetReference>CsvHelper</NuGetReference>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.Configuration.Attributes</Namespace>
  <Namespace>CsvHelper.Delegates</Namespace>
  <Namespace>CsvHelper.Expressions</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
  <Namespace>MAB.PostcodeUtils</Namespace>
  <Namespace>static Functions</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	/* 
	You can get the raw UK postcode data from:
	
	https://osdatahub.os.uk/downloads/open/CodePointOpen
	
	Dump all the CSV files from Code-Point Open into the 
	'data\codepointopen' subfolder in the folder containing 
	this query.
	
	Alternatively, you can use the ONS data available from:
	
	https://geoportal.statistics.gov.uk/datasets/ons::ons-postcode-directory-may-2022-1/about
	
	This includes Northern Ireland, Channel Islands and 
	Isle Of Man postcodes too... but the licensing is 
	restricted, so the NI data at least can't be used in 
	public-facing commercial applications without a license.
	
	Either way, you can then run the below to get a massive 
	list of all the UK postcodes as parsed UkPostcode values.
	*/

	var dataFolder = Path.GetDirectoryName(Util.CurrentQueryPath) + @"\data";
	
	// var all = LoadCodePointOpenPostcodes(dataFolder + @"\codepointopen");
	var all = LoadOnsPostcodes(dataFolder + @"\ons");
		
	var parsed = all
		.Select(c => {
			var r = UkPostcode.TryParse(c, out var p);
			return new { Input = c, Parsed = r, Value = p };
		})
		.ToList();
	
	parsed.Count.Dump("Total Postcodes");
	
	parsed.Where(p => !p.Parsed).Dump("Invalid / Unable To Parse");
	
	parsed.GetRandomSamples(3).Dump();
}

public static class Functions
{
	public static Random Rnd = new Random((int)(DateTime.Now.Ticks / 100));

	public static IEnumerable<string> LoadOnsPostcodes(string dataFolder)
	{
		var files = Directory.GetFiles(dataFolder, "*.csv");

		var codes = new List<string>();

		foreach (var file in files)
		{
			codes.AddRange(LoadOnsData(file));
		}
		
		return codes;
	}

	public static IEnumerable<string> LoadCodePointOpenPostcodes(string dataFolder)
	{
		var files = Directory.GetFiles(dataFolder, "*.csv");

		var ukCodes = new List<string>();

		foreach (var file in files)
		{
			ukCodes.AddRange(LoadCodePointOpenData(file));
		}

		/*
		NI postcodes aren't in the Code-Point Open dataset, 
		so we manually add the outward codes here (making 
		sure we remove the gaps which aren't used), and
		tack on a fake inward code so that we can at least 
		parse them with UkPostcode.
		*/

		var niOutwardExceptions =
			new[] { 50, 59, 72, 73, 83, 84, 85, 86, 87, 88, 89, 90, 91 };

		const string niArea = "BT";
		const string fakeInward = "9XX";

		var niCodes = Enumerable
			.Range(1, 94)
			.Except(niOutwardExceptions)
			.Select(c => niArea + c + fakeInward);

		return ukCodes.Concat(niCodes);
	}

	public static IEnumerable<T> GetRandomSamples<T>(this IList<T> all, int numberOfSamples) =>
		Enumerable.Range(1, numberOfSamples).Select(i => all[Rnd.Next(0, all.Count - numberOfSamples)]);

	public static IEnumerable<string> LoadCodePointOpenData(string file)
	{
		using (var reader = new StreamReader(file))
		using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			var records = csv.GetRecords<dynamic>();

			while (csv.Read())
			{
				yield return csv.GetField(0);
			}
		}
	}

	public static IEnumerable<string> LoadOnsData(string file)
	{
		using (var reader = new StreamReader(file))
		using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			var records = csv.GetRecords<dynamic>();

			csv.Read();

			while (csv.Read())
			{
				yield return csv.GetField(0);
			}
		}
	}
}
