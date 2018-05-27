using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeHappy.Args
{
    static class HelpCreator
    {  
        internal static List<string> GetHelp(IEnumerable<Arg> args, bool verbose)
        {
            if (args.Count() == 0)
                return new List<string>();
            List<string> helpText = new List<string>();
            string indent = String.Format("Usage: {0}", Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName));
            string line = indent;
            args.ForEach(arg =>
            {
                string argShortHelp = " " + HelpCreator.GetShortHelp(arg);
                if ((line + argShortHelp).Length > 80)
                {
                    helpText.Add(line);
                    line = new string(' ', indent.Length);
                }
                line += argShortHelp;
            });
            if (line != new string(' ', indent.Length))
                helpText.Add(line);
            helpText.Add(String.Empty);
            helpText.Add("Options:");

            int landslide0 = args.Max(arg => arg.ShortDesignation.Length);
            int landslide1 = args.Max(arg => arg.ToolTip.Length);
            int landslide2 = args.Max(arg => arg.Help.Length);
            string format = GetFormat("   -{0, @landslide0}{1, @landslide1}{2, @landslide2}: {3}", landslide0+1, landslide1+4, landslide2);
            args.ForEach(arg =>
            {
                string argLongHelp = HelpCreator.GetLongHelp(arg, format);
                helpText.Add(argLongHelp);
            });

            if (verbose)
            {
                args.ForEach(arg =>
                {
                    string argVerboseHelp = HelpCreator.GetVerboseText(arg);
                    if (argVerboseHelp != null)
                        helpText.Add(argVerboseHelp);
                });
            }
            return helpText;
        }

        internal static string GetShortHelp(Arg arg)
        {
            string shortHelp = String.Format("-{0}{1}", arg.ShortDesignation, String.IsNullOrWhiteSpace(arg.ToolTip) ? String.Empty : " " + arg.ToolTip);
            if (!arg.IsRequired)
                shortHelp = "[" + shortHelp + "]";
            return shortHelp;

        }

        internal static string GetLongHelp(Arg arg, string format)
        {
            return String.Format(format, arg.ShortDesignation, arg.ToolTip, arg.Help, arg);
        }

        internal static string GetFormat(string format, params int[] landslides)
        {
            for (int i = 0; i < landslides.Length; i++)
            {
                format = format.Replace(String.Format("@landslide{0}", i), (-1 * landslides[i]).ToString());
            }
            return format;
        }
        
        internal static string GetVerboseText(Arg arg)
        {
              if (arg.ShortDesignation != arg.LongDesignation)
                  return String.Format("'{0}' means the same as '{1}'", arg.ShortDesignation, arg.LongDesignation);
            return null;
        }

    }
}

