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
        int activeInputs = 0;
        float[] m_inputs; //input neurons
        float m_output; //output neurons
        public int m_input_size;
        double[] m_weights; //Weights 
        readonly double learningRate; //Learning rate constant for learning rule
        readonly double threshold; //Threshold value for T function
        readonly double totalError; //Error count for ranking neurons
        float hx = 0;

        /// <summary>
        /// Perceptron constructor takes a number of inputs
        /// number of iterations and a learning rate as 
        /// params.
        /// </summary>
        /// <param name="n_inputs"></param>
        /// <param name="n_iter"></param>
        /// <param name="l_rate"></param>
        public Perceptron(int n_inputs, double l_rate)
        {
            m_input_size = n_inputs;
            m_inputs = new float[n_inputs];
            m_weights = new double[n_inputs];
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
        /// Sets the values of the inputs
        /// </summary>
        /// <param name="m_in"></param>
        public void SetInput(int index, float value)
        {
            m_inputs[index] = value;
        }

        public float GetOutput()
        {
            return m_output;
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

        public void Sum()
        {
            
            //Sum the inputs and weights

           
            if (m_input_size > 1)
            {   
                if (activeInputs == m_input_size)
                {
                    //Return hx passed through sigmoid activation function
                    hx += m_inputs[activeInputs] * (float)m_weights[activeInputs];
                    m_output = Sigmoid(hx);
                }
                else
                {
                    hx += m_inputs[activeInputs] * (float)m_weights[activeInputs];
                }
            }
            else
            {
                hx += m_inputs[activeInputs] * (float)m_weights[activeInputs];
                m_output = Sigmoid(hx);
            }
            activeInputs++;
        }

        /// <summary>
        /// Implementation of the
        /// Perceptron Learning Rule
        /// </summary>
        public void Train()
        {
            //Compute perceptron output
            //float desired = 0; // Some value from GA

            //If actual value matches expected value
            //if (output == desired)
            //{
            //    System.Console.WriteLine("Dataset match");
            //}
            //else //If there is a difference
            //{
            //    //Adjust weights with learning rule
            //    for (byte i = 0; i < m_weights.Length; i++)
            //    {
            //        m_weights[i] = m_weights[i] * learningRate * (desired - output) * m_inputs[i];
            //    }
            //}
        }
    }
}