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

namespace VT2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string getkey(string s) {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < s.Length; i++) {
                if (i == 28) break;
                if (s[i] == '1' || s[i] == '0') res.Append(s[i]);
            }
            while (res.Length < 28) { res.Append('0');}
            return res.ToString();
        }

        private int getnext(int x, int y) {
            int res;
            res=x ^ y;
            return res;
        }

        private string shl(string s) {
            StringBuilder res = new StringBuilder(s.Substring(1)+"0"); 
            return res.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = getkey(textBox1.Text);
            string temp = text;
            int newbit;
            byte[] arr = new byte[1];
            FileStream filein = new FileStream(openFileDialog1.FileName , FileMode.Open);
            FileStream fileout = new FileStream(openFileDialog2.FileName, FileMode.Open);
            BinaryReader reader = new BinaryReader(filein);
            BinaryWriter writer = new BinaryWriter(fileout);
            StringBuilder resstr = new StringBuilder();
            try
            {
                while (true)
                {
                    StringBuilder keybuilder = new StringBuilder();
                    int _byte = (int)reader.ReadByte();
                    arr[0] = (byte)_byte;
                    textBox2.AppendText(Encoding.ASCII.GetChars(arr)[0].ToString());
                    for(int i = 0; i < 8; i++) { 
                        newbit = (temp[0] - '0') ^ (temp[25] - '0');
                        keybuilder.Append(temp[0]);
                        temp = shl(temp);
                        StringBuilder tmp = new StringBuilder(temp);
                        tmp.Remove(27, 1);
                        tmp.Append(newbit);
                        temp = tmp.ToString();
                    }
                    string k = keybuilder.ToString();
                    int sec = Convert.ToByte(k, 2);
                    int res = _byte ^ sec;
                    keybuilder.Clear();
                    arr[0] = (byte)res;
                    writer.Write((byte)res);
                    textBox3.AppendText(Encoding.ASCII.GetChars(arr)[0].ToString());
                }
            }
            catch (EndOfStreamException){}
            filein.Close();
            fileout.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "\u203A\u20AC\u2039\u2022\u00A0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }
    }
}
