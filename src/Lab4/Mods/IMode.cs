namespace Itmo.ObjectOrientedProgramming.Lab4.Mods;

public interface IMode
{
    public string Connect(string destinationPath);

    public void Disconnect();

    public void TreeGoTo(string destinationPath);

    public void TreeList(int depth);

    public void FileShow(string destinationPath);

    public void FileMove(string sourcePath, string destinationPath);

    public void FileCopy(string sourcePath, string destinationPath);

    public void FileDelete(string destinationPath);

    public void FileRename(string destinationPath, string newName);
}