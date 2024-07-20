using FileSyncDemo.BusinessLogic.Models.ReplicaModels;
using FileSyncDemo.BusinessLogic.Services.Implementations;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileSyncDemo.WEB.Controllers
{
    public class ReplicaFileController : ControllerBase
    {

        private readonly IReplicaFileService _replicaFileService;

        public ReplicaFileController(IReplicaFileService replicaFileService)
        {
            _replicaFileService = replicaFileService;
        }

        [HttpPost("Create-ReplicaFile")]
        public async Task<IActionResult> CreateFile(ReplicaFileModelForCreation request)
        {

            var replicaFile_id = await _replicaFileService.CreateAsync(request);

            if (replicaFile_id != Guid.Empty)
            {
                return Ok(new
                {
                    Message = "Fișierul Replica a fost creat pe desktop.",
                    File = replicaFile_id
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Fișierul Replica nu a fost creat pe desktop.",
                });
            }

            

        }

        [HttpDelete("Delete-ReplicaFile")]
        public async Task DeleteFile(Guid id)
        {
            await _replicaFileService.RemoveAsync(id);
        }


    }
}
