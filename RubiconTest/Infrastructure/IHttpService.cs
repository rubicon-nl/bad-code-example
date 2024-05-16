using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RubiconTest.Infrastructure
{
    /* I prefer to create an interface for all my HTTP calls, even if there is only 1 public facing function available.
     * 
     * Using an interface makes the HttpService, which is a vulnerable part of an application, a little bit more secure against hackers.
     *  Doing so by being able to hide the larger part of features and details, which aren't required for the user to know.
     *  
     * Using an interface it is easy for me to see which calls are already available and what parameters they need without scrolling through hunders of lines of code.
     *  You never know when you might want to add an additional call and having a clear view makes it easier to create one.
     */
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(Uri url, Dictionary<string, string> headers = null, string token = null);
    }
}
