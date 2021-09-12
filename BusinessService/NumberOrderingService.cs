using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessService
{
    /// <summary>
    /// NumberOrderingService.
    /// </summary>
    public class NumberOrderingService : INumberOrderingService
    {
        /// <summary>
        /// Save sorted numbers to file.
        /// </summary>
        /// <param name="numberList">Number list to be sorted</param>
        /// <returns>Saved or not</returns>
        public bool SortNumbers(List<int> numberList)
        {
            try
            {
                if(numberList != null && numberList.Count > 0)
                {
                    string workingDirectory = Environment.CurrentDirectory;
                    string path = Path.Combine(workingDirectory, "result");
                    var orderList = numberList;

                    //Quick Sort with time performance
                    var startTime = DateTime.Now;
                    var numberOrderedList = QuickSort(numberList, 0, numberList.Count - 1);
                    var endTime = DateTime.Now;
                    var timeDiffQuickSort = endTime.Subtract(startTime);

                    //Bubble Sort with time performance
                    var startTimeBubbleSort = DateTime.Now;
                    var numberListSorted = BubbleSort(orderList);
                    var endTimeBubbleSort = DateTime.Now;
                    var timeDiffBubbleSort = endTimeBubbleSort.Subtract(startTimeBubbleSort);

                    if (!Directory.Exists(path))
                    {
                        // Create directory  if it doesn't exist
                        Directory.CreateDirectory(path);
                    }

                    // Save Sorted Number list to a file
                    var datetimeFileCreated = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    string resultFileName = "results_" + datetimeFileCreated + ".txt";
                    string timeFileName = "timeperformance_" + datetimeFileCreated + ".txt";

                    string filePath = Path.Combine(path, resultFileName);
                    var orderContent = String.Join(" ", numberOrderedList.ToArray());
                    File.WriteAllText(filePath, orderContent);

                    // Save time performance of each sort operation to a file
                    string performanceFilePath = Path.Combine(path, timeFileName);
                    var timeDiffContent = "QuickSort time taken:" + timeDiffQuickSort + "\n" + "BubbleSort time taken:" + timeDiffBubbleSort;
                    File.WriteAllText(performanceFilePath, timeDiffContent);
                    return true;
                }
                else
                {
                    throw new Exception("No Numbers to sort");
                }
                
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// BubbleSort operation.
        /// </summary>
        /// <param name="orderList">Number list to be sorted</param>
        /// <returns>sorted list</returns>
        private List<int> BubbleSort(List<int> orderList)
        {
            int temp;
            for (int j = 0; j <= orderList.Count - 2; j++)
            {
                for (int i = 0; i <= orderList.Count - 2; i++)
                {
                    //comparing two adjascent numbers
                    if (orderList[i] > orderList[i + 1])
                    {
                        //assign smallest element to temp and
                        //assign greater valued element in next index 
                        temp = orderList[i + 1];
                        orderList[i + 1] = orderList[i];
                        orderList[i] = temp;
                    }
                }
            }
            return orderList;
        }

        /// <summary>
        /// QuickSort operation.
        /// </summary>
        /// <param name="orderList">Number list to be sorted</param>
        /// <param name="left">left</param>
        /// <param name="right">right</param>
        /// <returns>sorted list</returns>
        private List<int> QuickSort(List<int> numberList, int left, int right)
        {
            if (left < right)
            {
                // pivot index
                int pivot = Partition(numberList, left, right);

                if (pivot > 1)
                {
                    QuickSort(numberList, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(numberList, pivot + 1, right);
                }
            }
            return numberList;
        }

        /// <summary>
        /// QuickSort pivot operation.
        /// </summary>
        /// <param name="numberList">Number list to be sorted</param>
        /// <param name="left">left index</param>
        /// <param name="right">right index</param>
        /// <returns>index position</returns>
        private static int Partition(List<int> numberList, int left, int right)
        {
            //setting the first element as pivot element
            int pivot = numberList[left];
            while (true)
            {
                //if pivot element is greater than element of left , increase left index
                while (numberList[left] < pivot)
                {
                    left++;
                }
                //if pivot element is smaller than element of right , decrease right index
                while (numberList[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (numberList[left] == numberList[right]) return right;

                    //swap elements with greater on right and smaller on left 
                    int temp = numberList[left];
                    numberList[left] = numberList[right];
                    numberList[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }

        /// <summary>
        /// Load content of latest saved file 
        /// </summary>
        /// <returns>Sorted List</returns>
        public string[] GetLatestFileContent()
        {
            try
            {
                string workingDirectory = Environment.CurrentDirectory;
                string path = Path.Combine(workingDirectory, "result");
                var directory = new DirectoryInfo(path);

                //get latest created result file
                var latestFileContent = directory.GetFiles("*results_*")
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();
                if(latestFileContent == null)
                {
                    throw new Exception("No files found");
                }
                else
                {
                    //read content from latest created file
                    var orderedList = File.ReadAllLines(latestFileContent.ToString());
                    return orderedList;
                }

            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
