// MainForm.cs
// -----------
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
using System.IO;
using MovieList.Movies;

namespace MovieList
{
    /// <summary>
    /// MainForm : Form
    /// ---------------
    /// Takecare of all printing to GUI, what user see and can do in the program - The front of the software
    /// </summary>
    public partial class MainForm : Form
    {
        MovieList m_movielist = new MovieList();
        const string m_backupfile = "movielist.txt"; // File for backup

        /// <summary>
        /// Default constractor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
            GetBackup(); // Get backup from file
        }

        /// <summary>
        /// Initialize GUI at program start
        /// </summary>
        private void InitializeGUI()
        {
            this.Text = "Chibi's MovieList - Your movie organizer"; // Software title

            // Disabled and hide buttons
            btnChange.Enabled = false;
            btnRemove.Enabled = false;
            btnLend.Enabled = false;
            btnRemoveLend.Visible = false;
        }

        /// <summary>
        /// Get local backup from file
        /// </summary>
        private void GetBackup()
        {
            string line;

            try
            {   // Open the text file using a stream reader.
                using (StreamReader file = new StreamReader(m_backupfile))
                {
                    // Read the stream to a string, and write the string to the console.
                    while ((line = file.ReadLine()) != null)
                    {
                        m_movielist.AddBackupMovie(line);
                    }

                    file.Close();

                    UpdateGUI();
                }
            }
            catch { }
        }

        /// <summary>
        /// Update lstMovieList with changes from m_movielist
        /// </summary>
        private void UpdateGUI()
        {
            lstMovieList.SelectedIndex = -1; // Unselect
            lstMovieList.Items.Clear();      // Clear listbox
            lstMovieList.Items.AddRange(m_movielist.GetMovieList()); // Add list from a string array with movies
        }

        /// <summary>
        /// Listen to click on btnAdd - Open MovieForm to add a new movie
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            MovieForm formMovie = new MovieForm(); // Make a new MovieForm

