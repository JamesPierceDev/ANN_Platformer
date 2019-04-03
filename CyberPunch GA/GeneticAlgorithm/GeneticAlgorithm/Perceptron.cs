using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Perceptron
    {
        Random rndgen;
        float[] m_inputs = new float[2]; //input neurons
        int[] m_outputs; //output neurons
        int m_input_size;
        double[] m_weights; //Weights 
        double learningRate; //Learning rate constant for learning rule
        double threshold; //Threshold value for T function
        double totalError; //Error count for ranking neurons

        /// <summary>
        /// Perceptron constructor takes a number of inputs
        /// number of iterations and a learning rate as 
        /// params.
        /// </summary>
        /// <param name="n_inputs"></param>
        /// <param name="n_iter"></param>
        /// <param name="l_rate"></param>
        public Perceptron(int n_inputs, int l_rate)
        {
            m_input_size = n_inputs;
            m_inputs = new float[n_inputs];
            learningRate = l_rate;
            rndgen = new Random();
            RandomiseWeights();
        }

        /// <summary>
        /// Sigmoid function
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        float Sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        /// <summary>
        /// Recified linear unit function.
        /// Returns zero if input is less than 0
        /// otherwise returns the input
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        float Relu(float x)
        {
            return x < 0 ? 0 : x;
        }

        /// <summary>
        /// Generates random values for weights
        /// </summary>
        void RandomiseWeights()
        {
            //Loop through weights giving them random values
            for (byte i = 0; i < m_weights.Length; i++)
            {
                m_weights[i] = rndgen.NextDouble();
            }
        }

        float[] Threshold()
        {
            //Sum the inputs and weights
            float[] hx = new float[2];
           
            for (byte i = 0; i < m_input_size; i++)
            {
                hx[i] = m_inputs[0] * (float)m_weights[0] + m_inputs[1] * (float)m_weights[1];

                for (byte j = 0; j < m_inputs.Length; j++)
                {
                    hx[i] = m_inputs[i] * (float)m_weights[i] + m_inputs[j] * (float)m_weights[j];
                }
            }
            //Return hx passed through sigmoid activation function
            return Sigmoid(hx);
        }

        /// <summary>
        /// Implementation of the
        /// Perceptron Learning Rule
        /// </summary>
        void Train()
        {
            //Compute perceptron output
            float output = Threshold();
            float desired = 0;

            if (output == desired)
            {
                System.Console.WriteLine("Dataset match");
            }
            else
            {
                //Adjust weights with learning rule
                for (byte i = 0; i < m_weights.Length; i++)
                {
                    m_weights[i] = m_weights[i] * learningRate * (desired - output) * m_inputs[i];
                }
            }
        }
    }
}