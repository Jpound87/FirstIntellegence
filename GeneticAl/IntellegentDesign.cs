using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thinker
{
    
    public class IntellegentDesign
    {
        private readonly byte unit = Program.BYTE_SIZE;
        private readonly byte max = (byte)(Program.BYTE_SIZE * NaturalSelection.vN);
        private int game; 
        private NeuralNet net = new NeuralNet();
        private NeuralNet precog = new NeuralNet();
        private double[] lastAns = new double[Program.BYTE_SIZE];
        private double[][] input = new double[1][];
        private double[][] output = new double[1][];
        
        internal void initialize()
        {
            net.Initialize(1, max, max, unit);
            precog.Initialize(1, max, max, unit);

        }
        internal double [] dueronomy(double[][] helix)
        {
            {
                double[] add = new double[max];
                double[] ans = new double[unit];
                double[][] question = new double[NaturalSelection.vN][];
                int passover = 0;
                int addendum;
                byte num = 0;
                for (byte b = 0; b < NaturalSelection.vAN; b++)
                {
                    foreach (double[] d in helix)
                    {
                        question[num] = this.BinaryRedux(d[b]);
                        fuzzify(ref question[num]);
                        addendum = unit + passover;
                        for (int c = passover; c < addendum; c++)
                        {
                            byte slider = 0;
                            add[c] = question[num][slider];
                            slider++;
                        }
                        passover += unit;
                        num++;
                    }
                }            

                NeuralNet.PreparePerceptionLayerForPulse(precog, add);
                precog.Pulse();
                precog.ApplyLearning();
                
                return ans;

            }
        }
        internal double genesis(double [][] helix, double[] ans)
        {


            double[] muse = new double[unit];
            double[] add = new double[max];
            double[][] question = new double[NaturalSelection.vN][];
            int passover = 0;
            int addendum;
            byte num = 0;
            for (byte b = 0; b < NaturalSelection.vAN; b++)
            {
                foreach (double[] d in helix)
                {
                    question[num] = this.BinaryRedux(d[b]);
                    fuzzify(ref question[num]);
                    addendum = unit + passover;
                    for (int c = passover; c < addendum; c++)
                    {
                        byte slider = 0;
                        add[c] = question[num][slider];
                        slider++;
                    }
                    passover += unit;
                    num++;
                }
            }      
       
            bool end;
            int count = 0;

            if (game >= 100)
            {
                game = this.Afterthought(helix, ref muse);
            }
            game+=100;


            input[0] = add;
            output[0] = ans;
            do
            {
                end = false;
                count++;
                net.Train(input, output, TrainingType.BackPropogation, 1);
                
                for (byte c = 0; c<unit; c++)
                {
                    
                    double check = Math.Abs(net.OutputLayer[c].Output - ans[c]);
                    if (check>.1)
                        end = true;
                }
                
            }while (end);
            for (byte c = 0; c < unit; c++ )
            {
                ans[c] = net.OutputLayer[c].Output;
            }
            lastAns = ans;
            antifuzzify(ref ans);
           
            
            return BinaryAddux(ans);
            
            

                
        }
        internal int Afterthought(double [][] helix, ref double [] ans)
        {
            double[] add = new double[max];
            double[][] question = new double[NaturalSelection.vN][];
            int passover = 0;
            int addendum;
            byte num = 0;
            for (byte b = 0; b < NaturalSelection.vAN; b++)
            {
                foreach (double[] d in helix)
                {
                    question[num] = this.BinaryRedux(d[b]);
                    fuzzify(ref question[num]);
                    addendum = unit+passover;
                    for (int c = passover; c < addendum; c++)
                    {
                        byte slider = 0;
                        add[c] = question[num][slider];
                        slider++;
                    }
                    passover += unit;
                    num++;
                }
            }      

            input[0] = add;
            output[0] = lastAns;


            bool end;
            int count = 0;
            
            
            do
            {
                end = false;
                count++;
                precog.Train(input, output, TrainingType.BackPropogation, 1);

                for (byte c = 0; c < unit; c++)
                {

                    double check = Math.Abs(precog.OutputLayer[c].Output - lastAns[c]);
                    if (check > .01)
                        end = true;
                }
            } while (end&&(count<1000));
            for (byte c = 0; c < unit; c++)
            {
                ans[c] = precog.OutputLayer[c].Output;
            }
            antifuzzify(ref ans);
            

            Console.Write("Guess Value: ");
            Console.WriteLine(BinaryAddux(ans));
            return count;
        }
        private double[] BinaryRedux(double num)
        {
            double[] helix = new double[unit];
            byte c = (byte)(unit-1);
            do
            {
                if (num>=Math.Pow(2,c))
                {
                    num -= Math.Pow(2, c);
                }
                c--;   
                

            } while (c != 0);
            return helix;
        }
        private double BinaryAddux(double[] helix)
        {
            double num = 0;
            byte i = (byte)(unit - 1);
            for (byte c = 0; c < unit; c++, i--)
            {
                if (helix[c]==1)
                {
                    num+=Math.Pow(2,i);
                }
                
            }
            return num;
        }
        private void fuzzify(ref double[] helix)
        {
            int c = 0;
            
            double high, mid, low;
            high = .9;
            low = .1;
            mid = .5;
            
            foreach (double d in helix)
            {
                if (d == 0)
                    helix[c] = low;
                else if (d == 1)
                    helix[c] = high;
                else
                    helix[c] = mid;
                c++;
            }

        }
        private void antifuzzify(ref double[] helix)
        {
            for (byte c = 0; c < unit; c++)
            {
                if (helix[c] < .5)
                {
                    helix[c] = 0;
                }
                else
                {
                    helix[c] = 1;
                }
            }

        }

    }
}            
            
