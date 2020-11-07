using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task task = new Task(SimpleMethod);
            //task.Start();

            //Task<string> taskThatReturnSomething = new Task<string>(SaySomethin);
            //taskThatReturnSomething.Start();
            //taskThatReturnSomething.Wait();

            //Console.WriteLine(taskThatReturnSomething.Result);


            Task<string> taskFactory = Task.Factory.StartNew<string>
                (() => GetPosts("https://jsonplaceholder.typicode.com/posts"));


            SomthingElse();

            //taskFactory.Wait();
            try
            {
                Console.WriteLine(taskFactory.Result);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static void SomthingElse()
        {
            //DUmmy implementation
        }

        private static string GetPosts(string url)
        {
            //throw null;
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private static void SimpleMethod()
        {
            Console.WriteLine("Hello World");
            
        }

        private static string SaySomethin()
        {
            Thread.Sleep(2000);
            return "Hieeeeeee";
        }
    }
}
