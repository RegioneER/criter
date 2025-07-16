using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pomiager.Service.Parer
{
    public class ParerHttpClient
    {
        private static readonly HttpClient client;

        //static ParerHttpClient()
        //{
        //    // choose one of these depending on your framework

        //    // HttpClientHandler is an HttpMessageHandler with a common set of properties
        //    var handler = new HttpClientHandler()
        //    {
        //        ServerCertificateCustomValidationCallback = delegate { return true; },
        //    };
        //    // derives from HttpClientHandler but adds properties that generally only are available on full .NET
        //    var handler = new WebRequestHandler()
        //    {
        //        ServerCertificateValidationCallback = delegate { return true; },
        //        ServerCertificateCustomValidationCallback = delegate { return true; },
        //    };

        //    client = new HttpClient(handler);
        //}

    }
}
