// Movies/Movie.cs
// ---------------
// Made by: Rasmus Berg
// Purpose: This program is organize movies and help user keep record how has lend movies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using MovieList.Movies;

namespace MovieList.Movies
{
    /// <summary>
    /// Lending
    /// -------
    /// Class to make Movie object of, methods to save, change, validate and formate values from/to GUI
    /// </summary>
    public class Movie
    {
        private const string m_omdbUrl = "http://www.omdbapi.com/?r=xml&type=movie&v=1"; // r = response (xml), type = movie/serier/episoder (movie), v = api version (1)
        private string m_title;
        private Media m_media;
        private int m_year;
        private double m_rating;
        private string m_runtime;
        private Lending m_lending;
        private Genres m_genres;
        private string m_strErrMessage; // String with error messages

        /// <summary>
        /// Default constactor
        /// </summary>
        public Movie()
        {
            m_title = string.Empty;
            m_media = Media.Bluray;
            m_year = new DateTime().Year;
            m_rating = 0;
            m_runtime = string.Empty;
            m_lending = new Lending();
            m_genres = new Genres();
        }

        /// <summary>
        /// Constractor with old Movie object
        /// </summary>
        /// <param name="oldMovie">Movie object</param>
        public Movie(Movie oldMovie)
        {
            if (oldMovie != null) // Validate
            {
                m_title = oldMovie.Title;
                m_media = oldMovie.MovieMedia;
                m_year = oldMovie.Year;
                m_rating = oldMovie.Rating;
                m_runtime = oldMovie.Runtime;
                m_lending = new Lending(oldMovie.LendingData);
                m_genres = new Genres(oldMovie.GenresData);
            }
        }

        /// <summary>
        /// Constractor with backup string from file
        /// </summary>
        /// <param name="strMovie">String with backup data</param>
        public Movie(string strMovie)
        {
            string[] arrMovie;

            arrMovie = strMovie.Split('|'); // Split string where every "|" char is

            m_title = arrMovie[0];
            m_media = (Media)Enum.Parse(typeof(Media), arrMovie[1]); // Convert string to enum Media
            m_year = int.Parse(arrMovie[2]);
            m_rating = double.Parse(arrMovie[3]);
            m_runtime = arrMovie[4];
            m_lending = new Lending(arrMovie[5]); // Backup lending by string
            m_genres = new Genres(arrMovie[6]); // Backup genres by string
        }

        /// <summary>
        /// Properties for m_title - get only
        /// </summary>
        public string Title
        {
            get { return m_title; }
        }

        /// <summary>
        /// Properties for m_media - get and set
        /// </summary>
        public Media MovieMedia
        {
            get { return m_media; }
            set
            {
                if ((int)value > -1) // Validate
                    m_media = value;
                else
                    m_strErrMessage = "- You need to select a Media format!\n"; // Save error message to output
            }
        }

        /// <summary>
        /// Properties for m_year - get only
        /// </summary>
        public int Year
        {
            get { return m_year; }
        }

        /// <summary>
        /// Properties for m_rating - get only
        /// </summary>
        public double Rating
        {
            get { return m_rating; }
        }

        /// <summary>
        /// Properties for m_runtime - get only
        /// </summary>
        public string Runtime
        {
            get { return m_runtime; }
        }

        /// <summary>
        /// Properties for m_lending - get and set
        /// </summary>
        public Lending LendingData
        {
            get
            {
                return new Lending(m_lending); // Return deep copy
            }
            set
            {
                if (value != null) // Validate
                    m_lending = new Lending(value); // Add deep copy
            }
        }

        /// <summary>
        /// Properties for m_genres - get only
        /// </summary>
        public Genres GenresData
        {
            get { return new Genres(m_genres);  } // Return deep copy
        }

        /// <summary>
        /// Properties for m_strErrMessage - get only
        /// </summary>
        public string StrErrMessage
        {
            get { return m_strErrMessage; }
        }

