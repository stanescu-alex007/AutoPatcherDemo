using System.ComponentModel.DataAnnotations;

namespace FileSyncDemo.BusinessLogic.Models.SourceModels;

public class SourceFileModelForCreation
{
    [Required]
    [MaxLength(30)]
    public string? FileName { get; set; }
    public string? FileContent { get; set; }

}

