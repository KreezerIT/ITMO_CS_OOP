namespace Itmo.ObjectOrientedProgramming.Lab4.Mods.ModeTypes;

public class LocalMode : IMode
{
    public string Connect(string destinationPath)
    {
        return destinationPath;
    }

    public void FileShow(string destinationPath)
    {
        if (!Path.Exists(destinationPath))
            throw new FileNotFoundException($"Path {destinationPath} does not exist.");

        string fileContent = File.ReadAllText(destinationPath);

        Console.WriteLine($"File '{Path.GetFileName(destinationPath)}' content:");
        Console.WriteLine(fileContent);
    }

    public void Disconnect() { }

    public void TreeGoTo(string destinationPath) { }

    public void TreeList(int depth) { }

    public void FileMove(string sourcePath, string destinationPath) { }

    public void FileCopy(string sourcePath, string destinationPath) { }

    public void FileDelete(string destinationPath) { }

    public void FileRename(string destinationPath, string newName) { }
}