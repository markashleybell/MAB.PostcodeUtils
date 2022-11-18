<Query Kind="Program">
  <Reference Relative="..\src\MAB.PostcodeUtils\bin\Debug\netstandard2.0\MAB.PostcodeUtils.dll">D:\Src\Personal\MAB.PostcodeUtils\src\MAB.PostcodeUtils\bin\Debug\netstandard2.0\MAB.PostcodeUtils.dll</Reference>
  <Namespace>MAB.PostcodeUtils</Namespace>
</Query>

void Main()
{
    if (UkPostcode.TryParse("SM1 100", out var postcode))
	{
		postcode.Dump();
	}
}
