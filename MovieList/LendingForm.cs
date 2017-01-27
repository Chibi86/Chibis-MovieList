// LendingForm.cs
// --------------
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
using MovieList.Movies;

namespace MovieList
{
    /// <summary>
    /// LendingForm : Form
    /// ------------------
    /// Takecare of printing to LendingForm GUI, let the user add lending
    /// </summary>
    public partial class LendingForm : Form
    {
        Lending m_lending;
        bool m_doNotClose = false; // Variable for prevent closing

        /// <summary>
        /// Default constractor
        /// </summary>
        public LendingForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        /// <summary>
        /// Properties for m_lending - only get
        /// </summary>
        public Lending LendingData
        {
            get
            {
                return new Lending(m_lending);
            }
        }

        /// <summary>
        /// Initialize GUI (programstart)
        /// </summary>
        private void InitializeGUI()
        {
            this.Text = "Lend out movie"; // Form title

            m_lending = new Lending();
        }

        /// <summary>
        /// Listen to click on btnOK - Validate and save input to object, and the close form
        /// If validate fails prevent closing
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validate so txtName is not empty
            if (!string.IsNullOrEmpty(txtName.Text))
                m_lending.LendOut(txtName.Text); // Lend out
            else
                m_doNotClose = true; // Prevent closing
        }

        /// <summary>
        /// Listen to closing of form - Ask for confirmation when closing is about to happend. (Not when DialogResult.OK set button (btnOK) was use)
        /// If closing with a DialogResult.OK set button is about to happend check if it's okey to close
        /// </summary>
        private void LendingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check so so it was not a button with set to DialogResult.OK (btnOK) that was clicked
            if (DialogResult != DialogResult.OK)
            {
                // Double check with user if she/he really want to close without saving
                DialogResult result = MessageBox.Show("You gone lose every changes.\nDo really want close form?",
                    "Confirmation", MessageBoxButtons.OKCancel);

                // If it was cancel, prevent closing
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
            // If it was DialogResult.OK set button (btnOK) and it's not okey to close, prevent that. 
            else if (m_doNotClose && DialogResult == DialogResult.OK)
                e.Cancel = true;
        }
    }
}