            // Open and show dialog/form and then listen for click on DialogResult.OK set button (btnOK)
            if (formMovie.ShowDialog() == DialogResult.OK)
            {
                // Add customer to movielist
                if (m_movielist.AddMovie(new Movie(formMovie.MovieData)))
                    UpdateGUI(); // Update lstMovieList
                else
                    MessageBox.Show("- Something went wrong, movie was not added.\nPlease try agian!");
            }
        }

        /// <summary>
        /// Listen to click on btnChange - Open MovieForm for change media format with selected index
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            int index = lstMovieList.SelectedIndex; // Selected movie in movie listbox (lstMovieList)

            // Validate selected movie
            if (index > -1)
            {
                if (m_movielist.ValidateIndex(index))
                {
                    MovieForm formMovie = new MovieForm("Change media format"); // Make a new MovieForm

                    formMovie.MovieData = m_movielist.GetMovie(index); // Add movie object's values to form

                    // Open and show dialog/form and then listen for click on DialogResult.OK set button (btnOK)
                    if (formMovie.ShowDialog() == DialogResult.OK)
                    {
                        // Add customer to movielist
                        if (m_movielist.ChangeMovie(index, new Movie(formMovie.MovieData)))
                            UpdateGUI(); // Update lstMovieList
                        else
                            MessageBox.Show("- Something went wrong, media format was not change.\nPlease try agian!");
                    }
                }
                else
                    MessageBox.Show("- Not finding movie to change, fail to save input.\nPlease try agian!");
            }
        }

        /// <summary>
        /// Listen to click on btnDelete - Ask for confirmation delete customer with selected index, and do that if user accept
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = lstMovieList.SelectedIndex; // Selected movie in movie listbox (lstMovieList)

            // Validate selected movie
            if (index > -1)
            {
                if (m_movielist.ValidateIndex(index))
                {
                    // Check with user if she/he really want to remove movie
                    DialogResult result = MessageBox.Show("Sure you want to remove movie?",
                    "Confirmation", MessageBoxButtons.OKCancel);

                    // If it's okey on that, remove
                    if (result == DialogResult.OK)
                    {
                        // Remove movie with selected index
                        if (m_movielist.RemoveMovie(index))
                            UpdateGUI(); // Update movie listbox (lstMovieList)
                        else
                            MessageBox.Show("- Something went wrong, movie was not remove.\nPlease try agian!");
                    }
                }
                else
                    MessageBox.Show("- Not finding selected movie to remove!\nPlease try agian!");
            }
        }

        /// <summary>
        /// Listen to click on btnLend - Open LendingForm for add a lending to movie with selected index
        /// </summary>
        private void btnLend_Click(object sender, EventArgs e)
        {
            int index = lstMovieList.SelectedIndex; // Selected movie in movie listbox (lstMovieList)

            // Validate selected movie
            if (index > -1)
            {
                if (m_movielist.ValidateIndex(index))
                {
                    LendingForm formLend = new LendingForm(); // Make a new LendingForm

                    // Open and show dialog/form and then listen for click on DialogResult.OK set button (btnOK)
                    if (formLend.ShowDialog() == DialogResult.OK)
                    {
                        // Add lending to movie with selected index
                        m_movielist.LendOut(index, new Lending(formLend.LendingData));

                        UpdateGUI(); // Update lstMovieList
                    }
                }
            }
        }

        /// <summary>
        /// Listen to click on btnRemoveLend - Ask for conformation to remove lending, and do that if user accept
        /// </summary>
        private void btnRemoveLend_Click(object sender, EventArgs e)
        {
            int index = lstMovieList.SelectedIndex; // Selected movie in movie listbox (lstMovieList)

            // Validate selected movie
            if (index > -1)
            {
                if (m_movielist.ValidateIndex(index))
                {
                    // Check with user if user really want to remove movies lending
                    DialogResult result = MessageBox.Show("Sure you want to remove lend on movie?",
                    "Confirmation", MessageBoxButtons.OKCancel);

                    // If it's okey on that, remove
                    if (result == DialogResult.OK)
                    {
                        // Remove lending for movie with selected index
                        if (m_movielist.GetBackFromLend(index))
                            UpdateGUI(); // Update lstMovieList
                        else
                            MessageBox.Show("- Something went wrong, lend for movie was not remove.\nPlease try agian!");
                    }
                }
                else
                    MessageBox.Show("- Not finding selected movie to remove lend on!\nPlease try agian!");
            }
        }

        /// <summary>
        /// Listen for closing form - Backup to file
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string toFile = m_movielist.GetBackupForFile();

            try {
                StreamWriter file = new StreamWriter(m_backupfile);
                file.Write(toFile);

                file.Close();
            }
            catch
            {
                // Double check with user if user really want to close without backup
                DialogResult result = MessageBox.Show("You gone lose every thing.\nDo really want close program?",
                    "Backup to file fail!", MessageBoxButtons.OKCancel);

                // If it was cancel, prevent closing
                if (result != DialogResult.OK)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Listen to change selected index at lstMovieList - Enabled/disabled and show/hide buttons
        /// </summary>
        private void lstMovieList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstMovieList.SelectedIndex; // Selected movie in movie listbox (lstMovieList)

            // Validate selected movie
            if (index > -1)
            {
                if (m_movielist.ValidateIndex(index))
                {
                    btnChange.Enabled = true;
                    btnRemove.Enabled = true;

                    if (m_movielist.GetMovie(index).LendingData.LendingStatus)
                    {
                        btnLend.Enabled = false;
                        btnRemoveLend.Enabled = true;
                        btnLend.Visible = false;
                        btnRemoveLend.Visible = true;
                    }
                    else
                    {
                        btnLend.Enabled = true;
                        btnRemoveLend.Enabled = false;
                        btnLend.Visible = true;
                        btnRemoveLend.Visible = false;
                    }

                    return;
                }
            }

            btnChange.Enabled = false;
            btnRemove.Enabled = false;
            btnLend.Enabled = false;
            btnRemoveLend.Enabled = false;
            btnLend.Visible = true;
            btnRemoveLend.Visible = false;
        }
    }
}
