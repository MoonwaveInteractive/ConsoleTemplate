// (c) 2020 Moonwave Interactive. All rights reserved under MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonwave.Text;

namespace ConsoleTemplate
{
    /// <summary>
    /// Class that provides methods for registering and executing commands. This class cannot be inherited.
    /// </summary>
    public static class CmdSystem
    {
        private static readonly Dictionary<string, Command> commands = new Dictionary<string, Command>();

        /// <summary>
        /// Registers a new <see cref="Command"/> object to the <see cref="CmdSystem"/>.
        /// </summary>
        /// <param name="command">
        /// The <see cref="Command"/> object to be registered.
        /// </param>
        public static void Register(Command command)
        {
            if (commands.ContainsKey(command.CommandInfo.Name))
                Console.WriteLine($"Command '{command.CommandInfo.Name}' could not be added because it already exists.");
            else
                commands.Add(command.CommandInfo.Name.ToLower(), command);
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="input">
        /// The input string to be executed.
        /// </param>
        public static void Execute(string input)
        {
            var tokens = Tokenize(input);

            if (tokens.Length == 0)
                return;

            if (commands.ContainsKey(tokens[0]))
            {
                var command = commands[tokens[0]];

                if (HasHelpParam(tokens, command.PreserveInput))
                    PrintHelp(tokens[0]);
                else
                {
                    string[] arguments = null;

                    if (tokens.Length > 1)
                        if (command.PreserveInput)
                        {
                            arguments = new string[]
                            {
                                input[(tokens[0].Length + 1)..(input.Length)]
                            };
                        }
                        else
                            arguments = tokens[1..(tokens.Length)];

                    command.Execute(arguments);
                }
            }
            else
            {
                Console.WriteLine($"'{tokens[0]}' is not recognized as an internal command.\n" +
                    "Use the 'help' command for a list of available commands.\n");
            }
        }

        /// <summary>
        /// Initializes the <see cref="CmdSystem"/> with built in commands.
        /// </summary>
        public static void Initialize()
        {
            var procName = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() + ".EXE";

            Register(new Command(Exit, new CommandInfo(
                "exit",
                $"Quits the {procName} program.",
                null,
                new Parameter("exitCode", $"Specifies a numeric value. If quitting {procName}, sets the process exit code with that number.")), false));

            Register(new Command(Echo, new CommandInfo(
                "echo",
                "Displays messages.",
                null,
                new Parameter("message", "Displays the specified message.")), true));

            Register(new Command(ClearScreen, new CommandInfo(
                "cls",
                "Clears the screen."), false));

            Register(new Command(Help, new CommandInfo(
                "help",
                $"Provides help information for {procName} commands.",
                null,
                new Parameter("command", "Displays help information on that command.")), false));
        }

        private static string[] Tokenize(string input)
        {
            var tokens = new List<string>();
            var buffer = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (buffer.Length != 0)
                    {
                        tokens.Add(buffer.ToString());
                        buffer.Clear();
                    }

                    continue;
                }

                buffer.Append(c);
            }

            if (buffer.Length != 0)
                tokens.Add(buffer.ToString());

            return tokens.ToArray();
        }

        private static bool HasHelpParam(string[] tokens, bool preserveInput)
        {
            if (preserveInput && tokens.Length > 1)
            {
                if (tokens[1].StartsWith("/?"))
                    return true;
            }
            else
                foreach (string token in tokens)
                    if (token.Contains("/?"))
                        return true;

            return false;
        }

        private static void PrintHelp(string arg)
        {
            var command = commands[arg];

            Console.WriteLine(command.CommandInfo.Description + "\n");

            if (command.CommandInfo.Parameters != null)
            {
                var paramTable = new Table(" ", " ");
                paramTable.ColumnMargin = 4;

                var buffer = new StringBuilder();

                buffer.Append(command.CommandInfo.Name.ToUpper());

                foreach (Parameter parameter in command.CommandInfo.Parameters)
                {
                    buffer.Append($" [{parameter.Name}]");

                    paramTable.Add($"  {parameter.Name}", parameter.UsageText + "\n");
                }

                Console.WriteLine(buffer.ToString());

                paramTable.Print();
            }
            else
                Console.WriteLine(command.CommandInfo.Name.ToUpper() + "\n");
        }

        private static void Help(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("For more information on a specific command, type HELP [command].");

                var keys = commands.Keys.ToList();
                keys.Sort();

                var helpTable = new Table(" ", " ");
                helpTable.ColumnMargin = 4;

                foreach (string key in keys)
                {
                    var command = commands[key];

                    helpTable.Add(command.CommandInfo.Name, command.CommandInfo.Description);
                }

                helpTable.Print();
                Console.WriteLine();

                return;
            }

            if (commands.ContainsKey(args[0]))
            {
                PrintHelp(args[0]);
            }
            else
                Console.WriteLine($"This command is not supported by the help utility. Try \"{args[0]} /?\".\n");
        }

        private static void Echo(string[] args)
        {
            if (args != null)
            {
                Console.WriteLine(args[0] + "\n");
            }
        }

        private static void ClearScreen(string[] args)
        {
            Console.Clear();
        }

        private static void Exit(string[] args)
        {
            if (args == null)
                Environment.Exit(0);

            int.TryParse(args[0], out int result);

            Environment.Exit(result);
        }
    }
}