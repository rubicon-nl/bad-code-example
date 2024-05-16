using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RubiconTest.Infrastructure;
using RubiconTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RubiconTest
{
    class Program
    {
        
        public static async Task Main(string[] args)
        {
            // Define base variables
            string baseUrl = "https://intakeopdracht-apim-euw-p.azure-api.net/api/movies";
            XNamespace ns = "http://schemas.datacontract.org/2004/07/Rubicon.IntakeOpdracht.CosmosDb.Function.Models";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string token = null;

            // Build dependency injection services
            var host = CreateHostBuilder(args).Build();

            var httpService = host.Services.GetService<IHttpService>();
            var extensions = host.Services.GetService<Helper.Extensions>();

            // Initialize movies object
            List<MovieModel.Movie> movies = new List<MovieModel.Movie>();

            /////
            // Start program, setup initial variables
            int countToRetrieve = 2500;
            bool headerXml = true;
            if (headerXml)
            {
                //add xml to header so we get the correct format in the response
                headers.Add("Accept", "application/xml");
            }

            // Retrieve auth token from user
            Console.WriteLine("Hello Rubicon!");
            Console.WriteLine("Are we ready to retrieve some movies");
            Console.WriteLine("First I would need a valid API token, please insert the token here");
            token = Console.ReadLine();

            // Ask which process should be run
            Console.WriteLine();
            Console.WriteLine("Do you want to run the test process (leave empty) or make a custom api call (example: /title/toy?index=0)");
            string messageUrl = Console.ReadLine();

            ////
            // If input string is empty, start the test program
            if (messageUrl == "")
            {
                Console.WriteLine("Retrieving "+ countToRetrieve + " movie entries, please wait a moment");
                for(int i = 0; i< countToRetrieve; i += 100)
                {
                    messageUrl = "/genres/crime?index="+i;

                    // Get data from API
                    HttpResponseMessage responseMessage = await httpService.GetAsync(new Uri(baseUrl + messageUrl), headers, token);

                    // If StatusCode is not OK, try again
                    while (responseMessage.StatusCode.ToString() != "OK")
                    {
                        Thread.Sleep(3000);
                        responseMessage = await httpService.GetAsync(new Uri(baseUrl + messageUrl), headers, token);
                    }

                    // Read results into string
                    string responseString = responseMessage.Content.ReadAsStringAsync().Result;

                    // Parse results into XDocument
                    XDocument xdoc = XDocument.Parse(responseString);
                    // Using the helper funtion 'XmlToMovieList' we map the incomming data to our c# model
                    // Afterwards the mapped data gets added to the array of movies to check later
                    movies.AddRange(extensions.XmlToMovieList(xdoc, ns));

                    Console.WriteLine();
                    Console.WriteLine("Entries retrieved: " +(i+100)+ "/"+countToRetrieve);
                    Console.WriteLine();
                }

                // Order our list of movies by year
                List<MovieModel.Movie> moviesOrdered = movies.OrderByDescending(o => o.year).ToList();

                // Using the helper function 'MovieListToTable' we display information on the top 10 entries in the ordered list
                extensions.MovieListToTable(moviesOrdered.GetRange(0,10));
            }

            ////
            // Run a custom api call
            else
            {
                HttpResponseMessage responseMessage = await httpService.GetAsync(new Uri(baseUrl + messageUrl), headers, token);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseString = responseMessage.Content.ReadAsStringAsync().Result;

                    XDocument xdoc = XDocument.Parse(responseString);
                    movies.AddRange(extensions.XmlToMovieList(xdoc,ns));

                    Console.WriteLine("Entries found: " + movies.Count());
                    extensions.MovieListToInfolist(movies);

                }
            }

            // End the program
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Hit any key to initiate exit program sequence");
            Console.ReadKey();
            Console.Write("Program closing in");
            for (int i = 3; i > 0; i--)
            {
                Console.Write(" "+i+"..");
                Thread.Sleep(1000);
            }
        }

        // Builder for dependency injection
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient<IHttpService, HttpService>();
                    services.AddSingleton<Helper.Extensions>();
                });

            return hostBuilder;
        }

        
    }
}
