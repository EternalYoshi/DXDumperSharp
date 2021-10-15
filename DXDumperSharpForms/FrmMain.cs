//By Eternal Yoshi v0.8X
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using System.Reflection;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml.Style;

namespace DXDumperSharpForms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        public string SPTFilename;
        public string TBLFilename;
        public string HexTemp;
        public string HexText;
        public string KanaText;
        public string[] Words;
        public long ASize, FSize;
        public int SOffset, FOffset, m, n, TempI, TLen, r, s, t, u, v, breakcounter;
        public byte[] Bytes;
        public byte[] HeaderWord;
        public List<int> Offsets;
        public int ArrLength;
        public string Delimiter = " F800";
        public string Delimiter2 = "][";
        public string EndWord;
        public bool Splitted;
        public int SplitArLen;
        private object Value { get; set; }


        private void btnDump_Click(object sender, EventArgs e)
        {
            //Checks for blank fields and invalid inputs.
            Regex RegHex = new Regex("^[a-fA-F0-9]+$");
            if (!RegHex.IsMatch(txtOffsetStart.Text) || txtOffsetStart.Text == "")
            {
                MessageBox.Show("I need legal hexadecimal values to read the starting offset.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!RegHex.IsMatch(txtOffsetFin.Text) || txtOffsetFin.Text == "")
            {
                MessageBox.Show("I need legal hexadecimal values to read the ending offset.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Simple way to check if files were chosen.
            if (SPTFilename == "" || SPTFilename == null)
            {
                MessageBox.Show("I need a file to dump from.", "Uhhhh", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (TBLFilename == "" || TBLFilename == null)
            {
                MessageBox.Show("I need a table file or text file to use for the dumping.", "Uhhhh", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!File.Exists(SPTFilename))
            {
                MessageBox.Show("That file doesn't appear to exist.");
                return;
            }

            //Disables the controls to prevent tampering.
            txtOffsetStart.ReadOnly = true;
            txtOffsetFin.ReadOnly = true;
            btnDump.Enabled = false;
            btnFileOpen.Enabled = false;
            btnOpenTbl.Enabled = false;

            int pos = SPTFilename.LastIndexOf("\\") + 1;
            string shortname = SPTFilename.Substring(pos, SPTFilename.Length - pos);

            //Now to open the file and go to the starting offset.
            BinaryReader br = new BinaryReader(File.OpenRead(OFDialog1.FileName));
            StreamReader sr = new StreamReader(File.OpenRead(OFDialog2.FileName));


            SOffset = int.Parse(txtOffsetStart.Text, System.Globalization.NumberStyles.HexNumber);
            FOffset = int.Parse(txtOffsetFin.Text, System.Globalization.NumberStyles.HexNumber);
            br.BaseStream.Position = SOffset;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(SPTFilename + " - Table.xlsx")))
            {

                var WorkSheet = package.Workbook.Worksheets.Add(shortname);

                //WorkSheet.Cells[1, 0].Value = "";
                //Formatting the style of the 1st column(hopefully).
                using (var range = WorkSheet.Cells[1, 1, 1, 11])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightCyan);
                    range.Style.Font.Size = 14;
                }

                WorkSheet.Cells[1, 1].Value = " ";
                WorkSheet.Cells[1, 2].Value = "Offset";
                WorkSheet.Cells[1, 3].Value = "RawValues";
                WorkSheet.Cells[1, 4].Value = "Original Text";
                WorkSheet.Cells[1, 5].Value = "Google Translate";
                WorkSheet.Cells[1, 6].Value = "Put Community Translations Here!";
                WorkSheet.Cells[1, 7].Value = "___ Translations";
                WorkSheet.Cells[1, 8].Value = "Notes/Concerns";
                WorkSheet.Cells[1, 9].Value = "Proposed String";
                WorkSheet.Cells[1, 10].Value = "Difference in Entry Length";

                WorkSheet.Column(1).Width = 30;
                WorkSheet.Column(2).Width = 15;
                WorkSheet.Column(3).Width = 40;
                WorkSheet.Column(4).Width = 50;
                WorkSheet.Column(5).Width = 60;
                WorkSheet.Column(6).Width = 50;
                WorkSheet.Column(7).Width = 70;
                WorkSheet.Column(8).Width = 60;
                WorkSheet.Column(9).Width = 60;
                WorkSheet.Column(10).Width = 60;

                WorkSheet.Row(1).Height = 60;
                WorkSheet.Row(2).Height = 30;
                WorkSheet.Row(3).Height = 30;
                WorkSheet.Row(4).Height = 30;
                WorkSheet.Row(5).Height = 30;
                WorkSheet.Row(6).Height = 30;
                WorkSheet.Row(7).Height = 30;
                WorkSheet.Row(8).Height = 30;
                WorkSheet.Row(9).Height = 30;
                WorkSheet.Row(10).Height = 30;

                package.Workbook.Properties.Title = "Translation Sheet - " + SPTFilename;
                package.Workbook.Properties.Author = "Eternal Yoshi";
                package.Workbook.Properties.Comments = "Generated by Eternal Yoshi's DXDumperSharpForms program.";



                //Prepares the Byte Array.            
                ArrLength = FOffset - SOffset;
                Bytes = new byte[ArrLength];
                Offsets = new List<int>();
                r = SOffset;

                string m = txtOffsetStart.Text;
                int n = 0;
                int o = 0;
                WorkSheet.Cells[2, 2].Value = m;

                //Reads the Bytes and stores it in Bytes array.
                for (int q = 0; q < ArrLength; q++)
                {
                    Bytes[q] = br.ReadByte();
                    r++;
                    /*
                    if(Bytes[q] == 0xF8)
                    {

                    }
                    */
                    br.BaseStream.Position = r;
                }

                //Iterates through Bytes Array and makes words to prepare for printing out the bytes.
                int t = 0;
                u = 0;
                Words = new string[(ArrLength / 2)];
                for (int s = 0; s < ArrLength; s++)
                {

                    if (t % 2 == 1 && t != 0)
                    {
                        HexTemp = HexTemp + Bytes[s].ToString("X2");
                        HexText = HexText + HexTemp + " ";
                        Words[u] = HexTemp;
                        u++;
                    }
                    else
                    {
                        HexTemp = Bytes[s].ToString("X2");
                    }
                    t++;
                }

                //Decodes the Hex and outputs to text.
                for (int w = 0; w < Words.Length; w++)
                {
                    KanaText = KanaText + SwitchCaser.Decipherer(Words[w], SPTFilename, sr, WorkSheet);
                }

                /*
                //Decodes the Hex and outputs to text.
                for (int w = 0; w < Words.Length; w++)
                {
                    KanaText = KanaText + SwitchCaser.Decipherer(Words[w], SPTFilename,sr);
                }
                 */

                //Checks for delimiter and splits if present.
                string Delimiter = " F800 ";
                string[] Txtentries = HexText.Split(new string[] { Delimiter }, StringSplitOptions.None);
                int SplitArLen = Txtentries.Length;
                string Delimiter2 = "][";
                string[] KanaEntries = KanaText.Split(new string[] { Delimiter2 }, StringSplitOptions.None);
                n = int.Parse(txtOffsetStart.Text, System.Globalization.NumberStyles.HexNumber);

                for (int v = 0; v < Txtentries.Length; v++)
                {

                    WorkSheet.Cells[(2 + v), 3].Value = Txtentries[v];
                    o = GetWordCount(o, Txtentries, v);
                    n = n + (o * 2) + 2;
                    m = n.ToString("X");
                    WorkSheet.Cells[(3 + v), 2].Value = m;
                    o = 0;
                }

                for (int w = 0; w < KanaEntries.Length; w++)
                {

                    WorkSheet.Cells[(2 + w), 4].Value = KanaEntries[w];
                    WorkSheet.Row(2 + w).Height = 30;

                }

                /*
                //Prints stuffs to Output.txt.
                using (StreamWriter wrtTxt = new StreamWriter(SPTFilename + "_OUT.txt"))
                {
                    wrtTxt.WriteLine("File Read: " + SPTFilename);
                    wrtTxt.WriteLine("Table Used: " + TBLFilename);
                    wrtTxt.WriteLine("\nRead from " + txtOffsetStart.Text + " to " + txtOffsetFin.Text + "\n");
                    for(int vi = 0; vi < Txtentries.Length; vi++)
                    {
                        wrtTxt.WriteLine(Txtentries[vi]);
                        wrtTxt.WriteLine(KanaEntries[vi] + "\n");
                    }
                    //wrtTxt.WriteLine(HexText);
                    //wrtTxt.WriteLine(KanaText + "\n");
                    wrtTxt.Close();
                    HexText = "";
                    KanaText = "";
                    Array.Clear(Bytes,0,Bytes.Length);
                    Array.Clear(Words, 0, Words.Length);
                    Array.Clear(Txtentries,0,Txtentries.Length);
                    Array.Clear(KanaEntries, 0, KanaEntries.Length);
                }
                */

                WorkSheet.Cells[WorkSheet.Dimension.Address].AutoFitColumns();
                package.Save();

            }

            //Reenables the controls.
            txtOffsetStart.ReadOnly = false;
            txtOffsetFin.ReadOnly = false;
            btnDump.Enabled = true;
            btnFileOpen.Enabled = true;
            btnOpenTbl.Enabled = true;
        }

        //public string SPTfilename, HStart, HFinish, Temp, HexTemp;
        //public byte[] RawBytes;
        //public short TempWord;


        //This lets us use the dilogue without having to paste this within each button's function.
        OpenFileDialog OFDialog1 = new OpenFileDialog();
        OpenFileDialog OFDialog2 = new OpenFileDialog();

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            //This is where the alloted file extensions are chosen.
            OFDialog1.Filter = "All Supported formats|*.spt;*.SPT;*.*|Dokapon DX Event Archive |*.spt;*.SPT|All files| *.*";
            if (OFDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SPTFilename = OFDialog1.FileName;
                    lblOpenedSptFile.Text = SPTFilename;

                    BinaryReader hd = new BinaryReader(File.OpenRead(OFDialog1.FileName));
                    hd.BaseStream.Position = 0;
                    HeaderWord = new byte[2];
                    HeaderWord[0] = hd.ReadByte();
                    hd.BaseStream.Position = 1;
                    HeaderWord[1] = hd.ReadByte();
                    EndWord = HeaderWord[0].ToString("X2");
                    EndWord = EndWord + HeaderWord[1].ToString("X2");
                    txtOffsetFin.Text = EndWord;
                    hd.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Unable to access the file. Maybe it's already in use by something else?", "Cannot open this.");
                    return;
                }
            }
        }

        private void btnOpenTbl_Click(object sender, EventArgs e)
        {
            //This is where the alloted file extensions are chosen.
            OFDialog2.Filter = "All Supported formats|*.txt;*.tbl|Text File|*.txt|Table file|*.tbl";
            if (OFDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TBLFilename = OFDialog2.FileName;
                    lblOpenedTableFile.Text = TBLFilename;
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Unable to access the file. Maybe it's already in use by something else?", "Cannot open this.");
                    return;
                }
            }
        }

        private int GetWordCount(int v, string[] str, int w)
        {
            int index = 0;

            // skip whitespace until first word
            while (index < str[w].Length && char.IsWhiteSpace(str[w][index]))
                index++;

            while (index < str[w].Length)
            {
                // check if current char is part of a word
                while (index < str[w].Length && !char.IsWhiteSpace(str[w][index]))
                    index++;

                v++;

                // skip whitespace until next word
                while (index < str[w].Length && char.IsWhiteSpace(str[w][index]))
                    index++;
            }
            return v;
        }

    }
}
