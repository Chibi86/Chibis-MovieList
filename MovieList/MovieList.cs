// MovieList.cs
// ------------
// Made by: Rasmus Berg
// Purpose: This program is organize movies and help user keep record how has lend movies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieList.Movies;

namespace MovieList
{
    /// <summary>
    /// MovieList
    /// ---------
    /// Class to keep Movie object's in, methods to save, change, validate and formate values from/to GUI
    /// </summary>
    class MovieList
    {
        private List<Movie> m_movies;

        /// <summary>
        /// Default constractor
        /// </summary>
        public MovieList()
        {
            m_movies = new List<Movie>();
        }

        /// <summary>
        /// Properties of m_movies count - get only
        /// </summary>
        public int Count
        {
            get { return m_movies.Count; }
        }

        /// <summary>
        /// Add movie to list
        /// </summary>
        /// <param name="newMovie">Movie object to add</param>
        /// <returns>Validate status (bool)</returns>
        public bool AddMovie(Movie newMovie)
        {
            if (newMovie != null) // Validate
            {
                m_movies.Add(new Movie(newMovie)); // Add deep copy
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validate and add movie from string backup
        /// </summary>
        /// <param name="line">string with a movie data</param>
        /// <returns>Validate status</returns>
        public bool AddBackupMovie(string strMovie)
        {
            return AddMovie(new Movie(strMovie));
        }

        /// <summary>
        /// Return deep copy of movie with asked index
        /// </summary>
        /// <param name="index">index from selected (int)</param>
        /// <returns>Copy of Movie object</returns>
        public Movie GetMovie(int index)
        {
            if (ValidateIndex(index)) // Validate index
                return new Movie(m_movies[index]); // Return deep copy with selected index

            return null;
        }

        /// <summary>
        /// Remove movie from list
        /// </summary>
        /// <param name="index">Selected index (int)</param>
        /// <returns>Validate status (bool)</returns>
        public bool RemoveMovie(int index)
        {
            if (ValidateIndex(index)) // Validate index
            {
                m_movies.RemoveAt(index); // Delete movie with selected index
                return true;
            }

            return false;
        }

        /// <summary>
        /// Upgrade/downgrade media on movie 
        /// </summary>
        /// <param name="index">Selected index (int)</param>
        /// <param name="newMovie">Movie object to add</param>
        /// <returns>Validate status (bool)</returns>
        public bool ChangeMovie(int index, Movie newMovie)
        {
            if (ValidateIndex(index)) // Validate index
            {
                if (newMovie != null) // Validate object
                {
                    m_movies[index] = new Movie(newMovie); // Add new Movie object as deep copy at selected index
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Lend out selected movie
        /// </summary>
        /// <param name="index">Selected index (int)</param>
        /// <param name="newLending">New lending object</param>
        /// <returns>Validate status (bool)</returns>
        public bool LendOut(int index, Lending newLending)
        {
            if (ValidateIndex(index)) // Validate index
            {
                if (newLending != null) // Validate object
                {
                    m_movies[index].LendingData = new Lending(newLending); // Add new Lending object as deep copy
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get back selected movie from lend
        /// </summary>
        /// <param name="index">Selected index</param>
        /// <returns>Validate status</returns>
        public bool GetBackFromLend(int index)
        {
            if (ValidateIndex(index)) // Validate index
            {
                Lending newLending = new Lending(m_movies[index].LendingData); // Make a new Lending object as deep copy existing Lending
                newLending.GetBack(); // Run GetBack-method for set lending over
                m_movies[index].LendingData = newLending; // Add new Lending object as selected movies Lending object
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return movielist as a string-array
        /// </summary>
        /// <returns>String array with all movies</returns>
        public string[] GetMovieList()
        {
            string[] strMovies = new string[m_movies.Count];

            // Loop-through movies and add them to string array as strings
            for (int i = 0; i < m_movies.Count; i++)
                if(m_movies[i] != null) // Validate object
                    strMovies[i] = m_movies[i].ToString();

            return strMovies;
        }

        /// <summary>
        /// Retur movielist as a string for backup to file
        /// </summary>
        /// <returns>String with all movielist data</returns>
        public string GetBackupForFile()
        {
            string strMovies = "";

            // Loop-trough and add movies to string as backup-strings
            for (int i = 0; i < m_movies.Count; i++)
                if (m_movies[i] != null) // Validate object
                    strMovies += m_movies[i].GetBackupForFile(); // Get and add backup string for movie

            return strMovies;
        }

        /// <summary>
        /// Validate selected index
        /// </summary>
        /// <param name="index">Selected index to validate</param>
        /// <returns>Validate status (bool)</returns>
        public bool ValidateIndex(int index)
        {
            // If index is over -1 and lower and m_movies count
            if (index > -1 && index < m_movies.Count)
                if(m_movies[index] != null) // Validate object
                    return true;

            return false;
        }
    }
}
