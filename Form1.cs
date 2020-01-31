using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;

namespace TiPPGK5
{
    public partial class Form1 : Form
    {
        private readonly Graphics _graphics;
        private Bitmap _bmp = null;

        List<double> primes;
        double m, c, a, s;

        public Form1()
        {
            InitializeComponent();

            _bmp = new Bitmap(1280, 720);
            _graphics = Graphics.FromImage(_bmp);
            pictureBox1.Image = _bmp;

            primes = new List<double>();
            CalculatePrime(Math.Pow(2, 9));

            GenerateRandomizerValues();

            double seed = (a + c) % m;
            double random;

            int v = 7;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    random = seed / Math.Pow(2, 16);
                    if (random < 8191)
                    {
                        //_graphics.DrawRectangle(Pens.Blue, 0 + i * v, 0 + j * v, v, v);
                        _graphics.FillRectangle(Brushes.Blue, 0 + i * v, 0 + j * v, v, v);
                    }
                    else if (random >= 8191 && random < 16383)
                    {
                        //_graphics.DrawRectangle(Pens.Green, 0 + i * v, 0 + j * v, v, v);
                        _graphics.FillRectangle(Brushes.Green, 0 + i * v, 0 + j * v, v, v);
                    }
                    else if (random >= 16383 && random < 24575)
                    {
                        //_graphics.DrawRectangle(Pens.Red, 0 + i * v, 0 + j * v, v, v);
                        _graphics.FillRectangle(Brushes.Red, 0 + i * v, 0 + j * v, v, v);
                    }
                    else if (random >= 24575 && random < 32768)
                    {
                        //_graphics.DrawRectangle(Pens.Yellow, 0 + i * v, 0 + j * v, v, v);
                        _graphics.FillRectangle(Brushes.Yellow, 0 + i * v, 0 + j * v, v, v);
                    }
                    seed = (a * seed + c) % m;
                }
            }
        }

        void CalculatePrime(double upperLimit)
        {
            for (double i = 2; i <= upperLimit; i++)
            {
                bool isPrime = true;

                for (double j = 2; j <= i / 2; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    primes.Add(i);
                    //Debug.WriteLine(i);
                }
            }
        }

        double GCM(double a, double b)
        {
            double d = 0;

            while (a % 2 == 0 && b % 2 == 0)
            {
                a /= 2;
                b /= 2;
                d++;
            }

            while (a != b)
            {
                if (a % 2 == 0)
                    a /= 2;
                else if (b % 2 == 0)
                    b /= 2;
                else if (a > b)
                    a = (a - b) / 2;
                else
                    b = (b - a) / 2;
            }

            return a * Math.Pow(2, d);
        }

        void GenerateRandomizerValues()
        {
            m = Math.Pow(2, 31);
            c = Math.Truncate(((3 - Math.Sqrt(3) / 6) * m));
            
            while (GCM(m, c) != 1)
                c++;
            Debug.WriteLine("GCM(" + m + ", " + c + ") = " + GCM(m, c));

            a = Math.Pow(2, 9) + 1;

            if (m % 4 == 0)
            {
                if ((a - 1) % 4 == 0)
                {
                    double t1 = 0;
                    double t2 = 0;

                    foreach (double prime in primes)
                    {
                        if (prime <= a - 1 && m % prime == 0)
                        {
                            t1++;
                            if ((a - 1) % prime == 0)
                                t2++;
                        }
                    }

                    if (t1 == t2)
                    {
                        Debug.WriteLine("t1 == t2");
                        
                        double a1 = a;
                        for (double i = 2; i < 100 ; i++)
                        {
                            a1 = Math.Pow(a - 1, i);
                            if (a1 % m == 0)
                            {
                                s = i;
                                Debug.WriteLine("m: " + m + "\ta1: " + a1 + "\tS: " + s);
                                break;
                            }
                        }
                    }
                    else
                        Debug.WriteLine("t1 != t2");
                }
            }
            else
            {
                Debug.WriteLine("1. FALSE");
            }
        }
    }
}
