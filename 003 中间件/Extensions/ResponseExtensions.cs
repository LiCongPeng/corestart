using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    public static class ResponseExtensions
    {
        public static Task WriteLineAsyc(this HttpResponse response, string str)
        {
            return response.WriteAsync(str + "\r\n");
        }
    }
}
