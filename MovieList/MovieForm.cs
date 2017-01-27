// MovieForm.cs
// ------------
// Made by: Rasmus Berg
// Purpose: This program is organize movies and help user keep record how has lend movies
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MovieList.Movies;

namespace MovieList
{
    /// <summary>
    /// MovieForm : Form
    /// ----------------
    /// Takecare of printing to MovieForm GUI - Let user add movie and change movie media formate
    /// </summary>
    public partial class MovieForm : Form
    {
        private Movie m_movie;
        private bool m_doNotClose = false; // Variable to prevent closing

        /// <summary>
        /// Default constractor - calling another constractor
        /// </summary>
        public MovieForm() : this("Add a new movie") {}

        /// <summary>
        /// Constractor with form title
        /// </summary>
        /// <param name="title">Title for form (string)</param>
        public MovieForm(string title)
        {
            InitializeComponent();
            InitializeGUI();

            this.Text = title; // Set form title
        }

        /// <summary>
        /// Initialize GUI
        /// </summary>
        private void InitializeGUI()
        {
            m_movie = new Movie();

            // Convert Media to string array and then add array-values into combobox cmbMedia's list of choices
            cmbMedia.Items.AddRange(ConvertMediaToString());
            cmbMedia.SelectedIndex = 0; // Set default value to Bluray
        }

        /// <summary>
        /// Properties for m_movie - get and set
        /// </summary>
        public Movie MovieData
        {
            get {
                return new Movie(m_movie);
            }

            set
            {
                if (value != null) // Validate
                {
                    m_movie = new Movie(value); // Add Movie object as a deep copy
                    UpdateGUI(); // Set value to form
                }
            }
        }

        /// <summary>
        /// Update GUI when we got existing movie object
        /// </summary>
        private void UpdateGUI()
        {
            txtTitle.Text = m_movie.Title;
            txtTitle.ReadOnly = true; // Set read only if we only changing media formate
            cmbMedia.SelectedIndex = (int)m_movie.MovieMedia;
        }

        /// <summary>
        /// Convert Enum Media to string array
        /// </summary>
        /// <returns>Convert result in a string array</returns>
        private string[] ConvertMediaToString()
        {
            string[] formats = Enum.GetNames(typeof(Media)); // Convert Media to string array

            // Loop-trough and replace all underscores to spaces
            for (int i = 0; i < formats.Length; i++)
                formats[i] = formats[i].Replace("_", " ");

            return formats;
        }

        /// <summary>
        /// Read input, validate and save to movie object
        /// </summary>
        /// <returns></returns>
        private bool ReadInput()
        {
            bool validate = true;
            string strErrMessage = ""; // String for output error message

            // Validate input title
            if (string.IsNullOrEmpty(txtTitle.Text)) 
            {
                validate = false;
                strErrMessage = "- You need to input a movie title, imdb url or id!\n";
            }

            // Validate media format input
            if (cmbMedia.SelectedIndex < 0) 
            {
                validate = false;
                strErrMessage = "- You need to choose a media format!\n";
            }

            // If validate is okey, go through and add movie with more information from imdb (omdbapi) or only change media formate if it is existing movie
            if (validate)
            {
                // Check if Movie already existing
                if (string.IsNullOrEmpty(m_movie.Title))
                {
                    validate = m_movie.SaveMovie(txtTitle.Text, (Media)cmbMedia.SelectedIndex); // Save movie with more externed information

                    // If something fail, save error message from Movie object and replace object with new object with default values
                    if (!validate)
                    {
                        strErrMessage = m_movie.StrErrMessage;
                        m_movie = new Movie();
                    }
                }
                else {
                    m_movie.MovieMedia = (Media)cmbMedia.SelectedIndex; // Save new media format to movie
                    strErrMessage = m_movie.StrErrMessage;
                }

                if (string.IsNullOrEmpty(strErrMessage)) // If not got any error message return true
                    return true;
            }

            MessageBox.Show(strErrMessage, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Output error message

            return false;
        }

        /// <summary>
        /// Listen to click on btnOK, and when it happends validate and add inputs to object.
        /// If validate fail it will prevent closing
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // If ReadInput is validate okey close form
            if (ReadInput())
                m_doNotClose = false;
            // Otherwish prevent closing
            else
                m_doNotClose = true;
        }

        /// <summary>
        /// Listen to closing of form, and ask for confirmation when closing (not when DialogResult.OK set button (btnOK) was use) is about to happend.
        /// If closing with a DialogResult.OK set button is about to happend check if it's okey to close
        /// </summary>
        private void MovieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check so so it was not a button with set to DialogResult.OK (btnOK) that was clicked
            if (DialogResult != DialogResult.OK)
            {
                // Double check with user if she/he really want to close without saving
                DialogResult result = MessageBox.Show("You gone lose every changes.\nDo really want close form?",
                    "Confirmation", MessageBoxButtons.OKCancel);

                // If it was cancel, prevent closing
                if (result != DialogResult.OK)
                    e.Cancel = true;
            }
            // If it was DialogResult.OK set button (btnOK) and it's not okey to close, prevent that. 
            else if (m_doNotClose && DialogResult == DialogResult.OK)
                e.Cancel = true;
        }
    }
}
