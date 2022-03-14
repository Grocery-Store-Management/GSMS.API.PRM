using GSMS.API.PRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSMS.API.PRM.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }
    }
}
