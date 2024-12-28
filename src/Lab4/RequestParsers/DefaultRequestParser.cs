using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandler;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

namespace Itmo.ObjectOrientedProgramming.Lab4.RequestParsers;

public class DefaultRequestParser
{
    private readonly ICommandHandler _commandHandler;

    private readonly CommandFactory _commandFactory = new CommandFactory(new FileSystem());

    private readonly FileSystem _fileSystem = new FileSystem();

    private string _request;

    public DefaultRequestParser(string request, ICommandHandler startHandler, FileSystem? filesystem = null)
    {
        _request = request;
        _commandHandler = startHandler;
        _commandFactory = new CommandFactory(filesystem ?? _fileSystem);
    }

    public DefaultRequestParser(string request, ICommandHandler startHandler, CommandFactory commandFactory)
    {
        _request = request;
        _commandHandler = startHandler;
        _commandFactory = commandFactory;
    }

    public DefaultRequestParser(string request, ICommandHandler startHandler)
    {
        _request = request;
        _commandHandler = startHandler;
    }

    public void CheckCommandSupporting(string cmd)
    {
        if (!SupportedCommands.IsCommandSupported(cmd))
        {
            throw new InvalidOperationException($"Unknown command: {cmd}");
        }
    }

    public void TryCreateCommandWithParameters(string cmd)
    {
        _commandHandler.HandleCommand(_commandFactory.CreateCommand(cmd));
    }

    public void ChangeRequest(string newRequest)
    {
        _request = newRequest;
    }

    public void Parse()
    {
        var splittedRequest = new List<string>(_request.Split(" "));

        for (int i = 0; i < splittedRequest.Count; i++)
        {
            string commandElement = splittedRequest[i];
            string complexCommandElement = string.Empty;

            if (commandElement == "tree" || commandElement == "file")
            {
                if (i + 1 < splittedRequest.Count)
                {
                    complexCommandElement = commandElement + " " + splittedRequest[i + 1];
                    CheckCommandSupporting(complexCommandElement);

                    if (splittedRequest[i + 2] == "-d")
                    {
                        complexCommandElement += splittedRequest[i + 2] + splittedRequest[i + 3];
                        i += 3;
                        TryCreateCommandWithParameters(complexCommandElement);
                    }
                    else
                    {
                        if (splittedRequest[i + 1] == "goto" || splittedRequest[i + 1] == "delete")
                        {
                            CheckCommandSupporting(complexCommandElement);
                            complexCommandElement += " " + splittedRequest[i + 2];
                            i += 2;
                            TryCreateCommandWithParameters(complexCommandElement);
                        }
                        else if (splittedRequest[i + 1] == "move" || splittedRequest[i + 1] == "copy" || splittedRequest[i + 1] == "rename")
                        {
                            CheckCommandSupporting(complexCommandElement);
                            complexCommandElement += " " + splittedRequest[i + 2] + " " + splittedRequest[i + 3];
                            i += 3;
                            TryCreateCommandWithParameters(complexCommandElement);
                        }
                        else if (splittedRequest[i + 1] == "show")
                        {
                            CheckCommandSupporting(complexCommandElement);
                            complexCommandElement += " " + splittedRequest[i + 2] + " " + splittedRequest[i + 3] + " " + splittedRequest[i + 4];
                            i += 4;
                            TryCreateCommandWithParameters(complexCommandElement);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Unknown command: {commandElement}");
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Unknown command: {commandElement}");
                }
            }
            else
            {
                if (commandElement == "connect")
                {
                    complexCommandElement = commandElement + " " + splittedRequest[i + 1] + " " + splittedRequest[i + 2] + " " + splittedRequest[i + 3];
                    i += 3;
                    CheckCommandSupporting(commandElement);
                    TryCreateCommandWithParameters(complexCommandElement);
                }
                else
                {
                    CheckCommandSupporting(commandElement);
                    TryCreateCommandWithParameters(commandElement);
                }
            }
        }
    }
}