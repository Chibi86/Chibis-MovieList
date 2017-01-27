// Movies/Genres.cs
// ----------------
// Made by: Rasmus Berg
// Purpose: This program is organize movies and help user keep record how has lend movies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Movies
{
    /// <summary>
    /// Genres
    /// ------
    /// Class to make Genres object of, methods to save and formate values from/to GUI
    /// </summary>
    public class Genres
    {
        List<string> m_genres;

        /// <summary>
        /// Default constractor
        /// </summary>
        public Genres()
        {
            m_genres = new List<string>();
        }

        /// <summary>
        /// Constractor made with Genres object
        /// </summary>
        /// <param name="oldGenres">Genres object</param>
        public Genres(Genres oldGenres)
        {
            if(oldGenres != null) // Validate
                m_genres = new List<string>(oldGenres.GenresData); // Add old list to list
        }

        /// <summary>
        /// Constractor made with backup string
        /// </summary>
        /// <param name="strGenres"></param>
        public Genres(string strGenres)
        {
            m_genres = new List<string>();

            string[] arrGenres = strGenres.Split(','); // Split string by char ","

            // Loop in genres to list m_genres
            foreach (string genre in arrGenres)
                if (!string.IsNullOrEmpty(genre)) // Validate
                    m_genres.Add(genre.Trim()); // Add to m_genres after removing space first and last in string
        }

        /// <summary>
        /// Properties for string list m_genres - get only
        /// </summary>
        public List<string> GenresData
        {
            get
            {
                return new List<string>(m_genres); // Return deep copy of list
            }
        }

        /// <summary>
        /// Properties for count of m_genres list - get only
        /// </summary>
        public int Count
        {
            get
            {
                return m_genres.Count;
            }
        } 

        /// <summary>
        /// Validate and add new genre
        /// </summary>
        /// <param name="newGenre">Genre to add (string)</param>
        /// <returns>Validate-status</returns>
        public bool Add(string newGenre)
        {
            if (!string.IsNullOrEmpty(newGenre)) // Validate
            { 
                m_genres.Add(newGenre); // Add genre to list
                return true;
            }

            return false;
        }

        /// <summary>
        /// Override ToString method for Genres List to string
        /// </summary>
        /// <returns>Genres in string format</returns>
        public override string ToString()
        {
            string strOut = "";

            // Loop genres to string
            for (int i = 0; i < m_genres.Count; i++)
            {
                if (!string.IsNullOrEmpty(m_genres[i])) // Validate
                {
                    if (i != 0)
                        strOut += ", ";

                    strOut += m_genres[i];
                }
            }

            return strOut;
        }
    }
}
