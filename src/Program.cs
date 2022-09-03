using CommandLine;
using Uno.IconFontUpdater;

const string IconFontName = "uno-fluentui-assets.ttf";

var parseResult = Parser.Default.ParseArguments<CommandOptions>(args);
if (parseResult.Errors.Any() || parseResult.Value is null)
{
    Console.WriteLine("Invalid arguments");
    return;
}

CommandOptions options = parseResult.Value;
var repositoryPath = options.RepositoryPath;



void ReplaceIconFontFile(string iconFontFilePath)
{
    File.Replace(options.TtfPath, iconFontFilePath, null);
}
