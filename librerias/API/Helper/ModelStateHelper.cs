using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace API.Helper
{
    public static class ModelStateHelper
    {

        public static string getMessageError(this ValueEnumerable data)
        {
            string messages = string.Join("; ", data
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));

            return messages;
        }
    }
}
