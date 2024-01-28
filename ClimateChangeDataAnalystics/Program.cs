using ClimateChangeDataAnalystics.UIs;
using Spectre.Console;

class Program
{
    static async Task Main()
    {
        var mainUi = new MainUi();

        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText(" * Welcome * ")
            .LeftJustified()
            .Color(Color.CadetBlue));

        var ui = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[orange1 rapidblink italic bold]All about[/] [orange1 rapidblink]the[/] [orange1 rapidblink italic bold link]weather[/]!")
                .PageSize(5)
                .MoreChoicesText("[grey rapidblink](Move up and down to reveal more)[/]")
                .AddChoices(new[] {
                "[green bold italic rapidblink]Get started[/]", "[darkred bold]Exit[/]"
                }).HighlightStyle("cyan bold italic"));

        switch (ui)
        {
            case "[green bold italic rapidblink]Get started[/]":
                await mainUi.Run();
                break;
            case "[darkred bold]Exit[/]":
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("Come back again to check the weather, Goodbye!")
                        .LeftJustified()
                        .Color(Color.BlueViolet));
                AnsiConsole.WriteLine("-----");
                AnsiConsole.MarkupLine("| c | [bold]Asrorbek Abrorov[/]. [italic]All rights are reserved.[/]");
                AnsiConsole.WriteLine("-----");
                break;
        }
    }
}