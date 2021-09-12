using BusinessService;
using System;
using System.Collections.Generic;
using Xunit;

namespace NumberOrderingUnitTest
{
    /// <summary>
    /// NumberOrderingServiceTest
    /// </summary>
    public class NumberOrderingServiceTest
    {
        /// <summary>
        /// NumberOrderingService
        /// </summary>
        private NumberOrderingService _numOrderService = null;

        List<int> numberList = new List<int>() { 100, 2, 120 };

        public NumberOrderingServiceTest()
        {
            _numOrderService = new NumberOrderingService();
        }

        #region Save Sorted List

        [Fact]
        public void Task_SaveSortedList_Return_True()
        {
            //Act
            bool isSaved = _numOrderService.SortNumbers(numberList);

            //Assert
            Assert.True(isSaved, $"The list is sorted");
        }

        [Fact]
        public void Task_SaveSortedList_Return_Exception()
        { 
            //Act and Assert
            var ex = Assert.Throws<Exception>(() => _numOrderService.SortNumbers(null));
            Assert.Equal("No Numbers to sort", ex.Message);
        }
        #endregion Save Sorted List

        #region Get Sorted List
        [Fact]
        public void Task_GetLatestFileContent_Return_SortedList()
        {
            //Act
            var data = _numOrderService.GetLatestFileContent();
            string[] sortedList = { "2 100 120"};
            //Assert
            Assert.Equal(sortedList, data);
        }
        # endregion Get Sorted List
    }
}

