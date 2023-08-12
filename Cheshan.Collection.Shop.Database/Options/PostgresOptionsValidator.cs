using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheshan.Collection.Shop.Database.Options
{
    public class PostgresOptionsValidator : IValidateOptions<PostgresOptions>
    {
        public ValidateOptionsResult Validate(string? name, PostgresOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