        /// <summary>
        /// Save movie by movie title, imdb url or id and media format, all other information will automatic get from Imdb (by omdbapi)
        /// </summary>
        /// <param name="text">Movie title, imdb url or id (string)</param>
        /// <param name="newMedia">Media format (Enum/Media)</param>
        /// <returns>Validate status</returns>
        public bool SaveMovie(string text, Media newMedia)
        {
            string imdbId;
            bool validate;

            // Check if got accepted imdbid or movie title
            if (ValidateImdbId(text, out imdbId))
                validate = GetMovieInfoById(imdbId); // Get movie information from imdb (omdbapi) by search on imdbid
            else if (ValidateTitle(text))
            {
                m_title = text;
                validate = GetMovieInfoByTitle(); // Get movie info from imdb (omdbapi) search on title
            }
            // If user has not input any valid imdb id or movie title, fail validation and save error message
            else
            {
                m_strErrMessage = "- You need to input a title, imdb url or id!\n";
                validate = false;
            }

            // Validate media formate
            if ((int)newMedia > -1)
                m_media = newMedia;
            else
            {
                m_strErrMessage = "- You need to pick a media format!\n";
                validate = false;
            }

            m_lending = new Lending(); // Add a new Lending

            return validate;
        }

        /// <summary>
        /// Get movie information by movie title
        /// </summary>
        private bool GetMovieInfoByTitle()
        {
            Uri omdbUri = new Uri(m_omdbUrl + "&t=" + m_title.Replace(" ", "+")); // To convert m_title to accepted url

            return GetMovieInfo(omdbUri);
        }

        /// <summary>
        /// Get movie information by imdb id
        /// </summary>
        /// <param name="imdbid">Imdb id for movie (string)</param>
        private bool GetMovieInfoById(string imdbId)
        {
            Uri omdbUri = new Uri(m_omdbUrl + "&i=" + imdbId); // To convert imdbId to accepted url

            return GetMovieInfo(omdbUri);
        }

        /// <summary>
        /// Get movie information to variables
        /// </summary>
        /// <param name="omdbUrl"></param>
        private bool GetMovieInfo(Uri omdbUrl)
        {
            bool validate = false;
            string newTitle = "";
            int newYear = 0;
            double newRating = 0;
            string newRuntime = "";
            string newGenres = "";

            try {

                XmlDocument xmlDoc = new XmlDocument(); // Make a new XmlDocument object
                xmlDoc.Load(omdbUrl.AbsoluteUri); // Load xml-document from internet url

                XmlNode xmlStatus = xmlDoc.SelectSingleNode("root"); // Get returning status root-node

                if (xmlStatus.Attributes["response"].Value == "True") // Validate response
                {
                    XmlNodeList movies = xmlDoc.SelectNodes("//root/movie"); // Get node-list by select every movie-element under root

                    // Loop-trough all movie element until any validate okey
                    foreach (XmlNode movie in movies)
                    {
                        if (movie.Attributes["type"].Value != "movie") // Probly not needed anymore, url sort out only movies
                            continue;

                        validate = true; // Set validate okey until anything fails

                        // Validate title
                        if (ValidateTitle(movie.Attributes["title"].Value))
                            newTitle = movie.Attributes["title"].Value;
                        else
                            validate = false;

                        // Validate year
                        if (!ValidateYear(movie.Attributes["year"].Value, out newYear))
                            validate = false;

                        // Validate rating
                        if (!ValidateRating(movie.Attributes["imdbRating"].Value, out newRating))
                            validate = false;

                        // Validate runtime
                        if (ValidateRuntime(movie.Attributes["runtime"].Value))
                            newRuntime = movie.Attributes["runtime"].Value;
                        else
                            validate = false;

                        // Validate genres as a string
                        if (ValidateGenres(movie.Attributes["genre"].Value))
                            newGenres = movie.Attributes["genre"].Value;
                        else
                            validate = false;

                        // If validate okey break loop
                        if (validate)
                            break;
                    }
                }

                // If none of the movies was validate okey set error message about it
                if (!validate)
                    m_strErrMessage =  "Didn't find any released title with that title, imdb url or id!\n";
            }
            catch
            {
                // If anything fails we expect the connection to OMDb api is down
                m_strErrMessage = "- Connection to OMDb api fail!\nPlease check your internet connection and try agian!";
            }

            // If everything is validate okey on one movie, save values as movie values
            if (validate)
            {
                m_title = newTitle;
                m_year = newYear;
                m_rating = newRating;
                m_runtime = newRuntime;
                m_genres = new Genres(newGenres);
            }

            return validate;
                
        }

