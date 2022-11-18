dotnet-coverage collect -f xml -o .\coverage\coverage.xml dotnet test .\MAB.PostcodeUtils.sln
reportgenerator -reports:.\coverage\coverage.xml -targetdir:.\coverage\report -assemblyfilters:+MAB.PostcodeUtils.dll
