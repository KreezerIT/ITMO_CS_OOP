using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;
using Itmo.ObjectOrientedProgramming.Lab4.Mods;
using System.Text.RegularExpressions;

namespace Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;

public static class DefaultCommandDIContainer
    {
        public static Dictionary<string, Func<string, ICommand>> CreateContainer(IModeFactory modeFactory, FileSystem fileSystem)
        {
            var commandMappings = new Dictionary<string, Func<string, ICommand>>();

            commandMappings.Add(@"^connect (\S+) -m (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^connect (\S+) -m (\S+)$");
                if (match.Success)
                {
                    string param1 = match.Groups[1].Value;
                    string param2 = match.Groups[2].Value;
                    IMode mode = modeFactory.CreateMode(param2);

                    return new Connect(param1, mode, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^disconnect$", command =>
            {
                return new Disconnect(fileSystem);
            });

            commandMappings.Add(@"^tree goto (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^tree goto (\S+)$");
                if (match.Success)
                {
                    string param = match.Groups[1].Value;
                    return new TreeGoTo(param, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^tree list -d (\d+)$", command =>
            {
                Match match = Regex.Match(command, @"^tree list -d (\d+)$");
                if (match.Success)
                {
                    int depth = int.Parse(match.Groups[1].Value);
                    return new TreeList(depth, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^file show (\S+) -m (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^file show (\S+) -m (\S+)$");
                if (match.Success)
                {
                    string param1 = match.Groups[1].Value;
                    string param2 = match.Groups[2].Value;
                    IMode mode = modeFactory.CreateMode(param2);

                    return new FileShow(param1, mode, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^file move (\S+) (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^file move (\S+) (\S+)$");
                if (match.Success)
                {
                    string source = match.Groups[1].Value;
                    string destination = match.Groups[2].Value;
                    return new FileMove(source, destination, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^file copy (\S+) (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^file copy (\S+) (\S+)$");
                if (match.Success)
                {
                    string source = match.Groups[1].Value;
                    string destination = match.Groups[2].Value;
                    return new FileCopy(source, destination, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^file delete (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^file delete (\S+)$");
                if (match.Success)
                {
                    string target = match.Groups[1].Value;
                    return new FileDelete(target, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            commandMappings.Add(@"^file rename (\S+) (\S+)$", command =>
            {
                Match match = Regex.Match(command, @"^file rename (\S+) (\S+)$");
                if (Regex.IsMatch(command, @"^file rename (\S+) (\S+)$"))
                {
                    string oldName = match.Groups[1].Value;
                    string newName = match.Groups[2].Value;
                    return new FileRename(oldName, newName, fileSystem);
                }

                throw new InvalidOperationException($"Invalid command format: {command}");
            });

            return commandMappings;
        }
    }