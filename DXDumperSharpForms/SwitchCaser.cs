//By Eternal Yoshi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using System.Reflection;
using OfficeOpenXml;

namespace DXDumperSharpForms
{
    class SwitchCaser
    {

        //public int linecounter;
        //public string kanachar;

        public static string Decipherer(string v, string tblname, StreamReader srtbl, ExcelWorksheet xlsheet)
        {

            string kanachar = "\n";
            int linecounter = 0;
            if (v == "0000" || v == "6000" || v == "7000" || v == "8000" || v == "9000")
            {
                kanachar = " ";
                return kanachar;
            }

            while (!srtbl.EndOfStream)
            {
                var line = srtbl.ReadLine();
                if (String.IsNullOrEmpty(line)) continue;
                if (line.IndexOf(v, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    kanachar = line;
                    kanachar = kanachar.Remove(0, 5);
                    srtbl.BaseStream.Position = 0;

                    if (kanachar == "F800")
                    {
                        kanachar = "";
                    }

                    if (kanachar == "00=")
                    {
                        kanachar = "ァ";
                    }

                    return kanachar;
                }
                linecounter++;
            }
            //If the variable is not found in table, it just prints out the word inside brackets instead.
            if (kanachar == null || kanachar == "\n")
            {
                kanachar = " [" + v + "] ";
                srtbl.BaseStream.Position = 0;
                return kanachar;
            }
            //kanachar = " [" + v + "] ";
            return kanachar;

        }
    }
}
