using CommandLine;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Uno.IconFontUpdater;

const string IconFontName = "uno-fluentui-assets.ttf";
const string CssFontRegex = @"(font-family: ""Symbols"";[^\}]*src:)(.*format\('woff'\);)";
const string CssFontFormatString = "url(data:application/x-font-woff;charset=utf-8;base64,{0}) format('woff');";

var parseResult = Parser.Default.ParseArguments<CommandOptions>(args);
if (parseResult.Errors.Any() || parseResult.Value is null)
{
    Console.WriteLine("Invalid arguments");
    return;
}

CommandOptions options = parseResult.Value;
var repositoryPath = options.RepositoryPath;
var cssFontDeclaration = CreateWoffCssDeclaration();
UpdateDirectory(new DirectoryInfo(repositoryPath));

void UpdateDirectory(DirectoryInfo directory)
{
    foreach (var file in directory.EnumerateFiles("*.ttf"))
    {
        if (file.Name == IconFontName)
        {
            Console.WriteLine($"Updating {file.FullName}");
            ReplaceIconFontFile(file.FullName);
        }
    }

    foreach (var file in directory.EnumerateFiles("*.css"))
    {
        ReplaceCssFont(file.FullName);
    }

    foreach (var subDirectory in directory.EnumerateDirectories())
    {
        UpdateDirectory(subDirectory);
    }
}
                
void ReplaceIconFontFile(string iconFontFilePath)
{
    File.Copy(options.TtfPath, iconFontFilePath, true);
}

string CreateWoffCssDeclaration()
{
    var bytes = File.ReadAllBytes(options.WoffPath);
    var base64FontData =System.Convert.ToBase64String(bytes);
    return string.Format(CssFontFormatString, base64FontData);
}

void ReplaceCssFont(string cssFilePath)
{
    var fileContent = File.ReadAllText(cssFilePath);
    if (Regex.IsMatch(fileContent, CssFontRegex))
    {
        fileContent = Regex.Replace(fileContent, CssFontRegex, m => m.Groups[1].Value + cssFontDeclaration);
        File.WriteAllText(cssFilePath, fileContent);
    }
}