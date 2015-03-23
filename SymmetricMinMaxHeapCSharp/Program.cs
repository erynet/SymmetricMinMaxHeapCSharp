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
            SymmetricMinMaxHeap heap = new SymmetricMinMaxHeap();

            UInt64[] sources = { 30, 10, 29, 11, 28, 12, 27, 13, 26, 14, 25, 15, 24, 16, 23, 17, 22, 18, 21, 19, 20, 40, 50, 60, 55, 45, 35};

            System.Console.WriteLine("Insert Test\n");
            foreach (UInt64 s in sources)
            {
                //heap.
                System.Console.WriteLine("s : {0}", s);
                System.Console.WriteLine("--- Before ---");
                System.Console.WriteLine("heap.Size : {0}", heap.Size);
                System.Console.WriteLine("heap.Min : {0}", heap.Min);
                System.Console.WriteLine("heap.Max : {0}", heap.Max);
                ViewArray(heap);

                heap.Insert(s);
                System.Console.WriteLine("--- After ---");
                System.Console.WriteLine("heap.Size : {0}", heap.Size);
                System.Console.WriteLine("heap.Min : {0}", heap.Min);
                System.Console.WriteLine("heap.Max : {0}", heap.Max);
                ViewArray(heap);

                System.Console.WriteLine();
                //System.Console.ReadKey();
            }

            System.Console.WriteLine("Delete Min / Max Test\n");

            //for (int i = 0; i < sources.Length; i++)
            while (heap.Size > 0)
            {
                //System.Console.WriteLine("Before Delete Min");
                //System.Console.WriteLine("Current Min : {0}", heap.Min);
                //ViewArray(heap);
                //heap.DeleteMin();
                //System.Console.WriteLine("After Delete Min");
                //System.Console.WriteLine("Current Min : {0}", heap.Min);
                //ViewArray(heap);

                System.Console.WriteLine("Before Delete Max");
                System.Console.WriteLine("Current Max : {0}", heap.Max);
                ViewArray(heap);
                heap.DeleteMax();
                System.Console.WriteLine("After Delete Max");
                System.Console.WriteLine("Current Min : {0}", heap.Max);
                ViewArray(heap);

                System.Console.WriteLine();
                //System.Console.ReadKey();
            }
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
    }
}
