using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class FitnessCalculator
    {
        //Temp - replace hardcoded index value
        static byte[] solution = new byte[Individual.GetDefaultGeneLength()];

        public static void SetSolution(String newSolution)
        {
            solution = new byte[newSolution.Length];

            for (int i = 0; i < newSolution.Length; i++)
            {
                String character = newSolution.Substring(i, 1);
                if (character.Contains("0") || character.Contains("1"))
                {
                    solution[i] = byte.Parse(character);
                }
                else
                {
                    solution[i] = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public static int GetFitness(Individual individual)
        {
            int fitness = 0;

            for (int i = 0; i < individual.Size() && i < solution.Length; i++)
            {
                if (individual.GetGene(i) == solution[i])
                {
                    fitness++;
                }
            }
            return fitness;
        }

        /// <summary>
        /// Get the optimum fitness
        /// </summary>
        /// <returns></returns>
        public static int GetMaxFitness()
        {
            int maxFitness = solution.Length;
            return maxFitness;
        }
    }
}
