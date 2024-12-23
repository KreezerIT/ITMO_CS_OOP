using Itmo.ObjectOrientedProgramming.Lab4.Mods;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

public class FileSystem : IFileSystem
{
    public string? ConnectedAbsolutePath { get; set; }

    public string? CurrentAbsolutePath { get; set; }

    public void Connect(string destinationPath, IMode mode)
    {
        if (!Path.Exists(destinationPath))
        {
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");
        }

        ConnectedAbsolutePath = mode.Connect(destinationPath);
        CurrentAbsolutePath = ConnectedAbsolutePath;
    }

    public void Disconnect()
    {
        ConnectedAbsolutePath = string.Empty;
        CurrentAbsolutePath = string.Empty;
    }

    public void TreeGoTo(string destinationPath)
    {
        if (!Path.Exists(destinationPath))
        {
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");
        }

        CurrentAbsolutePath = destinationPath;
    }

    public void TreeList(int depth)
    {
        string currentPath = CurrentAbsolutePath ?? throw new InvalidDataException("CurrentAbsolutePath is null.");

        var stack = new Stack<(string Path, int CurrentDepth, string Prefix)>();
        stack.Push((currentPath, 0, string.Empty));

        while (stack.Count > 0)
        {
            (string path, int currentDepth, string prefix) = stack.Pop();

            if (currentDepth >= depth)
                continue;

            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            // Для директорий
            for (int i = 0; i < directories.Length; i++)
            {
                bool isLastDir = i == directories.Length - 1;
                string dirName = Path.GetFileName(directories[i]);
                Console.WriteLine($"{prefix}{(isLastDir ? "┗━" : "┣━")} {dirName}");

                // Push для следующего уровня директорий
                stack.Push((directories[i], currentDepth + 1, prefix + (isLastDir ? "    " : "┃   ")));
            }

            // Для файлов
            for (int i = 0; i < files.Length; i++)
            {
                bool isLastFile = i == files.Length - 1;
                string fileName = Path.GetFileName(files[i]);

                Console.WriteLine($"{prefix}{(isLastFile ? "┗━" : "┣━")} {fileName}");
            }
        }
    }

    public void FileShow(string destinationPath, IMode mode)
    {
        mode.FileShow(destinationPath);
    }

    public void FileMove(string sourcePath, string destinationPath)
    {
        if (!Path.Exists(sourcePath))
            throw new FileNotFoundException($"Path {sourcePath} does not exist.");
        if (!Path.Exists(destinationPath))
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");

        string destinationDir = Path.GetDirectoryName(destinationPath) ?? throw new ArgumentException("Invalid destination path");
        if (!Directory.Exists(destinationDir))
            Directory.CreateDirectory(destinationDir);

        File.Move(sourcePath, destinationPath, true);
    }

    public void FileCopy(string sourcePath, string destinationPath)
    {
        if (!Path.Exists(sourcePath))
            throw new FileNotFoundException($"Path {sourcePath} does not exist.");
        if (!Path.Exists(destinationPath))
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");

        string destinationDir = Path.GetDirectoryName(destinationPath) ?? throw new ArgumentException("Invalid destination path");
        if (!Directory.Exists(destinationDir))
            Directory.CreateDirectory(destinationDir);

        File.Copy(sourcePath, destinationPath, true);
    }

    public void FileDelete(string destinationPath)
    {
        if (!Path.Exists(destinationPath))
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");

        File.Delete(destinationPath);
    }

    public void FileRename(string destinationPath, string newName)
    {
        if (!Path.Exists(destinationPath))
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");
        if (string.IsNullOrEmpty(newName))
            throw new ArgumentException("New file name cannot be null or empty.", nameof(newName));

        string directory = Path.GetDirectoryName(destinationPath) ?? throw new ArgumentException("Invalid file path");

        string newFilePath = Path.Combine(directory, newName);

        if (File.Exists(newFilePath))
            throw new IOException($"A file with the name '{newName}' already exists in the destination directory.");

        File.Move(destinationPath, newFilePath);
    }
}