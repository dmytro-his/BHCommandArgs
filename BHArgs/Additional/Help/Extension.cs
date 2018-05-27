using System;
using System.Collections.Generic;

namespace BeHappy.Args
{
    public static class Extension
    {
        public static void ShowHelp(this IEnumerable<Arg> args, string forWhat, string addition, bool verbose)
        {
            ArgsHelper.ShowHelp(args, forWhat, verbose);
        }

        public static void ForEach(this IEnumerable<Arg> args, Action<Arg> action)
        {
            foreach (var arg in args)
                action(arg);
        }
    }
}
