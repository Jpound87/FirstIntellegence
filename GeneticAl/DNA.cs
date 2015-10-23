using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thinker
{
    internal class DNA : IEnumerable<double[]>
    {
        private readonly byte lim = Program.BYTE_SIZE;
        private double[] strand = new double[Program.BYTE_SIZE];
        internal double fitlevel { get; set; }
        internal bool solver { get; set; }
        public double[] Strand
        {
            get { return strand; }
            set
            {
                if (conforming(value))
                {
                    strand = value;
                }
                else
                {
                    strand = this.birth();
                }
            }
        }
        public IEnumerator<double[]> GetEnumerator()
        {
            yield return strand;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public DNA()
        {
            strand = this.birth();
        }
        private static int randorize;
        private Random seed = new Random((int) DateTime.Now.Ticks & randorize);
        private double[] birth()
        {
            
            solver = false;
            for (int c = 0; c < lim; c++)
            {
                int zygote = seed.Next(256);
                zygote = (zygote % 2);
                strand[c] = zygote;
                zygote = seed.Next(256);
                int b = zygote % lim;

                if (seed.Next(256) > 128)
                {
                    zygote = (zygote % 2);
                    strand[b] = zygote;
                }
                else if (c == (double)b)
                {
                    if (c != 0)
                        strand[c - 1] = 1;
                }
                else if (c >= 1)
                {
                    if (strand[c - 1] == 0)
                    {
                        strand[c] = 1;
                        strand[b] = 0;
                    }
                }
            }
            randorize = seed.Next((int)DateTime.Now.Ticks & 0x0000FFFF);
            return strand;

        }
        private bool conforming(double[] val)
        {
            double c = 0;
            foreach (double b in val)
            {
                c++;
                if (c < lim)
                {
                    if (b == 1)
                    {
                        break;
                    }
                    else if (b == 0)
                    {
                        break;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        internal double numberize()
        {

            {
                double num = 0;
                byte i = (byte)(lim - 1);
                for (byte c = 0; c < lim; c++, i--)
                {
                    if (this.strand[c] == 1)
                    {
                        num += Math.Pow(2, i);
                    }

                }
                return num;
            }

        }
    }
}
