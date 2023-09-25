using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManualResetEvent[] events = new ManualResetEvent[3];
            events[0] = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(CreateNumber, events[0]);
            events[0].WaitOne();
            events[1] = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(ProductNumber, events[1]);
            events[2] = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(SumNumber, events[2]);
            Console.ReadKey();
        }
        static void CreateNumber(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;
            Random rnd = new Random();
            StreamWriter write = new StreamWriter("Number.txt");
            for (int i =0; i < 4; i++)
            {
                write.WriteLine($"{rnd.Next(100)}");
                write.WriteLine($"{rnd.Next(100)}");
            }
            write.Close();
            Console.WriteLine("File Number is created");
            ev.Set();
        }
        static void ProductNumber(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;

            StreamWriter write = new StreamWriter("Product.txt");
            StreamReader read = new StreamReader("Number.txt");
            for (int i = 0; i < 4; i++)
            {
                write.WriteLine($"{int.Parse(read.ReadLine()) * int.Parse(read.ReadLine())}");
            }
            write.Close();
            read.Close();
            Console.WriteLine("File Product is created");
            ev.Set();

        }
        static void SumNumber(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;

            StreamWriter write = new StreamWriter("Sum.txt");
            StreamReader read = new StreamReader("Number.txt");
            for (int i = 0; i < 4; i++)
            {
                write.WriteLine($"{int.Parse(read.ReadLine()) + int.Parse(read.ReadLine())}");
            }
            write.Close();
            read.Close();
            Console.WriteLine("File Sum is created");
            ev.Set();
        }
    }
}