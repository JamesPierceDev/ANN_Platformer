using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Individual
    {
        static int defaultGeneLength = 64;
        private byte[] genes = new byte[defaultGeneLength];
        Random rndgen = new Random();

        private int fitness = 0;

        /// <summary>
        /// Create a random individual
        /// </summary>
        public void GenIndividual()
        {
            for (int i = 0; i < Size(); i++)
            {
                byte gene = (byte)Math.Round((float)rndgen.Next(0, 2));
                genes[i] = gene;
            }
        }

        /// <summary>
        /// Sets the default length of the genes
        /// </summary>
        /// <param name="length"></param>
        public static void SetDefaultGeneLength(int length)
        {
            defaultGeneLength = length;
        }

        /// <summary>
        /// Returns the defaultGeneLength
        /// </summary>
        /// <returns></returns>
        public static int GetDefaultGeneLength()
        {
            return defaultGeneLength;
        }

        /// <summary>
        /// Returns the gene at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte GetGene(int index)
        {
            return genes[index];
        }

        /// <summary>
        /// Sets the gene at the given index to
        /// the value and sets its fitness to zero
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetGene(int index, byte value)
        {
            genes[index] = value;
            fitness = 0;
        }

        /// <summary>
        /// Returns the fitness of the gene
        /// </summary>
        /// <returns></returns>
        public int GetFitness()
        {
            if(fitness == 0)
            {
                fitness = FitnessCalculator.GetFitness(this);
            }
            return fitness;
        }


        /// <summary>
        /// Return the length of the genes[]
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return genes.Length;
        }

        /// <summary>
        /// Override ToString function to
        /// work with genes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String geneStr = "";
            for (int i = 0; i < Size(); i++)
            {
                geneStr += GetGene(i);
            }
            return geneStr;
        }
    }
}
