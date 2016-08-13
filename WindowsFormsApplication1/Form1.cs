﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.Security.AccessControl;
using System.Linq;
using System.Collections.Generic;

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
            DEprivateKey = textBox13.Text;
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
            publicKey = null;
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
            DEpublicKey = textBox15.Text;
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
            privateKey = textBox9.Text;
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
                ctlhash = ctlhash + Convert.ToInt16(subpriv.Substring(0, 1));
                subpriv = subpriv.Remove(0, 1);
                if (subpriv.Length == 0 && ctlhash > (hash.ToString()).Length || messageascii.Length / 2 < ctlhash)
                {
                    dk = dk + 1;
                    subpriv = (BigInteger.Divide(BigInteger.Parse
                        (privateKey), dk)).ToString();
                    ctlhash = 0;
                }
            }           
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

        private string allist;
        private char[] alphabet;
        private void Alphabet()
        {
            allist =
            "0123456789abcdefghiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_-+=,./;'[]><|{}?:`~ \n\"\\\t\v";
            var subal = new List<char>();
            while (allist.Length != 0)
            {
                    subal.Add(Convert.ToChar(allist.Substring(0, 1)));
                    allist = allist.Remove(0, 1);
                }
                alphabet = subal.ToArray();
        }
                
        private void Encrypt()
        {
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
            Ascii();
            ControlHash(); //Tính hash điều khiển để chọn độ dài hash con
            //Encrypt chính
            string submess = message; //string phụ chạy điều kiện lặp while
            string subhash = hash.ToString(); //string phụ chạy lặp while
            publicKey = "0";
            int i = 0;
            MessageBox.Show(messageascii);
            if (publicKey.Length != messageascii.Length) //tạo ra chỗ điền key vào
            {
                publicKey = string.Concat(Enumerable.Repeat("0", messageascii.Length));
            }
            while ((((messageascii.Length%2) == 0) && i <= (messageascii.Length/2)) ||
                   (((messageascii.Length%2) == 1) && i <= (messageascii.Length/2) - 1))
            {
                ctlpublic = 0;
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
                        if (ctlpublic > messageascii.Length)
                        {
                            ctlpublic = 0;
                        }
                    }
                    bool exist1 = check.Exists(element => element == i);
                    bool exist2 = check.Exists(element => element == ctlpublic);
                    if (ctlpublic < messageascii.Length && exist1 == false && exist2 == false && i != ctlpublic)
                    {
                        //MessageBox.Show(i + "và " + ctlpublic);
                        char replace1 = (messageascii[i]);
                        char replace2 = (messageascii[ctlpublic]);
                        StringBuilder change = new StringBuilder(publicKey);
                        change[ctlpublic] = replace1;
                        change[i] = replace2;
                        publicKey = change.ToString();
                        check.Add(i);
                        check.Add(ctlpublic);
                        i = i + 1;
                    }
                    if (check.Count == (messageascii.Length - 1))
                    {
                        int thr = 0;
                        while ((check.Exists(element => element == thr)) == true)
                        {
                            thr = thr + 1;
                        }
                        char replace3 = (messageascii[thr]);
                        StringBuilder change1 = new StringBuilder(publicKey);
                        change1[thr] = replace3;
                        publicKey = change1.ToString();
                        i = i + 1;
                        check.Add(thr);
                    }
                    if (check.Count == (messageascii.Length - 2))
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
                            char replace6 = messageascii[rem1];
                            char replace5 = messageascii[rem2];
                            StringBuilder change2 = new StringBuilder(publicKey);
                            change2[rem1] = replace5;
                            change2[rem2] = replace6;
                            publicKey = change2.ToString();
                            i = i + 1;
                        }
                    }
                    if (check.Exists(element => element == i) == true)
                    {
                        i = i + 1;
                    }
                }
                while (subhash.Length < ctlhash) //Trường hợp mà hết hash phải thêm hash vào
                {
                    subhash = subhash + hash;
                }
                subhash = subhash.Remove(0, ctlhash);
            }          
            string TpublicKey = publicKey;
            if ((publicKey.Length%2) == 1)
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
            if (((publicKey.Length%2) == 0))
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
            MessageBox.Show(TpublicKey);

        }
        private void DEControlHash()
        {
            string subpriv = string.Concat(Enumerable.Repeat(DEprivateKey, (hash.ToString()).Length / DEprivateKey.Length));
            while (subpriv.Length > 1)
            {
                ctlhash = ctlhash + Convert.ToInt16((subpriv.Substring(0, 1)));
                subpriv = subpriv.Remove(0, 1);
                while (subpriv.Length == 0 && ctlhash > (hash.ToString()).Length)
                {
                    subpriv = ctlhash.ToString(); //set subpriv mới chạy tiếp
                    ctlhash = 0; //reset ctlhash để chạy tiếp
                }
            }
        }
        private void Decrypt()
        {
            string tempkey = "";
            var check = new List<Int32>();
            var bytelist = new List<Byte>();
            int privlenght = DEprivateKey.Length;
            //Rào điều kiện chạy dãy hash
            if ((privlenght%2) == 0) //trường hợp privatekey chẵn
            {
                hash = BigInteger.Pow(BigInteger.Parse(DEprivateKey.Substring(0, DEprivateKey.Length/2)),
                    Convert.ToInt32(DEprivateKey.Substring(DEprivateKey.Length/2, DEprivateKey.Length/2)));
            }
            if ((privlenght%2) == 1) //trường hợp privatekey lẻ
            {
                hash = BigInteger.Pow(BigInteger.Parse(DEprivateKey.Substring(0, (DEprivateKey.Length - 1)/2)),
                    Convert.ToInt32(DEprivateKey.Substring((DEprivateKey.Length - 1)/2, (DEprivateKey.Length - 1)/2 + 1)));
            }
            DEControlHash();
            DEmessage = "";
            int idx = 0;
            string index = "";
            while (DEpublicKey.Length != 0)
            {
                if (DEpublicKey.Length >= 6 && (DEpublicKey.Substring(0, 7) != "single0" || DEpublicKey.Substring(0, 7) != "single1" ||
                     DEpublicKey.Substring(0, 7) != "single2" || DEpublicKey.Substring(0, 7) != "single3" ||
                     DEpublicKey.Substring(0, 7) != "single4" || DEpublicKey.Substring(0, 7) != "single5" ||
                     DEpublicKey.Substring(0, 7) != "single6" || DEpublicKey.Substring(0, 7) != "single7" ||
                     DEpublicKey.Substring(0, 7) != "single8" || DEpublicKey.Substring(0, 7) != "single9") &&
                    (DEpublicKey.Substring(0, 6) == "range0" || DEpublicKey.Substring(0, 6) == "range1"))
                {
                    tempkey = tempkey + DEpublicKey.Substring(5, 1);
                    DEpublicKey = DEpublicKey.Remove(0, 6);
                }
                    if (DEpublicKey.Length >= 7 && (DEpublicKey.Substring(0, 7) == "single0" || DEpublicKey.Substring(0, 7) == "single1" ||
                     DEpublicKey.Substring(0, 7) == "single2" || DEpublicKey.Substring(0, 7) == "single3" ||
                     DEpublicKey.Substring(0, 7) == "single4" || DEpublicKey.Substring(0, 7) == "single5" ||
                     DEpublicKey.Substring(0, 7) == "single6" || DEpublicKey.Substring(0, 7) == "single7" ||
                     DEpublicKey.Substring(0, 7) == "single8" || DEpublicKey.Substring(0, 7) == "single9") &&
                    (DEpublicKey.Substring(0, 6) != "range0" || DEpublicKey.Substring(0, 6) != "range1"))
                {
                    tempkey = tempkey + DEpublicKey.Substring(6, 1);
                    DEpublicKey = DEpublicKey.Remove(0, 7);
                }
                    if (DEpublicKey.Length >= 1 && (DEpublicKey.Substring(0, 7) != "single0" || DEpublicKey.Substring(0, 7) != "single1" ||
                     DEpublicKey.Substring(0, 7) != "single2" || DEpublicKey.Substring(0, 7) != "single3" ||
                     DEpublicKey.Substring(0, 7) != "single4" || DEpublicKey.Substring(0, 7) != "single5" ||
                     DEpublicKey.Substring(0, 7) != "single6" || DEpublicKey.Substring(0, 7) != "single7" ||
                     DEpublicKey.Substring(0, 7) != "single8" || DEpublicKey.Substring(0, 7) != "single9") &&
                    (DEpublicKey.Substring(0, 6) != "range0" || DEpublicKey.Substring(0, 6) != "range1"))
                {
                    idx = 0;
                    while (alphabet[idx].ToString() != DEpublicKey.Substring(0, 1) && idx < alphabet.Length && DEpublicKey.Length > 0)
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
                    DEpublicKey = DEpublicKey.Remove(0, 1);
                }
            }
            MessageBox.Show(tempkey);
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