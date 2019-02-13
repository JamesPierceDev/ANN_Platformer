using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Population
    {
        Individual[] individuals;

        /// <summary>
        /// Create a new population
        /// </summary>
        /// <param name="populationSize"></param>
        /// <param name="initialise"></param>
        public Population(int populationSize, Boolean initialise)
        {
            individuals = new Individual[populationSize];

            if (initialise)
            {
                for (int i = 0; i < Size(); i++)
                {
                    Individual newIndiv = new Individual();
                    newIndiv.GenIndividual();
                    SaveIndividual(i, newIndiv);
                }
            }
        }

        /// <summary>
        /// Return the individual at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Individual GetIndividual(int index)
        {
            return individuals[index];
        }

        /// <summary>
        /// Gets the fittest gene in the population
        /// </summary>
        /// <returns></returns>
        public Individual GetFittest()
        {
            Individual fittest = individuals[0];

            for (int i = 0; i < Size(); i++)
            {
                if (fittest.GetFitness() <= GetIndividual(i).GetFitness())
                {
                    fittest = GetIndividual(i);
                }
            }
            return fittest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return individuals.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="indiv"></param>
        public void SaveIndividual(int index, Individual indiv)
        {
            individuals[index] = indiv;
        }
    }
}
