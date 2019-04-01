using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Algorithm
    {
        private static double uniformRate = 0.5;
        private static double mutationRate = 0.010;
        private static int tournamentSize = 5;
        private static Boolean elitism = true;

        public static Population EvolvePopulation(Population p)
        {
            Population newPopulation = new Population(p.Size(), false);

            if (elitism)
            {
                newPopulation.SaveIndividual(0, p.GetFittest());
            }

            int elitismOffset;
            if (elitism)
            {
                elitismOffset = 1;
            }
            else
            {
                elitismOffset = 0;
            }

            for (int i = elitismOffset; i < p.Size(); i++)
            {
                Individual indiv1 = TournamentSelection(p);
                Individual indiv2 = TournamentSelection(p);
                Individual newIndiv = Crossover(indiv1, indiv2);
                newPopulation.SaveIndividual(i, newIndiv);
            }

            for (int i = elitismOffset; i < newPopulation.Size(); i++)
            {
                Mutate(newPopulation.GetIndividual(i));
            }

            return newPopulation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indiv1"></param>
        /// <param name="indiv2"></param>
        /// <returns></returns>
        private static Individual Crossover(Individual indiv1, Individual indiv2)
        {
            Individual newSolution = new Individual();
            Random rndgen = new Random();

            for (int i = 0; i < indiv1.Size(); i++)
            {
                if (rndgen.NextDouble() <= uniformRate)
                {
                    newSolution.SetGene(i, indiv1.GetGene(i));
                }
                else
                {
                    newSolution.SetGene(i, indiv2.GetGene(i));
                }
            }
            return newSolution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indiv"></param>
        private static void Mutate(Individual indiv)
        {
            Random rndgen = new Random();

            for (int i = 0; i < indiv.Size(); i++)
            {
                if (rndgen.NextDouble() <= mutationRate)
                {
                    byte gene = (byte)Math.Round((float)rndgen.NextDouble());
                    indiv.SetGene(i, gene);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Individual TournamentSelection(Population p)
        {
            Population tournament = new Population(tournamentSize, false);
            Random rndgen = new Random();

            for (int i = 0; i < tournamentSize; i++)
            {
                int randomID = (int)(rndgen.NextDouble() * p.Size());
                tournament.SaveIndividual(i, p.GetIndividual(randomID));
            }
            Individual fittest = tournament.GetFittest();
            return fittest;
        }
    }
}