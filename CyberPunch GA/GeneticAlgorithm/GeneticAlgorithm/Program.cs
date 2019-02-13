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
            FitnessCalculator.SetSolution("1111000000000001111000000000000000000000000000000000000000001111");

            Population pop = new Population(50, true);

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

            System.Console.ReadLine();
        }
    }
}