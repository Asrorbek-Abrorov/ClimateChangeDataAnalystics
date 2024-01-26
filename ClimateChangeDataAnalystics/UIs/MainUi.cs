using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateChangeDataAnalystics.UIs;

public class MainUi
{
    private readonly HourByHour hourByHour = new HourByHour();
    private readonly NowUi nowUi = new NowUi();
    private readonly WeekUi weekUi = new WeekUi();
    public async Task Run()
    {
        var keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText(" * Main Ui * ")
                .LeftJustified()
                .Color(Color.Cyan1));

            AnsiConsole.WriteLine();

            var ui = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[orange1 rapidblink italic bold]All about[/] [orange1 rapidblink]the[/] [orange1 rapidblink italic bold link]weather[/]!")
                .PageSize(5)
                .MoreChoicesText("[grey rapidblink](Move up and down to reveal more)[/]")
                .AddChoices(new[] {
                "Show me now forecast", "Show me today forecast", "Show me weekly forecast", "[red1 invert]Exit[/]"
                }));
            switch (ui)
            {
                case "Show me now forecast":
                    var date1 = DateTime.Now;
                     await nowUi.Run(date1);
                    break;
                case "Show me today forecast":
                    var date2 = DateTime.Now;
                    await hourByHour.Run(date2);
                    break;
                case "Show me weekly forecast":
                    var date3 = DateTime.Now;
                    await weekUi.Run(date3);
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }
            await Console.Out.WriteLineAsync();
            AnsiConsole.Markup("[slowblink green1]Enter[/] any [link yellow1]key[/] to [italic red]continue[/]!");
            Console.ReadKey();
        }
    }
}
