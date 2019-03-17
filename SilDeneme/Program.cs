using DataManager;
using DataModel;
using System;
using System.Collections.Generic;

namespace SilDeneme
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MovieManager mMan = new MovieManager();
            List<Movies> list = mMan.GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].Title);
            }
            Console.ReadKey();
        }
    }
}
