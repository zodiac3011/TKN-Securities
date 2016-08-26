using System;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.Security.AccessControl;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private static string publicKey, //sau khi hoá đổi
            privateKey,
            message,
            outputkey,
            DEprivateKey,
            DEmessage,
            DEpublicKey,
            outputmessage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Alphabet();
            MessageBox.Show("This is BETA VERSION");
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            privateKey = textBox13.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            hash = 0;
            ctlhash = 0;
            ctlpublic = 0;
            orgpublic = null;
            DEmessage = null;
            bytesub = null;
            messageascii = null;
            if (textBox13.Text.Trim() == string.Empty || textBox15.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter all the required details");
                return;
            }
            Decrypt();
            textBox14.Text = outputmessage;
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            publicKey = textBox15.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            publicKey = null;
            hash = 0;
            ctlhash = 0;
            ctlpublic = 0;
            orgpublic = null;
            messageascii = null;
            textBox12.Text = null;
            bytesub = null;
            outputkey = null;
            if (textBox11.Text.Trim() == string.Empty || textBox9.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter all the required details");
                return;
            }
            Encrypt();
            textBox12.Text = outputkey;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            message = textBox11.Text;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        BigInteger hash;
        private int ctlhash, ctlpublic;

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private string orgpublic, messageascii;
        private byte[] bytesub;

        private void ControlHash()
        {
            string subpriv = privateKey;
            BigInteger dk = 1;
            while (subpriv.Length != 0)
            {
                ctlhash = ctlhash + Convert.ToInt32(subpriv.Substring(0, 1));
                subpriv = subpriv.Remove(0, 1);
                if (subpriv.Length == 0 && ctlhash > (hash.ToString()).Length || messageascii.Length / 2 < ctlhash)
                {
                    dk = dk + 1;
                    subpriv = (Math.Sqrt(Convert.ToInt32(ctlhash))).ToString();
                    ctlhash = 0;
                }
                if(subpriv.Contains("."))
                {
                    subpriv = subpriv.Remove(subpriv.IndexOf("."), subpriv.Length - subpriv.IndexOf("."));
                }
            }
            ctlhash = Convert.ToInt32(Math.Sqrt(ctlhash));           
        }
        private void ByteControlHash()
        {
            string subpriv = privateKey;
            BigInteger dk = 1;
            while (subpriv.Length != 0)
            {
                ctlhash = ctlhash + Convert.ToInt32(subpriv.Substring(0, 1));
                subpriv = subpriv.Remove(0, 1);
                if (subpriv.Length == 0 && ctlhash > (hash.ToString()).Length || bytefile.Length / 2 < ctlhash)
                {
                    dk = dk + 1;
                    subpriv = (Math.Sqrt(Convert.ToInt32(ctlhash))).ToString();
                    ctlhash = 0;
                }
                if (subpriv.Contains("."))
                {
                    subpriv = subpriv.Remove(subpriv.IndexOf("."), subpriv.Length - subpriv.IndexOf("."));
                }
            }
            ctlhash = Convert.ToInt32(Math.Sqrt(ctlhash));
        }
        private void Ascii()
        {
            int e = 0; //biến đếm điều kiện array
            byte[] temp = Encoding.ASCII.GetBytes(message); //biến đổi message sang ascii
            while (e < temp.Length)
            {
                messageascii = messageascii + temp[e];
                e = e + 1;
            }
        }

        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {
            privateKey = textBox9.Text;
        }

        private void textBox11_TextChanged_1(object sender, EventArgs e)
        {
            message = textBox11.Text;
        }

        private void textBox12_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
        private OpenFileDialog inputfile = new OpenFileDialog();
        private bool chexist1, chexist2;
        private void button6_Click(object sender, EventArgs e)
        {            
            DialogResult result1 = inputfile.ShowDialog();
            string file = "";
            if (result1 == DialogResult.OK) // Test result.
            {
                file = inputfile.FileName;
                textBox19.Text = file;
            }
        }
        private string inputpath;
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            inputpath = textBox19.Text;           
        }
        private byte[] bytefile;
        private void button8_Click(object sender, EventArgs e)
        {
            if (inputfile.CheckFileExists == false || inputfile.CheckPathExists == false)
            {
                return;
            }
            bytefile = System.IO.File.ReadAllBytes(inputpath);
            Encrypt();           
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            privateKey = textBox17.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private string allist;
        private char[] alphabet;
        private void Alphabet()
        {
            allist =
            "0123456789abcdefghiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_-+=,./;'[]><|{}?:`~\n\"\\\t\v";
            var subal = new List<char>();
            while (allist.Length != 0)
            {
                    subal.Add(Convert.ToChar(allist.Substring(0, 1)));
                    allist = allist.Remove(0, 1);
                }
                alphabet = subal.ToArray();
        }
        private string[] original; 
        private int nogroup;
        private void Group()
        {
            int number = 0;
            var temporiginal = new List<string>();
            string subascii = messageascii;
            int asciilength = 0;
            if (messageascii.Length % 10 == 0)
            {
                var dividend = new List<Int32>();
                int j = 1;
                while (j <= messageascii.Length)
                {
                    if (messageascii.Length % j == 0)
                    {
                        dividend.Add(j);
                    }
                    j = j + 1;
                }
                nogroup = dividend[(dividend.Count / 2) - 1];
                number = messageascii.Length / nogroup;
            }
            if (messageascii.Length % 10 != 0)
            {
                var dividend = new List<Int32>();
                asciilength = Convert.ToInt32(((messageascii.Length).ToString()).Substring(0, ((messageascii.Length).ToString()).Length - 1));
                asciilength = asciilength * 10;
                int j = 1;
                while (j <= asciilength)
                {
                    if (asciilength % j == 0)
                    {
                        dividend.Add(j);
                    }
                    j = j + 1;
                }
                nogroup = dividend[(dividend.Count / 2) - 1];
                number = asciilength / nogroup;
            }
            int a = 1;
            while (a <= nogroup)
            {
                temporiginal.Add(subascii.Substring(0, number));
                subascii = subascii.Remove(0, number);
                a++;
            }
            if (subascii.Length != 0)
            {
                nogroup = nogroup + 1;
                temporiginal.Add(subascii);
            }
            original = temporiginal.ToArray();

        }
        private int amtofgrp;
        private void Transition()
        {
            int a = 0;
            int asciilength = 0;
            asciilength = bytefile.Length;
            
                var dividend = new List<Int32>();
                int j = 1;
                while (j <= asciilength)
                {
                    if (asciilength % j == 0)
                    {
                        dividend.Add(j);
                    }
                    j = j + 1;
                }
                nogroup = dividend[dividend.Count / 2];
                amtofgrp = asciilength / nogroup;           
        }
        byte[] outputbyte;
        private void Encrypt()
        {

            var check = new List<Int32>();
            var bytelist = new List<Byte>();
            int privlenght = privateKey.Length;
            //Rào điều kiện chạy dãy hash
            if ((privlenght % 2) == 0) //trường hợp privatekey chẵn
            {
                hash = BigInteger.Pow(BigInteger.Parse(privateKey.Substring(0, privateKey.Length / 2)),
                    Convert.ToInt32(privateKey.Substring(privateKey.Length / 2, privateKey.Length / 2)));
            }
            if ((privlenght % 2) == 1) //trường hợp privatekey lẻ
            {
                hash = BigInteger.Pow(BigInteger.Parse(privateKey.Substring(0, (privateKey.Length - 1) / 2)),
                    Convert.ToInt32(privateKey.Substring((privateKey.Length - 1) / 2, (privateKey.Length - 1) / 2 + 1)));
            }
            if (inputpath == null)
            {
                Ascii();
                ControlHash();
            }

            //Tính hash điều khiển để chọn độ dài hash con
            if (inputpath != null)
            {
                ByteControlHash();
                nogroup = bytefile.Length;
            }
            //Encrypt chính
            string subhash = hash.ToString(); //string phụ chạy lặp while
            publicKey = "";
            int i = 0;
            MessageBox.Show(messageascii);
            publicKey = "";
            if (inputpath == null)
            {
                Group();
            }
            var tempspace = new List<byte>();
            if (inputpath != null)
            {
                Transition();
                int zz = 0;                
                while (zz < bytefile.Length)
                {
                    tempspace.Add(0);
                    zz = zz + 1;
                }
            }            
            while ((((nogroup % 2) == 0) && i <= (nogroup / 2)) ||
                   (((nogroup % 2) == 1) && i <= (nogroup / 2) - 1))
            {
                if (inputpath == null)
                {
                    var modified = new List<string>();
                    int j = 0;
                    while (j < nogroup)
                    {
                        modified.Add("0");
                        j++;
                    }
                    while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                    {
                        subhash = subhash + hash;
                    }
                    if (subhash.Length > ctlhash) //Đủ hash thì quẫy thôi :v
                    {
                        orgpublic = subhash.Substring(0, ctlhash); //dãy hash con
                        int s = 0;
                        while (s <= Math.Sqrt(Convert.ToDouble(orgpublic)))
                        {
                            ctlpublic = ctlpublic + 1;
                            s = s + 1;
                            if (ctlpublic > nogroup)
                            {
                                ctlpublic = 0;
                            }
                        }
                        bool exist1 = check.Exists(element => element == i);
                        bool exist2 = check.Exists(element => element == ctlpublic);
                        if (ctlpublic < nogroup && exist1 == false && exist2 == false && i != ctlpublic)
                        {
                            modified[i] = original[ctlpublic];
                            modified[ctlpublic] = original[i];
                            check.Add(i);
                            check.Add(ctlpublic);
                            i = i + 1;
                        }
                        if (check.Count == (nogroup - 1))
                        {
                            int thr = 0;
                            while ((check.Exists(element => element == thr)) == true)
                            {
                                thr = thr + 1;
                            }
                            modified[thr] = original[thr];
                            i = i + 1;
                            check.Add(thr);
                        }
                        if (check.Count == (nogroup - 2))
                        {
                            int rem1 = 0;
                            int rem2 = 0;
                            if ((check.Exists(element => element == rem1)) == true ||
                                (check.Exists(element => element == rem2)) == true)
                            {
                                while ((check.Exists(element => element == rem1)) == true)
                                {
                                    rem1 = rem1 + 1;
                                }
                                check.Add(rem1);
                                while ((check.Exists(element => element == rem2)) == true)
                                {
                                    rem2 = rem2 + 1;
                                }
                                check.Add(rem2);
                                modified[rem1] = original[rem2];
                                modified[rem2] = original[rem1];
                                i = i + 1;
                            }
                        }
                        if (check.Exists(element => element == i) == true)
                        {
                            i = i + 1;
                        }
                        while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                        {
                            subhash = subhash + hash;
                        }
                        subhash = subhash.Remove(0, ctlhash);

                        int l = 0;
                        while (l < nogroup)
                        {
                            publicKey = publicKey + modified[l];
                            l++;
                        }
                    }
                }
                if (inputpath != null)
                {
                    while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                    {
                        subhash = subhash + hash;
                    }
                    if (subhash.Length > ctlhash) //Đủ hash thì quẫy thôi :v
                    {
                        orgpublic = subhash.Substring(0, ctlhash); //dãy hash con
                        int s = 0;
                        while (s <= Math.Sqrt(Convert.ToDouble(orgpublic)))
                        {
                            ctlpublic = ctlpublic + 1;
                            s = s + 1;
                            if (ctlpublic > nogroup)
                            {
                                ctlpublic = 0;
                            }
                        }
                        var bytebool = new List<Boolean>();
                        int z = 0;
                        bool check1 = false;
                        bool check2 = false;
                        while (z < amtofgrp)
                        {
                            if (check.Exists(element => element == ctlpublic) == true)
                            {
                                check1 = true;
                                z++;
                            }
                            if (check.Exists(element => element == i) == true)
                            {
                                check2 = true;
                                z++;
                            }
                            if (check.Exists(element => element == i) == false && check.Exists(element => element == i) == false)
                            {
                                z++;
                            }
                        }
                        z = 0;
                        if (ctlpublic < nogroup && check1 == false && check2 == false)
                        {
                            while (z < amtofgrp)
                            {
                                tempspace[i+z] = bytefile[ctlpublic+z];
                                tempspace[ctlpublic+z] = bytefile[i+z];
                                z++;
                            }
                            check.Add(i);
                            check.Add(ctlpublic);
                            z++;
                        }
                        if (check.Count == (amtofgrp * (nogroup - 1)))
                        {
                            z = 0;
                            int thr = 0;
                            while ((check.Exists(element => element == thr)) == true)
                            {
                                thr = thr + 1;
                            }
                            while (z < amtofgrp)
                            {
                                tempspace[thr + z] = bytefile[thr + z];
                                
                                z++;
                            }
                            check.Add(thr);
                            i = i + 1;
                        }
                        if (check.Count == (amtofgrp *(nogroup - 2)))
                        {
                            int rem1 = 0;
                            int rem2 = 0;
                            if ((check.Exists(element => element == rem1)) == true ||
                                (check.Exists(element => element == rem2)) == true)
                            {
                                z = 0;
                                while ((check.Exists(element => element == rem1)) == true)
                                {
                                    rem1 = rem1 + 1;
                                }

                                while ((check.Exists(element => element == rem2)) == true)
                                {
                                    rem2 = rem2 + 1;
                                }
                                while (z < amtofgrp)
                                {
                                    
                                    tempspace[rem1 + z] = bytefile[rem2 + z];
                                    tempspace[rem2 + z] = bytefile[rem1 + z];
                                    z++;
                                }
                                check.Add(rem2);
                                check.Add(rem1);
                                i = i + 1;
                            }
                        }
                        if (check.Exists(element => element == i) == true)
                        {
                            i = i + 1;
                        }
                        while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                        {
                            subhash = subhash + hash;
                        }
                        subhash = subhash.Remove(0, ctlhash);
                    }
                }
            }
                if (inputpath == null)
                {
                    string TpublicKey = publicKey;
                    if ((publicKey.Length % 2) == 1)
                    {
                        while (publicKey.Length != 1)
                        {
                            if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 97)
                            {
                                outputkey = outputkey + "range0";
                                publicKey = publicKey.Remove(0, 2);
                            }
                            if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 98)
                            {
                                outputkey = outputkey + "range1";
                                publicKey = publicKey.Remove(0, 2);
                            }
                            if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 99)
                            {
                                outputkey = outputkey + "range2";
                                publicKey = publicKey.Remove(0, 2);
                            }
                            if (publicKey.Length >= 1 && Convert.ToInt32(publicKey.Substring(0, 2)) <= 96)
                            {
                                outputkey = outputkey + alphabet[Convert.ToInt32(publicKey.Substring(0, 2))];
                                publicKey = publicKey.Remove(0, 2);
                            }
                        }
                        if (publicKey.Length == 1)
                        {
                            outputkey = outputkey + "single" + publicKey.Substring(0, 1);
                            publicKey = publicKey.Remove(0, 1);
                        }
                    }
                    if (((publicKey.Length % 2) == 0))
                    {
                        while (publicKey.Length != 0)
                        {

                            if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 97)
                            {
                                outputkey = outputkey + "range0";
                                publicKey = publicKey.Remove(0, 2);
                            }
                            if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 98)
                            {
                                outputkey = outputkey + "range1";
                                publicKey = publicKey.Remove(0, 2);
                            }
                            if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 99)
                            {
                                outputkey = outputkey + "range2";
                                publicKey = publicKey.Remove(0, 2);
                            }
                            if (publicKey.Length >= 1 && Convert.ToInt32(publicKey.Substring(0, 2)) <= 96)
                            {
                                outputkey = outputkey + alphabet[Convert.ToInt32(publicKey.Substring(0, 2))];
                                publicKey = publicKey.Remove(0, 2);
                            }
                        }
                    }
                    MessageBox.Show(TpublicKey);
                }
                if (inputpath != null)
                {
                    outputbyte = tempspace.ToArray();
                    File.WriteAllBytes("C:\\Users\\zodiac3011\\Desktop\\test.exe", outputbyte);
                }            
        }
        private void DEControlHash()
        {
            string subpriv = privateKey;
            BigInteger dk = 1;
            while (subpriv.Length != 0)
            {
                ctlhash = ctlhash + Convert.ToInt32(subpriv.Substring(0, 1));
                subpriv = subpriv.Remove(0, 1);
                if (subpriv.Length == 0 && ctlhash > (hash.ToString()).Length || messageascii.Length / 2 < ctlhash)
                {
                    dk = dk + 1;
                    subpriv = (Math.Sqrt(Convert.ToInt32(ctlhash))).ToString();
                    ctlhash = 0;
                }
                if (subpriv.Contains("."))
                {
                    subpriv = subpriv.Remove(subpriv.IndexOf("."), subpriv.Length - subpriv.IndexOf("."));
                }
            }
            ctlhash = Convert.ToInt32(Math.Sqrt(ctlhash));
        }
        private void Decrypt()
        {
            string tempkey = "";
            var check = new List<Int32>();
            var bytelist = new List<Byte>();
            int privlenght = privateKey.Length;
            //Rào điều kiện chạy dãy hash
            if ((privlenght%2) == 0) //trường hợp privatekey chẵn
            {
                hash = BigInteger.Pow(BigInteger.Parse(privateKey.Substring(0, privateKey.Length/2)),
                    Convert.ToInt32(privateKey.Substring(privateKey.Length/2, privateKey.Length/2)));
            }
            if ((privlenght%2) == 1) //trường hợp privatekey lẻ
            {
                hash = BigInteger.Pow(BigInteger.Parse(privateKey.Substring(0, (privateKey.Length - 1)/2)),
                    Convert.ToInt32(privateKey.Substring((privateKey.Length - 1)/2, (privateKey.Length - 1)/2 + 1)));
            }
            
            DEmessage = "";
            int idx = 0;
            string index = "";
            while (publicKey.Length != 0)
            {
                if (publicKey.Length >= 6 && (publicKey.Substring(0, 7) != "single0" || publicKey.Substring(0, 7) != "single1" ||
                     publicKey.Substring(0, 7) != "single2" || publicKey.Substring(0, 7) != "single3" ||
                     publicKey.Substring(0, 7) != "single4" || publicKey.Substring(0, 7) != "single5" ||
                     publicKey.Substring(0, 7) != "single6" || publicKey.Substring(0, 7) != "single7" ||
                     publicKey.Substring(0, 7) != "single8" || publicKey.Substring(0, 7) != "single9") &&
                    (publicKey.Substring(0, 6) == "range0" || publicKey.Substring(0, 6) == "range1" || publicKey.Substring(0, 6) == "range2"))
                {
                    if (publicKey.Substring(0, 6) == "range2")
                    {
                        tempkey = tempkey + "99";
                    }
                    if (publicKey.Substring(0, 6) == "range1")
                    {
                        tempkey = tempkey + "98";
                    }
                    if (publicKey.Substring(0, 6) == "range0")
                    {
                        tempkey = tempkey + "97";
                    }
                    publicKey = publicKey.Remove(0, 6);
                }
                    if (publicKey.Length >= 7 && (publicKey.Substring(0, 7) == "single0" || publicKey.Substring(0, 7) == "single1" ||
                     publicKey.Substring(0, 7) == "single2" || publicKey.Substring(0, 7) == "single3" ||
                     publicKey.Substring(0, 7) == "single4" || publicKey.Substring(0, 7) == "single5" ||
                     publicKey.Substring(0, 7) == "single6" || publicKey.Substring(0, 7) == "single7" ||
                     publicKey.Substring(0, 7) == "single8" || publicKey.Substring(0, 7) == "single9") &&
                    (publicKey.Substring(0, 6) != "range0" || publicKey.Substring(0, 6) != "range1" || publicKey.Substring(0, 6) != "range2"))
                {
                    tempkey = tempkey + publicKey.Substring(6, 1);
                    publicKey = publicKey.Remove(0, 7);
                }
                    if (publicKey.Length >= 1 && (publicKey.Substring(0, 7) != "single0" || publicKey.Substring(0, 7) != "single1" ||
                     publicKey.Substring(0, 7) != "single2" || publicKey.Substring(0, 7) != "single3" ||
                     publicKey.Substring(0, 7) != "single4" || publicKey.Substring(0, 7) != "single5" ||
                     publicKey.Substring(0, 7) != "single6" || publicKey.Substring(0, 7) != "single7" ||
                     publicKey.Substring(0, 7) != "single8" || publicKey.Substring(0, 7) != "single9") &&
                    (publicKey.Substring(0, 6) != "range0" || publicKey.Substring(0, 6) != "range1" || publicKey.Substring(0, 6) == "range2"))
                {
                    idx = 0;
                    while (alphabet[idx].ToString() != publicKey.Substring(0, 1) && idx < alphabet.Length && publicKey.Length > 0)
                    {
                        idx = idx + 1;
                    }
                    if ((idx.ToString()).Length == 1)
                    {
                        index = "0" + idx.ToString();
                    }
                    if ((idx.ToString()).Length == 2)
                    {
                        index = idx.ToString();
                    }
                    tempkey = tempkey + index;
                    publicKey = publicKey.Remove(0, 1);
                }
            }
            MessageBox.Show(tempkey);
            DEControlHash();
            if (DEmessage.Length != tempkey.Length)
            {
                DEmessage = string.Concat(Enumerable.Repeat("0", tempkey.Length));
            }
            int i = 0;
            string submess = message; //string phụ chạy điều kiện lặp while
            string subhash = hash.ToString(); //string phụ chạy lặp while
            while ((((tempkey.Length%2) == 0) && i <= (tempkey.Length/2)) ||
                   (((tempkey.Length%2) == 1) && i <= (tempkey.Length/2) - 1))
            {
                int a = 0;
                ctlpublic = 0;
                while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                {
                    subhash = subhash + hash;
                }
                if (subhash.Length > ctlhash) //Đủ hash thì quẫy thôi :v
                {

                    orgpublic = subhash.Substring(0, (ctlhash.ToString()).Length); //dãy hash con
                    while (a <= ctlhash)
                    {
                        if (ctlpublic <= tempkey.Length)
                        {
                            ctlpublic = ctlpublic + 1;
                        }
                        if (ctlpublic > tempkey.Length)
                        {
                            ctlpublic = 0;
                        }
                        a = a + 1;
                    }
                }
                bool exist1 = check.Exists(element => element == i);
                bool exist2 = check.Exists(element => element == ctlpublic);
                if (ctlpublic < tempkey.Length && exist1 == false && exist2 == false && i != ctlpublic)
                {
                    MessageBox.Show(i + "và " + ctlpublic);
                    char replace1 = (tempkey[i]);
                    char replace2 = (tempkey[ctlpublic]);
                    StringBuilder change = new StringBuilder(DEmessage);
                    change[ctlpublic] = replace1;
                    change[i] = replace2;
                    DEmessage = change.ToString();
                    check.Add(i);
                    check.Add(ctlpublic);
                    i = i + 1;
                }
                if (check.Count == (tempkey.Length - 1))
                {
                    int thr = 0;
                    while ((check.Exists(element => element == thr)) == true)
                    {
                        thr = thr + 1;
                    }
                    char replace3 = (tempkey[thr]);
                    StringBuilder change1 = new StringBuilder(DEmessage);
                    change1[thr] = replace3;
                    DEmessage = change1.ToString();
                    i = i + 1;
                    check.Add(thr);
                }
                if (check.Count == (tempkey.Length - 2))
                {
                    int rem1 = 0;
                    int rem2 = 0;
                    if ((check.Exists(element => element == rem1)) == true ||
                        (check.Exists(element => element == rem2)) == true)
                    {
                        while ((check.Exists(element => element == rem1)) == true)
                        {
                            rem1 = rem1 + 1;
                        }
                        check.Add(rem1);
                        while ((check.Exists(element => element == rem2)) == true)
                        {
                            rem2 = rem2 + 1;
                        }
                        check.Add(rem1);
                        char replace6 = tempkey[rem1];
                        char replace5 = tempkey[rem2];
                        StringBuilder change2 = new StringBuilder(DEmessage);
                        change2[rem1] = replace5;
                        change2[rem2] = replace6;
                        DEmessage = change2.ToString();
                        i = i + 1;
                    }
                }
                if (check.Exists(element => element == i) == true)
                {
                    i = i + 1;
                }
                while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                {
                    subhash = subhash + hash;
                }
                subhash = subhash.Remove(0, ctlhash);
            }
            MessageBox.Show(DEmessage);

        }

        }
}