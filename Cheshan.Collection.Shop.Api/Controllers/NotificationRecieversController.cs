using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Api.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationRecieversController: ControllerBase
    {

        private readonly INotificationRecieversService _service;

        public NotificationRecieversController(INotificationRecieversService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [Route("add")]
        public async Task AddToList(string emailAdress)
        {
            if (emailAdress == null)
            {
                await _service.AddNotificationAsync(emailAdress);
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task DeleteFromList(string emailAdress)
        {
            if (emailAdress == null)
            {
                await _service.DeleteNotificationAsync(emailAdress);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<NotificationRecieverModel>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return result;
        }
    }
}
