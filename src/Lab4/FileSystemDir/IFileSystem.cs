using Itmo.ObjectOrientedProgramming.Lab4.Mods;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystemDir;

public interface IFileSystem
{
    public void Connect(string destinationPath, IMode mode);

    public void Disconnect();

    public void TreeGoTo(string destinationPath);

    public void TreeList(int depth);

    public void FileShow(string destinationPath, IMode mode);

    public void FileMove(string sourcePath, string destinationPath);

    public void FileCopy(string sourcePath, string destinationPath);

    public void FileDelete(string destinationPath);

    public void FileRename(string destinationPath, string newName);
}