namespace FileSyncDemo.Core.Entities;

public class ReplicaFile : Entity
{
    public static string DesktopPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public string? FileName { get; set; }
    public string FileExtension { get; set; } = "txt";
    public string? FilePath { get => Path.Combine(DesktopPath, FileName + "." + FileExtension); set { Path.Combine(DesktopPath, FileName + "." + FileExtension); } }

    public Guid SourceFileId { get; set; }
    public SourceFile SourceFile { get; set; }
}
