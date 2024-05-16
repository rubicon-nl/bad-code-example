using RubiconTest.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RubiconTest.Helper
{
    class Extensions
    {
        public List<MovieModel.Movie> XmlToMovieList(XDocument xdoc, XNamespace ns)
        {
            List<MovieModel.Movie> movies = new List<MovieModel.Movie>();
            foreach (var el in xdoc.Descendants(ns + "Movie"))
            {
                MovieModel.Movie movie = new MovieModel.Movie
                {
                    budget = (float)Convert.ToDouble(el.Element(ns + "budget").Value),
                    country = el.Element(ns + "country").Value,
                    genres = el.Element(ns + "genres").Value,
                    homepage = el.Element(ns + "homepage").Value,
                    imdbid = el.Element(ns + "imdbid").Value,
                    imdburl = el.Element(ns + "imdburl").Value,
                    language = el.Element(ns + "language").Value,
                    originaltitle = el.Element(ns + "originaltitle").Value,
                    overview = el.Element(ns + "overview").Value,
                    productioncompany = el.Element(ns + "productioncompany").Value,
                    rating = (float)Convert.ToDouble(el.Element(ns + "rating").Value),
                    recordtype = el.Element(ns + "recordtype").Value,
                    revenue = (float)Convert.ToDouble(el.Element(ns + "revenue").Value),
                    runtime = el.Element(ns + "runtime").Value,
                    status = el.Element(ns + "status").Value,
                    tagline = el.Element(ns + "tagline").Value,
                    title = el.Element(ns + "title").Value,
                    year = Convert.ToInt32(el.Element(ns + "year").Value)
                };

                movies.Add(movie);
            }
            return movies;
        }

        public void MovieListToInfolist(List<MovieModel.Movie> movies)
        {
            foreach (MovieModel.Movie movie in movies)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine();
                Console.WriteLine("budget: " + movie.budget);
                Console.WriteLine("country: " + movie.country);
                Console.WriteLine("genres: " + movie.genres);
                Console.WriteLine("homepage: " + movie.homepage);
                Console.WriteLine("imdbid: " + movie.imdbid);
                Console.WriteLine("imdburl: " + movie.imdburl);
                Console.WriteLine("language: " + movie.language);
                Console.WriteLine("originaltitle: " + movie.originaltitle);
                Console.WriteLine("overview: " + movie.overview);
                Console.WriteLine("productioncompany: " + movie.productioncompany);
                Console.WriteLine("rating: " + movie.rating);
                Console.WriteLine("recordtype: " + movie.recordtype);
                Console.WriteLine("revenue: " + movie.revenue);
                Console.WriteLine("runtime: " + movie.runtime);
                Console.WriteLine("status: " + movie.status);
                Console.WriteLine("tagline: " + movie.tagline);
                Console.WriteLine("title: " + movie.title);
                Console.WriteLine("year: " + movie.year);
            }
        }

        public void MovieListToTable(List<MovieModel.Movie> movies)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("- year | title");
            Console.WriteLine("---------------------------------------");
            foreach (MovieModel.Movie movie in movies)
            {
                Console.WriteLine("- "+movie.year+" | "+movie.title);
            }
        }
    }
}
