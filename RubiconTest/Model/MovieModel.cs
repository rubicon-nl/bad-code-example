using System.Collections.Generic;

namespace RubiconTest.Model
{
    class MovieModel
    {
         // Generated this code using Edit-> Paste Special from the response I received through Postman
         
        public class Movies
        {
            public List<Movie> Movie { get; set; }
        }

        public class Movie
        {
            public float budget { get; set; }
            public string country { get; set; }
            public string genres { get; set; }
            public string homepage { get; set; }
            public string imdbid { get; set; }
            public string imdburl { get; set; }
            public string language { get; set; }
            public string originaltitle { get; set; }
            public string overview { get; set; }
            public string productioncompany { get; set; }
            public float rating { get; set; }
            public string recordtype { get; set; }
            public float revenue { get; set; }
            public string runtime { get; set; }
            public string status { get; set; }
            public string tagline { get; set; }
            public string title { get; set; }
            public int year { get; set; }
        }

    }
}
