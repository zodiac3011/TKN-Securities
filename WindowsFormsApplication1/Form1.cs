﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private static string publicKey, //sau khi hoá đổi
            privateKey,
            message,
            outputkey,
            DEmessage,
            outputmessage;

        private string allist;

        private char[] alphabet;

        private int amtofgrp;

        private byte[] bytefile;

        private string constant, outputpath;

        private int ctlhash, ctlpublic;

        private string[] DEmodified;

        private BigInteger hash;

        private OpenFileDialog inputfile = new OpenFileDialog();

        private string inputpath;

        private int nogroup;

        private string orgpublic, messageascii;

        private string[] original;

        private string originalmessage;

        private byte[] outputbyte;

        private SaveFileDialog outputfile = new SaveFileDialog();

        private string tempkey;

        public Form1()
        {
            InitializeComponent();
        }

        private void Alphabet()
        {
            allist =
            "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_-+=,./;'[]><|{}?:`~\n\"\\\t\v ";
            var subal = new List<char>();
            while (allist.Length != 0)
            {
                subal.Add(Convert.ToChar(allist.Substring(0, 1)));
                allist = allist.Remove(0, 1);
            }
            alphabet = subal.ToArray();
        }

        private void Ascii()
        {
            string index = "";
            int idx = 0;
            while (message.Length != 0)
            {
                idx = 0;
                while (alphabet[idx].ToString() != message.Substring(0, 1) && idx < alphabet.Length && message.Length > 0)
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
                messageascii = messageascii + index;
                message = message.Remove(0, 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult result2 = outputfile.ShowDialog();
            string file = "";
            if (result2 == DialogResult.OK) // Test result.
            {
                file = outputfile.FileName;
                textBox21.Text = file;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult result1 = inputfile.ShowDialog();
            string file = "";
            if (result1 == DialogResult.OK) // Test result.
            {
                file = inputfile.FileName;
                textBox22.Text = file;
            }
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
            outputkey = null;
            if (textBox11.Text.Trim() == string.Empty || textBox9.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter all the required details");
                return;
            }
            PrivatekeyProcess();
            Encrypt();
            textBox12.Text = outputkey;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            hash = 0;
            ctlhash = 0;
            ctlpublic = 0;
            orgpublic = null;
            DEmessage = null;
            messageascii = null;
            if (textBox13.Text.Trim() == string.Empty || textBox15.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter all the required details");
                return;
            }
            Decrypt();
            textBox14.Text = originalmessage;
        }

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

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result2 = outputfile.ShowDialog();
            string file = "";
            if (result2 == DialogResult.OK) // Test result.
            {
                file = outputfile.FileName;
                textBox20.Text = file;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ctlpublic = 0;
            if (inputfile.CheckFileExists == false || inputfile.CheckPathExists == false)
            {
                return;
            }
            bytefile = System.IO.File.ReadAllBytes(inputpath);
            privateKey = textBox17.Text;
            PrivatekeyProcess();
            Encrypt();
            Array.Clear(bytefile, 0, bytefile.Length);
            Array.Clear(outputbyte, 0, outputbyte.Length);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ctlpublic = 0;
            privateKey = textBox23.Text;
            if (inputfile.CheckFileExists == false || inputfile.CheckPathExists == false)
            {
                return;
            }
            bytefile = System.IO.File.ReadAllBytes(inputpath);
            FDecrypt();
            Array.Clear(bytefile, 0, bytefile.Length);
            Array.Clear(outputbyte, 0, outputbyte.Length);
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
                if (subpriv.Contains("."))
                {
                    subpriv = subpriv.Remove(subpriv.IndexOf("."), subpriv.Length - subpriv.IndexOf("."));
                }
            }
            ctlhash = Convert.ToInt32(Math.Sqrt(ctlhash));
        }

        private void DEControlHash()
        {
            string subpriv = privateKey;
            BigInteger dk = 1;
            while (subpriv.Length != 0)
            {
                ctlhash = ctlhash + Convert.ToInt32(subpriv.Substring(0, 1));
                subpriv = subpriv.Remove(0, 1);
                if (subpriv.Length == 0 && ctlhash > (hash.ToString()).Length || tempkey.Length / 2 < ctlhash)
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
            PrivatekeyProcess();
            tempkey = "";
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

            DEmessage = "";
            int idx = 0;
            string index = "";
            while (publicKey.Length != 0)
            {
                if (publicKey.Length >= 6 && (publicKey.Substring(0, 6) == "range0" || publicKey.Substring(0, 6) == "range1"))
                {
                    if (publicKey.Substring(0, 6) == "range1")
                    {
                        tempkey = tempkey + "99";
                    }
                    if (publicKey.Substring(0, 6) == "range0")
                    {
                        tempkey = tempkey + "98";
                    }
                    publicKey = publicKey.Remove(0, 6);
                }
                if (publicKey.Length >= 7 && (publicKey.Substring(0, 7) == "single0" || publicKey.Substring(0, 7) == "single1" ||
                 publicKey.Substring(0, 7) == "single2" || publicKey.Substring(0, 7) == "single3" ||
                 publicKey.Substring(0, 7) == "single4" || publicKey.Substring(0, 7) == "single5" ||
                 publicKey.Substring(0, 7) == "single6" || publicKey.Substring(0, 7) == "single7" ||
                 publicKey.Substring(0, 7) == "single8" || publicKey.Substring(0, 7) == "single9"))
                {
                    tempkey = tempkey + publicKey.Substring(6, 1);
                    publicKey = publicKey.Remove(0, 7);
                }
                if (publicKey.Length >= 7 && (publicKey.Substring(0, 7) != "single0" || publicKey.Substring(0, 7) != "single1" ||
                 publicKey.Substring(0, 7) != "single2" || publicKey.Substring(0, 7) != "single3" ||
                 publicKey.Substring(0, 7) != "single4" || publicKey.Substring(0, 7) != "single5" ||
                 publicKey.Substring(0, 7) != "single6" || publicKey.Substring(0, 7) != "single7" ||
                 publicKey.Substring(0, 7) != "single8" || publicKey.Substring(0, 7) != "single9") &&
                (publicKey.Substring(0, 6) != "range0" || publicKey.Substring(0, 6) != "range1"))
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
                if (publicKey.Length == 6 && (publicKey.Substring(0, 6) != "range0" || publicKey.Substring(0, 6) != "range1"))
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
                if (publicKey.Length < 6 && publicKey.Length > 0)
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
            //
            DEGroup();
            DEControlHash();
            var temporiginal = new List<string>();
            temporiginal.Add("0");
            while (temporiginal.Count != DEmodified.Length)
            {
                temporiginal.Add("0");
            }
            temporiginal[temporiginal.Count - 1] = constant;
            check.Add(temporiginal.Count - 1);
            int i = 0;
            string subhash = hash.ToString(); //string phụ chạy lặp while
            while ((((nogroup % 2) == 0) && i <= (nogroup / 2)) ||
                   (((nogroup % 2) == 1) && i <= (nogroup / 2) - 1))
            {
                while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
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
                        temporiginal[i] = DEmodified[ctlpublic];
                        temporiginal[ctlpublic] = DEmodified[i];
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
                        temporiginal[thr] = DEmodified[thr];
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
                            temporiginal[rem1] = DEmodified[rem2];
                            temporiginal[rem2] = DEmodified[rem1];
                            i = i + 1;
                        }
                    }
                    if (check.Exists(element => element == i) == true)
                    {
                        i = i + 1;
                    }
                    while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                    {
                        subhash = subhash + hash;
                    }
                    subhash = subhash.Remove(0, ctlhash);
                }
            }
            int l = 0;
            while (l < temporiginal.Count)
            {
                DEmessage = DEmessage + temporiginal[l];
                l++;
            }
            while (DEmessage.Length != 0)
            {
                originalmessage = originalmessage + alphabet[Convert.ToInt16(DEmessage.Substring(0, 2))];
                DEmessage = DEmessage.Remove(0, 2);
            }
            MessageBox.Show("Decrytion Complete");
            textBox14.Text = originalmessage;
        }

        private void DEGroup()
        {
            int number = 0;
            var tempmodified = new List<string>();
            string subascii = tempkey;
            int asciilength = 0;
            if (tempkey.Length % 10 == 0)
            {
                var dividend = new List<Int32>();
                int j = 1;
                while (j <= tempkey.Length)
                {
                    if (tempkey.Length % j == 0)
                    {
                        dividend.Add(j);
                    }
                    j = j + 1;
                }
                nogroup = dividend[(dividend.Count / 2) - 1];
                number = tempkey.Length / nogroup;
            }
            if (tempkey.Length % 10 != 0)
            {
                var dividend = new List<Int32>();
                asciilength = Convert.ToInt32(((tempkey.Length).ToString()).Substring(0, ((tempkey.Length).ToString()).Length - 1));
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
                tempmodified.Add(subascii.Substring(0, number));
                subascii = subascii.Remove(0, number);
                a++;
            }
            if (subascii.Length != 0)
            {
                nogroup = nogroup + 1;
                tempmodified.Add(subascii);
            }
            DEmodified = tempmodified.ToArray();
        }

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
            }
            //Encrypt chính
            string subhash = hash.ToString(); //string phụ chạy lặp while
            publicKey = "";
            int i = 0;
            publicKey = "";
            var modified = new List<string>();
            if (inputpath == null)
            {
                Group();

                int j = 0;
                while (j < nogroup)
                {
                    modified.Add("0");
                    j++;
                }
                if (constant != null)
                {
                    modified[modified.Count - 1] = constant;
                    check.Add(modified.Count - 1);
                }
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
            if (inputpath == null)
            {
                while ((((nogroup % 2) == 0) && i <= (nogroup / 2)) ||
                   (((nogroup % 2) == 1) && i <= (nogroup / 2) - 1))
                {
                    while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
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
                        while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                        {
                            subhash = subhash + hash;
                        }
                        subhash = subhash.Remove(0, ctlhash);
                    }
                }

                while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                {
                    subhash = subhash + hash;
                }
            }
            if (inputpath != null)
            {
                while (check.Count < nogroup)
                {
                    if (Convert.ToInt16(constant) != 0 && i == 0)
                    {
                        int isolate = Convert.ToInt16(constant);
                        while (isolate > 0)
                        {
                            tempspace[bytefile.Length - isolate] = bytefile[bytefile.Length - isolate];
                            isolate = isolate - 1;
                        }
                    }
                    while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                    {
                        subhash = subhash + hash;
                    }
                    if (subhash.Length > ctlhash) //Đủ hash thì quẫy thôi :v
                    {
                        orgpublic = subhash.Substring(0, ctlhash + 1); //dãy hash con
                        int s = 0;
                        while (s <= Math.Sqrt(Convert.ToDouble(orgpublic)))
                        {
                            ctlpublic = ctlpublic + 1;
                            s = s + 1;
                            if (ctlpublic >= (nogroup))
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
                        if (ctlpublic < nogroup && check1 == false && check2 == false && i != ctlpublic)
                        {
                            while (z < amtofgrp)
                            {
                                tempspace[i * amtofgrp + z] = bytefile[ctlpublic * amtofgrp + z];
                                tempspace[ctlpublic * amtofgrp + z] = bytefile[i * amtofgrp + z];
                                z++;
                            }
                            check.Add(i);
                            check.Add(ctlpublic);
                            //MessageBox.Show(i + " và " + ctlpublic);
                            z++;
                            i = i + 1;
                        }
                        if (check.Count == nogroup - 1)
                        {
                            z = 0;
                            int thr = 0;
                            while ((check.Exists(element => element == thr)) == true)
                            {
                                thr = thr + 1;
                            }
                            while (z < amtofgrp)
                            {
                                tempspace[thr * amtofgrp + z] = bytefile[thr * amtofgrp + z];

                                z++;
                            }
                            check.Add(thr);
                            i = i + 1;
                        }
                        if (check.Count == (nogroup - 2))
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
                                    tempspace[rem1 * amtofgrp + z] = bytefile[rem2 * amtofgrp + z];
                                    tempspace[rem2 * amtofgrp + z] = bytefile[rem1 * amtofgrp + z];
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
                        while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                        {
                            subhash = subhash + hash;
                        }
                        subhash = subhash.Remove(0, ctlhash);
                    }
                }
            }

            if (inputpath == null)
            {
                int l = 0;
                while (l < modified.Count)
                {
                    publicKey = publicKey + modified[l];
                    l++;
                }
                string TpublicKey = publicKey;
                if ((publicKey.Length % 2) == 1)
                {
                    while (publicKey.Length != 1)
                    {
                        if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 98)
                        {
                            outputkey = outputkey + "range0";
                            publicKey = publicKey.Remove(0, 2);
                        }
                        if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 99)
                        {
                            outputkey = outputkey + "range1";
                            publicKey = publicKey.Remove(0, 2);
                        }
                        if (publicKey.Length >= 1 && Convert.ToInt32(publicKey.Substring(0, 2)) <= 97)
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
                        if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 98)
                        {
                            outputkey = outputkey + "range0";
                            publicKey = publicKey.Remove(0, 2);
                        }
                        if (publicKey.Length >= 2 && Convert.ToInt32(publicKey.Substring(0, 2)) == 99)
                        {
                            outputkey = outputkey + "range1";
                            publicKey = publicKey.Remove(0, 2);
                        }
                        if (publicKey.Length >= 1 && Convert.ToInt32(publicKey.Substring(0, 2)) <= 97)
                        {
                            outputkey = outputkey + alphabet[Convert.ToInt32(publicKey.Substring(0, 2))];
                            publicKey = publicKey.Remove(0, 2);
                        }
                    }
                }
            }
            if (inputpath != null)
            {
                outputbyte = tempspace.ToArray();
                File.WriteAllBytes(outputpath, outputbyte);
                MessageBox.Show("Encryption Completed");
                privateKey = null;
                check.Clear();
                tempspace.Clear();
            }
        }

        private void FDecrypt()
        {
            PrivatekeyProcess();
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
            if (inputpath != null)
            {
                ByteControlHash();
            }
            string subhash = hash.ToString(); //string phụ chạy lặp while
            int i = 0;
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
            while (check.Count < nogroup)
            {
                if (inputpath != null)
                {
                    while (check.Count < nogroup)
                    {
                        if (Convert.ToInt16(constant) != 0 && i == 0)
                        {
                            int isolate = Convert.ToInt16(constant);
                            while (isolate > 0)
                            {
                                tempspace[bytefile.Length - isolate] = bytefile[bytefile.Length - isolate];
                                isolate = isolate - 1;
                            }
                        }
                        while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                        {
                            subhash = subhash + hash;
                        }
                        if (subhash.Length > ctlhash) //Đủ hash thì quẫy thôi :v
                        {
                            orgpublic = subhash.Substring(0, ctlhash + 1); //dãy hash con
                            int s = 0;
                            while (s <= Math.Sqrt(Convert.ToDouble(orgpublic)))
                            {
                                ctlpublic = ctlpublic + 1;
                                s = s + 1;
                                if (ctlpublic >= (nogroup))
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
                            if (ctlpublic < nogroup && check1 == false && check2 == false && i != ctlpublic)
                            {
                                while (z < amtofgrp)
                                {
                                    tempspace[i * amtofgrp + z] = bytefile[ctlpublic * amtofgrp + z];
                                    tempspace[ctlpublic * amtofgrp + z] = bytefile[i * amtofgrp + z];
                                    z++;
                                }
                                check.Add(i);
                                check.Add(ctlpublic);
                                //MessageBox.Show(i + " và " + ctlpublic);
                                z++;
                                i = i + 1;
                            }
                            if (check.Count == nogroup - 1)
                            {
                                z = 0;
                                int thr = 0;
                                while ((check.Exists(element => element == thr)) == true)
                                {
                                    thr = thr + 1;
                                }
                                while (z < amtofgrp)
                                {
                                    tempspace[thr * amtofgrp + z] = bytefile[thr * amtofgrp + z];

                                    z++;
                                }
                                check.Add(thr);
                                i = i + 1;
                            }
                            if (check.Count == (nogroup - 2))
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
                                        tempspace[rem1 * amtofgrp + z] = bytefile[rem2 * amtofgrp + z];
                                        tempspace[rem2 * amtofgrp + z] = bytefile[rem1 * amtofgrp + z];
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
                            while (subhash.Length <= ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                            {
                                subhash = subhash + hash;
                            }
                            subhash = subhash.Remove(0, ctlhash);
                        }
                    }
                }
            }
            if (inputpath != null)
            {
                outputbyte = tempspace.ToArray();
                File.WriteAllBytes(outputpath, outputbyte);
                MessageBox.Show("Decryption Completed");
            }
            privateKey = null;
            check.Clear();
            tempspace.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Alphabet();
        }

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
                constant = subascii;
            }
            original = temporiginal.ToArray();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
        }

        private void label17_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void PrivatekeyProcess()
        {
            string subprivatekey = "";
            if (Regex.IsMatch(privateKey, "[a-zA-Z]") == true)
            {
                int a = 0;
                int index = 0;
                while (a < privateKey.Length)
                {
                    if (Regex.IsMatch(privateKey[a].ToString(), "[a-zA-Z]") == false)
                    {
                        index = 0;
                        subprivatekey = subprivatekey + privateKey[a].ToString();
                        a++;
                    }
                    index = 0;
                    if (a < privateKey.Length)
                    {
                        if (Regex.IsMatch(privateKey[a].ToString(), "[a-zA-Z]"))
                        {
                            while (alphabet[index] != privateKey[a])
                            {
                                index++;
                            }
                            subprivatekey = subprivatekey + index;
                            a++;
                        }
                    }
                }
                privateKey = subprivatekey;
            }
            BigInteger test = BigInteger.Parse(privateKey);
            while (test > Int32.MaxValue)
            {
                if (privateKey.Length % 2 == 0)
                {
                    privateKey = (BigInteger.Parse(((test.ToString()).Substring(0, privateKey.Length / 2))) + BigInteger.Parse(((test.ToString()).Substring(privateKey.Length / 2, privateKey.Length / 2)))).ToString();
                }
                if (privateKey.Length % 2 == 1)
                {
                    privateKey = (BigInteger.Parse(((test.ToString()).Substring(0, (privateKey.Length - 1) / 2))) + BigInteger.Parse(((test.ToString()).Substring((privateKey.Length - 1) / 2, ((privateKey.Length - 1) / 2) + 1)))).ToString();
                }
                test = BigInteger.Parse(privateKey);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            message = textBox11.Text;
        }

        private void textBox11_TextChanged_1(object sender, EventArgs e)
        {
            message = textBox11.Text;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox12_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void textBox13_TextChanged_1(object sender, EventArgs e)
        {
            privateKey = textBox13.Text;
        }

        private void textBox14_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox15_TextChanged_1(object sender, EventArgs e)
        {
            publicKey = textBox15.Text;
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            privateKey = textBox17.Text;
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            inputpath = textBox19.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            outputpath = textBox20.Text;
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            outputpath = textBox21.Text;
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            inputpath = textBox22.Text;
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            privateKey = textBox23.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {
            privateKey = textBox9.Text;
        }
        private void Transition()
        {
            long asciilength = bytefile.Length;
            var dividend = new List<Int32>();
            int j = 1;
            if (bytefile.Length % 10 == 0)
            {
                while (j <= asciilength)
                {
                    if (asciilength % j == 0)
                    {
                        dividend.Add(j);
                    }
                    j = j + 1;
                }
            }
            if (bytefile.Length % 10 != 0)
            {
                asciilength = asciilength - Convert.ToInt16((asciilength.ToString()).Substring((asciilength.ToString()).Length - 1, 1));
                while (j <= asciilength)
                {
                    if (asciilength % j == 0)
                    {
                        dividend.Add(j);
                    }
                    j = j + 1;
                }
            }
            nogroup = dividend[(dividend.Count / 2) - 1];
            amtofgrp = Convert.ToInt32(asciilength / nogroup);
            constant = (Convert.ToInt32(bytefile.Length) - Convert.ToInt32(asciilength)).ToString();
        }
    }
}