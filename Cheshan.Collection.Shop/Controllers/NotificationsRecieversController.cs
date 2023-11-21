using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationRecieversController : Controller
    {

        private readonly INotificationRecieversService _service;

        public NotificationRecieversController(INotificationRecieversService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [Route("add/{emailAdress}")]
        public async Task<bool> AddToList(string emailAdress, [FromBody] string name)
        {
            try
            {

                if (emailAdress != null)
                {
                    await _service.AddNotificationAsync(emailAdress);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task DeleteFromList(string emailAdress)
        {
            if (emailAdress != null)
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


        [HttpPost]
        [Route("notify/{productId}")]
        public async Task NotifyOnProductSize(Guid productId, [FromBody] ProductNotificaitonRequestModel request)
        {
            try
            {
                await _service.AddProductNotificationAsync(productId, request.Size, request.Email);
            }
            catch
            {
                Redirect("NotFound");
            }
        }
    }
}
