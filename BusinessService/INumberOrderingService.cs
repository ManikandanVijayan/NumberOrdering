using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService
{
    public interface INumberOrderingService
    {
        /// <summary>
        /// Save sorted numbers to file.
        /// </summary>
        /// <param name="numberList">Number list to be sorted</param>
        /// <returns>Saved or not</returns>
        bool SortNumbers(List<int> numberList);

        /// <summary>
        /// Load content of latest saved file 
        /// </summary>
        /// <returns>Sorted List</returns>
        string[] GetLatestFileContent();
    }
}
