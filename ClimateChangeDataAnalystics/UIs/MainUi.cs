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
                .Color(Color.Green));

            AnsiConsole.WriteLine();

            var ui = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("What's your [green]favorite fruit[/]?")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices(new[] {
                "Show me now forecast", "Show me today forecast", "Show me weekly forecast", "Exit"
                }));
            switch (ui)
            {
                case "Show me now forecast":
                    nowUi.Run();
                    break;
                case "Show me today forecast":
                    var date = DateTime.Now;
                    await hourByHour.Run(date);
                    break;
                case "Show me weekly forecast":
                    weekUi.Run();
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await Console.Out.WriteLineAsync();
            await Console.Out.WriteLineAsync("Enter any key to continue");
            Console.ReadKey();
        }
    }
}
