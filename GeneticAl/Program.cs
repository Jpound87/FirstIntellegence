using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Thinker
{

    class Program
    {
        protected static byte unit = 8;
        protected static double [] spark;
        public static byte BYTE_SIZE
        {
            get { return unit; }
            set
            {
                unit = value;
            }
        }
        private static double Ans = 0;
        private static int novelAns = 0;
        private const int cap = 1;
        static void Main(string[] args)
        {
            DNA firstStep = new DNA();
            spark = firstStep.Strand;
            bool t = false;
            int iteration = 0;
            
            ConsoleKeyInfo end = new ConsoleKeyInfo();
            Console.WriteLine("AI solver will attempt equations after GA designs them");
            NaturalSelection ns = new NaturalSelection();
            IntellegentDesign id = new IntellegentDesign();
            id.initialize();
            ns.darwin(spark);
            do
            {
                
                    
                if (t==false)
                {
                    t = true;
                    Root(ns,id);
                }
                ns.darwin(id.dueronomy(ns.vA));
                Root(ns, id);
                if (iteration>100||novelAns>=cap)
                {
                    end = Console.ReadKey();
                    iteration = 0;
                    novelAns = 0;
                }
                iteration++;
                

            } while (end.Key != ConsoleKey.Escape);
            

        }
        private static void Root(NaturalSelection N, IntellegentDesign I)
        {
            
            Ans = I.genesis(N.vA, N.ANS);
            if (Ans != N.X)
            {
                Console.WriteLine("Algorithm found novel answer: {0}", Ans);
                Console.WriteLine("Original Root: {0}", N.X);
                novelAns++;
            }
            else
            {
                Console.WriteLine("Algorithm found trivial answer: {0}", Ans);
            }
            Console.WriteLine("------------------------------------------------------");

        }
    }
    

}
