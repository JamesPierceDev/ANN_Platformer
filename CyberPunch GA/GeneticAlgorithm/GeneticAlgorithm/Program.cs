using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            MLP testMLP = new MLP(2, 3, 1);

            testMLP.AddInput(0, 10.5f);
            testMLP.AddInput(1, 7.25f);
            testMLP.GenerateOutput();
            System.Console.WriteLine("MLP output: " + testMLP.m_out);

            System.Console.ReadLine();
        }

        void RunGA()
        {
            FitnessCalculator.SetSolution("1111000000000001111000000000000000000000000000000000000000001111");

            Population pop = new Population(6, true);

            int epoch = 0;
            while (pop.GetFittest().GetFitness() < FitnessCalculator.GetMaxFitness())
            {
                epoch++;
                pop = Algorithm.EvolvePopulation(pop);
                System.Console.WriteLine("Generation: " + epoch + " Fittest: " + pop.GetFittest().GetFitness());
                System.Console.WriteLine(pop.GetFittest());
            }
            System.Console.WriteLine("Solution found");
            System.Console.WriteLine("Generation: " + epoch);
            System.Console.WriteLine("Genes:");
            System.Console.WriteLine(pop.GetFittest());
        }
    }
}