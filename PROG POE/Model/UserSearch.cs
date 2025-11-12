using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{
    /// <summary>
    /// Represents a user search pattern for tracking and analyzing user search behavior
    /// Used for generating personalized recommendations in the Events system
    /// </summary>
    public class UserSearch
    {
        /// <summary>
        /// The search term entered by the user
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// The category selected by the user for filtering
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The date and time when the search was performed
        /// </summary>
        public DateTime SearchDate { get; set; }

        /// <summary>
        /// The number of times this specific search has been performed
        /// </summary>
        public int SearchCount { get; set; }

        /// <summary>
        /// Default constructor initializes with default values
        /// </summary>
        public UserSearch()
        {
            SearchDate = DateTime.Now;
            SearchCount = 1;
        }

        /// <summary>
        /// Constructor with search parameters
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <param name="category">The category filter</param>
        public UserSearch(string searchTerm, string category)
        {
            SearchTerm = searchTerm;
            Category = category;
            SearchDate = DateTime.Now;
            SearchCount = 1;
        }

        /// <summary>
        /// Increments the search count and updates the search date
        /// </summary>
        public void IncrementCount()
        {
            SearchCount++;
            SearchDate = DateTime.Now;
        }

        /// <summary>
        /// Creates a unique key for this search combination for dictionary storage
        /// </summary>
        /// <returns>A unique string key</returns>
        public string GetSearchKey()
        {
            return $"{SearchTerm}_{Category}";
        }

        /// <summary>
        /// Returns a string representation of the search
        /// </summary>
        /// <returns>Formatted string with search details</returns>
        public override string ToString()
        {
            return $"Search: '{SearchTerm}' | Category: {Category} | Count: {SearchCount} | Date: {SearchDate:g}";
        }
    }
}



