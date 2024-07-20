namespace FileSyncDemo.Core.Entities;

public class SourceFile : Entity
{
    //For now txt is the default extension, and the default path is desktop too.
    public static string DesktopPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public string? FileName { get; set; }
    public string FileExtension { get; set; } = "txt"; 
    public string? FilePath { get => Path.Combine(DesktopPath, FileName + "." + FileExtension); set { Path.Combine(DesktopPath, FileName + "." + FileExtension); } }

    public ICollection<ReplicaFile> ReplicaFiles { get; set; }

}
