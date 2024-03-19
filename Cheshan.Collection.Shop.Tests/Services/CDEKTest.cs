using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Core.Services;
using Cheshan.Collection.Shop.Database.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cheshan.Collection.Shop.Tests.Service
{
    public class CDEKTest
    {
        private CDEKService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new CDEKService();
        }

        [Test]
        public async Task Seccess_OnCorrectRegister()
        {

            var deliverys = await _service.GetDileveryPoints();
            var delJson = JsonConvert.DeserializeObject<JArray>(deliverys);

            var testProducts = new[]
            {
                new PurchasedProductEntity
                {
                    Name = "Test1",
                    SKU = "TestSku1",
                    Amount= 1,
                    Price = 100
                },
                new PurchasedProductEntity
                {
                    Name = "Test2",
                    SKU = "TestSku2",
                    Amount= 1,
                    Price = 100
                },

            };

            var testPurcahse = new PurchaseEntity
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Created = DateTime.Now,
                DeliveryAdress = "Санкт-Петербург, 192241,ул Пражская д.22",
                Email = "straicmaggacet@gmail.com",
                Name = "Test",
                Phone = "+78005553535",
                PurchasedProducts = testProducts,
            };

            testPurcahse.PurchaseId = testPurcahse.GetHashCode().ToString();

            var testAdress = new CDEKAdressModel
            {
                Code = "CHEL24"
            };
            _service.SaveAdress(testAdress);

            Guid? result = null;
            Assert.DoesNotThrowAsync(async () => result = await _service.RegisterDeliveryRequestAsync(testPurcahse));

            Assert.IsTrue(result != null);

            var requestResult = await _service.GetRequestInfo(result.Value.ToString());
            var requestInfo = JsonConvert.DeserializeObject<JObject>(requestResult);

            Assert.IsTrue(requestInfo != null);

            var a = requestInfo?["entity"]?["cdek_number"]?.Value<string>();
            Assert.IsTrue(requestInfo["requests"].First["state"].ToString() == "SUCCESSFUL");


        }
    }
}
