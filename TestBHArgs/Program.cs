using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeHappy.Args;
using BeHappy.Args.Generics;

namespace TestBHArgs
{
    class Program
    {
        static ArgFlag Help = new ArgFlag(false, "?", "help", "show Help");
        static ArgFlag Verbose = new ArgFlag(false, "v", "verbose", "toggle verbose");
        static Arg<string> FileName = new Arg<string>("Default.txt", "f", "file", "File Name", "<Path.txt>");

        static void Main(string[] args)
        {
            if (ArgsHelper.ReadArgs() == 0)
            {
                Console.WriteLine("nothing to do");
                return;
            }
            if(Help.IsUsed)
                ArgsHelper.ShowHelp("Test BHArgs", (bool)Verbose);

            if (FileName.IsUsed)
                DoSmtWithFile(FileName);
            Console.ReadKey();
        }

        private static void DoSmtWithFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
