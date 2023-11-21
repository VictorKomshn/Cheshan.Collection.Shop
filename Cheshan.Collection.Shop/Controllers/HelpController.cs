using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("help")]
    [ApiController]
    public class HelpController : Controller
    {
        private readonly IHelpService _service;

        public HelpController(IHelpService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("")]
        [HttpPost]
        public async Task<bool> RequestHelp([FromBody]HelpRequest request)
        {
            try
            {
                await _service.RequestHelpAsync(request.Name, request.Email, request.Phone, request.Message);
                return true;
            }
            catch(Exception ex)
            {
                return false ;
            }
        }
    }
}
