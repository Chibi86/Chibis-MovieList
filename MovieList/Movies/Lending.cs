// Movies/Lending.cs
// -----------------
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
    /// Lending
    /// -------
    /// Class to make Lending object of, methods to save, change, validate and formate values from/to GUI
    /// </summary>
    public class Lending
    {
        private bool m_lendingStatus;
        private string m_lendTo;
        private DateTime m_lendingDate;

        /// <summary>
        /// Default constractor - calling other constractor
        /// </summary>
        public Lending() : this(false, "", DateTime.Now) {}


        /// <summary>
        /// Constractor with other Lending object to copy
        /// </summary>
        /// <param name="oldLending">Lending object to copy</param>
        public Lending(Lending oldLending)
        {
            if(oldLending != null) // Validate
                m_lendingStatus = oldLending.LendingStatus;
                m_lendTo = oldLending.LendTo;
                m_lendingDate = oldLending.LendingDate;
        }

        /// <summary>
        /// Constractor with string for backup from file
        /// </summary>
        /// <param name="strLending">string with backup data</param>
        public Lending(string strLending)
        {
            string[] arrLending = strLending.Split(','); // Split string by char ","

            m_lendingStatus = bool.Parse(arrLending[0]); // Convert string to bool
            m_lendTo = arrLending[1];
            m_lendingDate = DateTime.Parse(arrLending[2]); // Convert string to datetime
        }

        /// <summary>
        /// Constrator with lending status, lend to and date
        /// </summary>
        /// <param name="newStatus">Lending status (bool)</param>
        /// <param name="newLendTo">Name on person that lend (string)</param>
        /// <param name="newDate">Lending date (datetime)</param>
        public Lending(bool newStatus, string newLendTo, DateTime newDate)
        {
            m_lendingStatus = newStatus;
            m_lendTo = newLendTo;
            m_lendingDate = newDate;
        }

        /// <summary>
        /// Properties for m_lendingStatus - get only
        /// </summary>
        public bool LendingStatus
        {
            get { return m_lendingStatus;  }
        }

        /// <summary>
        /// Properties for m_lendTo - get only
        /// </summary>
        public string LendTo
        {
            get { return m_lendTo; }
        }

        /// <summary>
        /// Properties for m_lendingDate - get only
        /// </summary>
        public DateTime LendingDate
        {
            get { return m_lendingDate; }
        }

        /// <summary>
        /// Validate name and lending status and add lending
        /// </summary>
        /// <param name="name">Name on person that lend</param>
        /// <returns>Validate status</returns>
        public bool LendOut(string name)
        {
            // Validate so it is not already lended and lendTo name is not empty
            if (m_lendingStatus || string.IsNullOrEmpty(name))
                return false;

            m_lendingStatus = true;
            m_lendTo = name;
            m_lendingDate = DateTime.Now; // Set lending date to now

            return true;
        }

        /// <summary>
        /// Remove lending
        /// </summary>
        /// <returns>Validate status</returns>
        public bool GetBack()
        {
            if(!m_lendingStatus) // Validate so we got a lend
                return false;

            m_lendingStatus = false; // Set status to false
            m_lendTo = string.Empty; // Remove lend to name

            return true;
        }

        /// <summary>
        /// Override ToString method to output lending information to string
        /// </summary>
        /// <returns>Formate string with lending info</returns>
        public override string ToString()
        {
            string strOut = "";

            // If lend out return "Yes", name and date
            if (m_lendingStatus)
                strOut = string.Format("{0} {1} - {2}", "Yes,", m_lendTo, m_lendingDate.ToString("yyyy-MM-dd"));
            // Otherwish only return "No"
            else
                strOut = "No";

            return strOut;
        }

        /// <summary>
        /// Return a string with lending data separet by "," for backup to file
        /// </summary>
        /// <returns>Lending data as a string</returns>
        public string GetBackupForFile()
        {
            return string.Format("{0},{1},{2}", m_lendingStatus, m_lendTo, m_lendingDate);
        }
    }
}
