using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IHelpService
    {
        Task RequestHelpAsync(string name, string email, string phone, string message);
    }
}
