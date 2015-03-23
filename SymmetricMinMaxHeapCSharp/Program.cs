using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SymmetricMinMaxHeapCSharpLib;

namespace SymmetricMinMaxHeapCSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SymmetricMinMaxHeap Heap = new SymmetricMinMaxHeap();
            SymmetricMinMaxHeap AscHeap = new SymmetricMinMaxHeap();
            SymmetricMinMaxHeap DscHeap = new SymmetricMinMaxHeap();

            UInt64[] sources =    { 6,  4, 46, 11, 10, 45, 13, 24, 
                                   48, 20, 35, 62, 64, 37, 50, 22, 
                                   25, 27, 36, 44, 38, 39, 17,  5, 
                                   19, 51,  2,  7, 52, 30, 58, 33, 
                                    1, 49, 29, 18, 42, 55, 53, 60, 
                                   57, 40,  3, 15, 21, 54, 14, 61, 
                                   59,  8, 12, 16, 56, 34, 32, 63, 
                                   41, 28, 26, 47, 23, 43, 31, 9 };

            UInt64[] AscSorted = new UInt64[64];
            UInt64[] DscSorted = new UInt64[64];

            System.Console.Write(System.Environment.NewLine);
            System.Console.WriteLine("##### 64-Digit Random Sequence #####");
            System.Console.Write(System.Environment.NewLine);

            PrintSample(sources);

            System.Console.Write(System.Environment.NewLine);
            System.Console.Write("Before Insert : ");
            ViewArray(Heap);

            foreach (UInt64 s in sources)
            {
                Heap.Insert(s);
                AscHeap.Insert(s);
                DscHeap.Insert(s);
            }

            System.Console.Write(" After Insert : ");
            ViewArray(Heap);
            System.Console.Write(" After Clear  : ");
            Heap.Clear();
            ViewArray(Heap);

            System.Console.Write(System.Environment.NewLine);
            System.Console.WriteLine("##### Sort using SymmetricMinMaxHeap #####");
            System.Console.Write(System.Environment.NewLine);

            for (int i = 0; i < 64; i++)
            {
                AscSorted[i] = AscHeap.Min;
                AscHeap.DeleteMin();
                DscSorted[i] = DscHeap.Max;
                DscHeap.DeleteMax();
            }

            System.Console.WriteLine("----- Ascending -----");
            PrintSample(AscSorted);
            System.Console.WriteLine("----- Decending -----");
            PrintSample(DscSorted);
        }

        static void ViewArray(SymmetricMinMaxHeap heap)
        {
            System.Console.Write("[ ");
            for (int i = 0; i < heap.Size + 2; i++)
            {
                System.Console.Write("{0}", heap.Array[i]);
                if (i != (heap.Size + 1))
                    System.Console.Write(", ");
            }
            System.Console.WriteLine(" ]");
        }

        static void PrintSample(UInt64[] sources)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    System.Console.Write("{0,2} ", sources[(i * 8) + j]);
                }
                System.Console.WriteLine("");
            }
        }
    }
}
