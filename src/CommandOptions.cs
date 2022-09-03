using CommandLine;

namespace Uno.IconFontUpdater;

public class CommandOptions
{
	[Option('r', "repository", Required = true, HelpText = "Root folder of the cloned repository.")]
	public string RepositoryPath { get; set; }

	[Option('t', "ttf-font", Required = true, HelpText = "Path to the new icon font .ttf.")]
	public string TtfPath { get; set; }

    [Option('w', "woff2-font", Required = true, HelpText = "Path to the new icon font .woff2.")]
    public string Woff2Path { get; set; }
}
