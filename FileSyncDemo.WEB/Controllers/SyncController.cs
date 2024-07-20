using FileSyncDemo.BusinessLogic.BackgroundServices;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileSyncDemo.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;
        private readonly FileSyncBackgroundService _backgroundService;

        public SyncController(ISyncService syncService, FileSyncBackgroundService backgroundService )
        {
            _syncService = syncService;
            _backgroundService = backgroundService;
        }

        [HttpPost("Synchronize_Files")]
        public async Task<IActionResult> SyncFile()
        {
            var response = await _syncService.SynchronizeFileManuallyAsync();
            return Ok(response);
        }

        [HttpPost("Start_Automatic_Synchronization")]
        public IActionResult StartAutomaticSync()
        {
            _backgroundService.StartManualAutoSync();
            return Ok(new { Message = "Automatic synchronization has been started manually." });
        }

        [HttpPost("Stop_Automatic_Synchronization")]
        public IActionResult StopAutomaticSync()
        {
            _backgroundService.StopManualAutoSync();
            return Ok(new { Message = "Automatic synchronization has been stopped manually." });
        }


    }
}
