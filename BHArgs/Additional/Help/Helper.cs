using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace BeHappy.Args
{
    public class ArgsHelper
    {
        public static void ShowHelp(IEnumerable<Arg> args, string forWhat, bool verbose)
        {
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            //string version = fvi.FileVersion;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string title = String.Format("{0} (version: {1})", forWhat, version);
            List<string> helpText = new List<string>();

            helpText.Add(title);
            helpText.Add(String.Empty);
            helpText.AddRange(HelpCreator.GetHelp(args, verbose));


            if (Common.HasConsole())
                helpText.ForEach(Console.Error.WriteLine);
            else
            {
                string text = string.Empty;
                helpText.ForEach(e => text += e + Environment.NewLine);
                MessageBox.Show(text, title);
            }
        }

        public static void ShowHelp(string forWhat = null, bool verbose = false)
        {
            StackTrace st = new StackTrace();
            var type = st.GetFrame(st.FrameCount - 1).GetMethod().ReflectedType;
            ShowHelp(type, forWhat, verbose);
        }

        public static void ShowHelp(Type type, string forWhat = null, bool verbose = false)
        {
            List<Arg> args = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).Select(e => e.GetValue(null)).OfType<Arg>().ToList();
            if (forWhat == null)
                forWhat = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            ShowHelp(args, forWhat, verbose);
        }



        public static int ReadArgs()
        {
            StackTrace st = new StackTrace();
            var type = st.GetFrame(st.FrameCount - 1).GetMethod().ReflectedType;

            return ReadArgs(type);
        }
        
        public static int ReadArgs(Type type)
        {
            string[] args = Environment.GetCommandLineArgs();
            List<Arg> argVariables = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).Select(e => e.GetValue(null)).OfType<Arg>().ToList();

            int affectedCount = 0;
            for (int i = 0; i < args.Length; i++)
            {
                foreach (var arg in argVariables)
                {
                    if (arg.Check(ref i, args))
                    {
                        affectedCount++;
                        break;
                    }
                }
            }
            return affectedCount;
        }

        public static int ReadArgs(IEnumerable<Arg> argVariables)
        {
            string[] args = Environment.GetCommandLineArgs();

            int affectedCount = 0;
            for (int i = 0; i < args.Length; i++)
            {
                foreach (var arg in argVariables)
                {
                    if (arg.Check(ref i, args))
                    {
                        affectedCount++;
                        break;
                    }
                }
            }
            return affectedCount;
        }
    }
}
