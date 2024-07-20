using System.ComponentModel.DataAnnotations;

namespace FileSyncDemo.BusinessLogic.Models.ReplicaModels;

public class ReplicaFileModelForCreation
{
    [Required]
    [MaxLength(30)]
    public string? FileName { get; set; }
    public string? FileContent { get; set; }

}
