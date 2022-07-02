using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Examples
{
    public static class Sort
    {
        public static void SelectionSort_Asscending<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[i]) < 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

        }


        public static void SelectionSort_Descending<T>(T[] array) where T : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[i]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }



        }

        public static void InsertionSort<T>(T[] arr) where T : IComparable
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int j = i;

                while (j > 0 && arr[j].CompareTo(arr[j - 1]) < 0)
                {
                    //swap
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;

                    j--;
                }
            }
        }

        public static void BubbleSort<T>(T[] arr) where T : IComparable
        {
            for (int i = 0; i < arr.Length; i++)
            {
                bool isChanged = false;
                for (int j = 0; j < arr.Length-1; j++)
                {
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        //swap
                        T temp = arr[j];
                        arr[j] = arr[j+1];
                        arr[j+1] = temp;

                        isChanged = true;
                    }


                }

                if (!isChanged)
                    break;

            }
        }

        public static void QuickSort<T>(T[] arr,int left,int right) where T : IComparable
        {
            int pivot = -1;
           

            if (left < right)
            {
                pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort<T>(arr, left, pivot - 1);
                }

                if (pivot + 1 < right)
                {
                    QuickSort<T>(arr,pivot+1, right);
                }
            }

        }

        private static int Partition<T>(T[] arr, int left, int right) where T : IComparable
        {
            T pivot = arr[left];

            while (true)
            {
                while (arr[left].CompareTo(pivot)<0)
                {
                    left++;
                }

                while (arr[right].CompareTo(pivot)>0)
                {
                    right--;
                }

                if (left < right)
                {
                    T temp = arr[right];
                    arr[right] = arr[left];
                    arr[left] = temp;
                }
                else
                    return right;

                
                   
            }
        }
    }
}
