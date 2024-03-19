using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("cdek-service")]
    [ApiController]
    public class CDEKController : Controller
    {
        private readonly ICDEKService _cdekservice;

        public CDEKController(ICDEKService CDEKService)
        {
            _cdekservice = CDEKService;
        }

        /// <summary>
        /// Метод, вызываемый API СДЭКа для получения списка отделений
        /// </summary>
        /// <param name="action"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Process(string action, [FromQuery] Dictionary<string, string> requestData)
        {
            string result;
            switch (action)
            {
                case "offices":
                    result = await _cdekservice.SendHttpRequestAsync("deliverypoints", requestData);
                    break;
                case "calculate":
                    result = await _cdekservice.SendHttpRequestAsync("calculator/tarifflist", requestData);
                    break;
                default:
                    return BadRequest("Unknown action");
            }

            return Content(result, "application/json");
        }

        /// <summary>
        /// Метод сохранения выбранного адреса ПВЗ
        /// </summary>
        /// <param name="adress"></param>
        [Route("save")]
        [HttpPost]
        public IActionResult SaveSetAdress([FromBody] CDEKAdressModel adress)
        {
            _cdekservice.SaveAdress(adress);
            return Ok();
        }
    }
}