        /// <summary>
        /// Validate movie title
        /// </summary>
        /// <param name="title">Movie title (string)</param>
        /// <returns>Validate-status (bool)</returns>
        private bool ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return false;

            return true;
        }

        /// <summary>
        /// Validate if we got imdb id, if got grabb it and return it
        /// </summary>
        /// <param name="text">string to get imdb id from</param>
        /// <param name="imdbId">string to return imdb id in</param>
        /// <returns>Validate status</returns>
        private bool ValidateImdbId(string text, out string imdbId)
        {
            Match regMatch = Regex.Match(text, @"(tt[0-9]{7})"); // Search for text with "tt" plus seven numbers between 0-9

            if (regMatch.Success) // If imdb id is find
            {
                imdbId = regMatch.Value; // Save find value to return variable

                return true;
            }

            imdbId = string.Empty; // If we fail return empty string

            return false;
        }

        /// <summary>
        /// Convert and validate year as string to int
        /// </summary>
        /// <param name="strYear">String year to convert and validate</param>
        /// <param name="intYear">Returning result of convert</param>
        /// <returns>Validate-status (bool)</returns>
        private bool ValidateYear(string strYear, out int intYear)
        {
            DateTime date = DateTime.Now; // Date now

            if (!int.TryParse(strYear, out intYear)) // If convert string to int fail return false
                return false;

            if (intYear < 1888 || intYear > date.Year) // Oldest movie at imdb - Roundhay Garden Scene (http://www.imdb.com/title/tt0392728/)
                return false;

            return true;
        }

        /// <summary>
        /// Convert and validate rating as string to int
        /// </summary>
        /// <param name="strRating">String rating to convert and validate</param>
        /// <param name="dblRating">Returning result of convert</param>
        /// <returns>Validate-status (bool)</returns>
        private bool ValidateRating(string strRating, out double dblRating)
        {
            if (!double.TryParse(strRating, out dblRating)) // If convert string to double fail return false
                return false;
            
            if (dblRating < 0) // Should not be lower and zero 
                return false;
            
            return true;
        }

        /// <summary>
        /// Validate movie runtime
        /// </summary>
        /// <param name="runtime">Movie runtime (string)</param>
        /// <returns>Validate-status (bool)</returns>
        private bool ValidateRuntime(string runtime)
        {
            if (string.IsNullOrEmpty(runtime)) // Should not be empty
                return false;

            return true;
        }

        /// <summary>
        /// Validate movie genres as string
        /// </summary>
        /// <param name="strGenres">Genres as string</param>
        /// <returns>Validate status</returns>
        private bool ValidateGenres(string strGenres)
        {
            if (string.IsNullOrEmpty(strGenres))
                return false;
            
            return true;
        } 

        /// <summary>
        /// Overide ToString method for convert Movie object to string format
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0,-60} {1,-16} {2,-7} {3,-7} {4,-10} {5,-40} {6}", m_title, m_media.ToString(), m_year, m_rating, m_runtime, m_lending.ToString(), m_genres.ToString());
        }

        /// <summary>
        /// Returns a string with all movie data seperate with "|" for backup to file
        /// </summary>
        /// <returns>String with all movie data</returns>
        public string GetBackupForFile()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}\n", m_title, m_media.ToString(), m_year, m_rating, m_runtime, m_lending.GetBackupForFile(), m_genres.ToString());
        }
    }
}
