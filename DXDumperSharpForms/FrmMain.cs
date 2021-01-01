//By Eternal Yoshi v0.8
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
        public int SOffset, FOffset, m, n, TempI, TLen, r,s,t,u,v, breakcounter;
        public byte[] Bytes;
        public int ArrLength;
        public string Delimiter = " F800";
        public string Delimiter2 = "][";
        public bool Splitted;
        public int SplitArLen;

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

            //Now to open the file and go to the starting offset.
            BinaryReader br = new BinaryReader(File.OpenRead(OFDialog1.FileName));
            StreamReader sr = new StreamReader(File.OpenRead(OFDialog2.FileName));

            
            SOffset = int.Parse(txtOffsetStart.Text, System.Globalization.NumberStyles.HexNumber);
            FOffset = int.Parse(txtOffsetFin.Text, System.Globalization.NumberStyles.HexNumber);
            br.BaseStream.Position = SOffset;

            //Prepares the Byte Array.            
            ArrLength = FOffset - SOffset;
            Bytes = new byte[ArrLength];
            r = SOffset;

            //Reads the Bytes and stores it in Bytes array.
            for (int q = 0; q < ArrLength; q++)
            {
                Bytes[q] = br.ReadByte();
                r++;
                br.BaseStream.Position = r;
            }

            //Iterates through Bytes Array and makes words to prepare for printing out the bytes.
            int t = 0;
            u = 0;
            Words = new string[(ArrLength/2)];
            for (int s=0;s<ArrLength;s++)
            {

                if(t % 2 == 1 && t != 0)
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
                KanaText = KanaText + SwitchCaser.Decipherer(Words[w], SPTFilename,sr);
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
                Array.Clear(Txtentries,0,Txtentries.Length);
                Array.Clear(KanaEntries, 0, KanaEntries.Length);
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
                }
                catch(UnauthorizedAccessException)
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



    }
}
