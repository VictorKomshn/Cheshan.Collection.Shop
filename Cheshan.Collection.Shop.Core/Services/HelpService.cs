using Cheshan.Collection.Shop.Core.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class HelpService : IHelpService
    {
        public async Task RequestHelpAsync(string name, string email, string phone, string message)
        {
            await Task.CompletedTask;
        }
    }
}
