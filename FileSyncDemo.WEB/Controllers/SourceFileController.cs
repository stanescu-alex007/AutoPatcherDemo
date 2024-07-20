using FileSyncDemo.BusinessLogic.Models.SourceModels;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileSyncDemo.WEB.Controllers
{
    public class SourceFileController : ControllerBase
    {
        private readonly ISourceFileService _sourceFileService;

        public SourceFileController(ISourceFileService sourceFileService)
        {
            _sourceFileService = sourceFileService;
        }

        [HttpPost("Create-SourceFile")]
        public async Task<IActionResult> CreateFile(SourceFileModelForCreation request)
        {

            var sourceFileId = await _sourceFileService.CreateAsync(request);

            if (sourceFileId != Guid.Empty)
            {
                return Ok(new
                {
                    Message = "Fișierul Sursa a fost creat pe desktop.",
                    File = sourceFileId
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Fișierul Sursa nu a fost creat pe desktop.",
                });
            }

        }

        [HttpDelete("Delete-SourceFile")]
        public async Task DeleteFile(Guid id)
        {
            await _sourceFileService.RemoveAsync(id);

        }


    }
}
