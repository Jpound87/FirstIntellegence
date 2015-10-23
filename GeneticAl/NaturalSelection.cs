using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thinker
{
    public class NaturalSelection : IEnumerable<double[][]>
    {
        IntellegentDesign motherNature; 
        Random cross = new Random();
        protected readonly static byte varNum = 4;
        protected const byte varAryNum = 1;
        protected double[][] varArray = new double[varNum][];
        protected double[] answer = new double[Program.BYTE_SIZE];
        private readonly byte unit = Program.BYTE_SIZE;
        private int x = 0;

        
        private int pop = 100*Program.BYTE_SIZE;
        public double[] ANS
        {
            get { return answer; }
        }
        public double[][] vA 
        {
            get { return varArray; }
        }
        public static byte vN
        {
            get { return varNum; }
        }
        public static byte vAN
        {
            get { return varAryNum; }
        }
        public int X
        {
            get { return x; }

        }
        public int population
        {
            get { return pop; }
            set 
            { 
                if (value <=  2147483647)
                pop = value; 
            }
        }
        private DNA[] organism;
        public IEnumerator<double[][]> GetEnumerator()
        {
            yield return varArray;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        private DNA[] populate()
        {
            motherNature = new IntellegentDesign();
            motherNature.initialize();

            this.Equation();
            
            organism = new DNA[pop];
            for (int c = 0; c < pop; c++)
            {
                organism[c] = new DNA();
            }
            return organism;
        }
        private void fitness()/////////////////////////////////////////
        {
            for (int c = 0; c < pop; c++)
            {
                double fitnum = organism[c].numberize();

                organism[c].fitlevel = ((varArray[1][0] * (Math.Pow(fitnum, varArray[0][0]))) - ((varArray[2][0]) * fitnum)) - (varArray[3][0]);//Insert problem here!!!

                if (organism[c].fitlevel == 0)
                {
                    organism[c].solver = true;
                }
            }
        }
        private void breed(ref DNA dna)
        {
            
            int crossat = cross.Next(unit);

            DNA partner = organism[0];
            if (crossat >= (Math.Pow(unit, 2)/2))
            {
                for (int i = crossat; i > 0; i--)
                {
                    partner.Strand[i] = dna.Strand[i];
                }
            }
            else
            {
                for (int i = crossat; i < 0; i++)
                {
                    partner.Strand[i] = dna.Strand[i];
                }
            }
            


        }
        private void mutate (ref DNA dna)
        {

            int changeat = cross.Next(unit);
            if (dna.Strand[changeat]==0)
            {
                dna.Strand[changeat] = 1;
            }
            else
            {
                dna.Strand[changeat] = 0;
            }
            if (cross.Next(101)>77)//about 33% chance
            {
                changeat = cross.Next(unit);
                if (dna.Strand[changeat]==0)
                {
                    dna.Strand[changeat] = 1;
                }
                else
                {
                    dna.Strand[changeat] = 0;
                }
            }
        }
        internal void darwin(double [] spark)//Hopefully m0ar better than nature!!!!
        {
            DNA rna = new DNA();
            bool evolution = new bool();
            int asteroid = (int)(cross.Next(1000) * Math.Pow(unit, 2));    
            this.populate();
            if (!(spark.Length==0))
            {
                organism[0].Strand = spark;
                organism[(int)pop / 2].Strand = spark;
                organism[pop - 1].Strand = spark;
            }
            this.fitness();
            DNA[] PrevEpoc = organism;
            do
            {
                if (asteroid <= 0)
                {
                    PrevEpoc = organism;
                    pop = 100 * Program.BYTE_SIZE;
                    
                    motherNature.Afterthought(varArray, ref spark);
                    PrevEpoc = organism;
                    organism = new DNA[pop];
                    for (int c = 0; c < pop; c++)
                    {
                        organism[c] = new DNA();
                    }
                    organism[(int)pop / 2].Strand = spark;
                    organism[pop - 1].Strand = spark;
                    organism[0] = PrevEpoc[0];
                    this.fitness();
                    asteroid = (int)(cross.Next(100*unit) * Math.Pow(unit, 2)); 
                }

                bool entropy = true;
                if (pop <= this.unit * 10)
                {
                    pop = 100 * Program.BYTE_SIZE;
                    PrevEpoc = organism;
                    organism = new DNA[pop];
                    for (int c = 0; c < pop; c++)
                    {
                        organism[c] = new DNA();
                    }
                    int i = 0;
                    foreach (DNA fossil in PrevEpoc)
                    {
                        organism[i] = fossil;
                        i++;
                    }
                    this.fitness();
                   
                }
                do
                {
                    entropy = false;
                    for (int c = 0; c < pop; c++)
                    {
                        if (organism[c].solver)
                        {
                            answer = organism[c].Strand;
                            evolution = true;
                            break;
                        }
                        if (evolution)
                        {
                            break;
                        }
                        if (cross.Next(101) > 97)//about 3% chance
                        {
                            this.mutate(ref organism[c]);
                        }
                        if (organism[c].fitlevel<0)
                        {
                            organism[c] = organism[pop - 1];
                            pop--;
                        }
                        if (c > 0)
                        {
                            
                            
                            if (organism[c].fitlevel < organism[c - 1].fitlevel)
                            {
                                rna = organism[c - 1];
                                organism[c - 1] = organism[c];
                                organism[c] = rna;
                                entropy = true;
                            }
                        }
                    }
                } while (entropy);
                
                if (evolution)
                {
                    break;
                }
 
                for (int c = 0; c < pop; c++)
                {
                    bool same = new bool();
                    int i = 0;
                    if (pop <= 10)
                    {
                        break;
                    }
                    if (c > 0)
                    {
                        foreach (double d in organism[c].Strand)
                        {
                            if (!(d.Equals(organism[c - 1].Strand[i])))
                            {
                                same = false;
                                break;
                            }
                        }
                    }
                    if (same)
                        {
                             organism[c] = organism[pop - 1];
                             pop--;
                        }
                    else if (organism[c].fitlevel == organism[pop - 1].fitlevel)
                    {
                        if (cross.Next(101) < 77)//about 77% chance
                        {
                            this.mutate(ref organism[c]);
                            this.mutate(ref organism[pop-1]);
                        }
                        else
                        {
                            pop--;
                        }
                            
                    }
                    else if (organism[c].fitlevel < organism[(int)(pop / 6)].fitlevel)
                    {
                        this.breed(ref organism[c]);

                    }
                    else
                    {
                        this.mutate(ref organism[c]);
                    }

                    
                }

                asteroid--;
            } while (!evolution);

   
        
        }
        private void Equation()
        {

            do
            {
                x = cross.Next((int)(2* unit));
            }while(x==0);
            

            for (byte b = 0; b < varAryNum; b++)
            {
                for (byte i = 0; i < varNum; i++)
                {
                    int n;
                    do
                    {
                        n = cross.Next((int)(2 * unit));
                    } while (n == 0);
                    varArray[i] = new double[varAryNum] { n };
                }
            }
            if (varArray[0][0]>unit)
            {
                varArray[0][0]=varArray[0][0] - unit;
            }
            if (varArray[0][0] > (int)(unit / 2))
            {
                varArray[0][0] = varArray[0][0] - (int)(unit/2);
            }

            double fixer = ((varArray[1][0] * (Math.Pow(x, varArray[0][0]))) - ((varArray[2][0]) * x)) - (varArray[3][0]);

            varArray[3][0] = varArray[3][0] + fixer;
           
            if ((varArray[3][0])>=0)
            {
                Console.WriteLine("The problem is:\n{1}x^{0} = {2}x+{3}", varArray[0][0], varArray[1][0], varArray[2][0], varArray[3][0]);
            }
            else
            {
                Console.WriteLine("The problem is:\n{1}x^{0} = {2}x{3}", varArray[0][0], varArray[1][0], varArray[2][0], varArray[3][0]);
            }
              
        }
        
            
        
        
    }            
}
