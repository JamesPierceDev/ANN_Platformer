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
        //double[] m_weights; //Weights 
        List<double> m_weights;
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
            //m_weights = new double[n_inputs];
            m_weights = new List<double>();
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

        /// <summary>
        /// Returns the single activated
        /// outputs as a float
        /// </summary>
        /// <returns></returns>
        public float GetOutput()
        {
            return m_output;
        }

        /// <summary>
        /// Returns the array of connection
        /// weights as an array of doubles
        /// </summary>
        /// <returns></returns>
        public List<double> GetWeights()
        {
            return m_weights;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        public void UpdateWeights(List<double> w)
        {
            m_weights = w;
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
            for(byte i = 0; i < m_input_size; i++)
            {
                m_weights[i] = rndgen.NextDouble();
            }
        }

        public void Sum()
        {
            activeInputs++;

            if (activeInputs == m_input_size)
            {
                //Return hx passed through sigmoid activation function
                hx += m_inputs[activeInputs - 1] * (float)m_weights[activeInputs - 1];
                m_output = Sigmoid(hx);
                //m_output = Relu(hx);
            }
            else
            {
                //Sum the inputs and weights
                hx += m_inputs[activeInputs] * (float)m_weights[activeInputs];
            }
        }

        /// <summary>
        /// Utilises a Genetic Algorithm to train
        /// the perceptron weights
        /// </summary>
        public void Train()
        {
            //Set population to a concatenation of the weight values
        }
    }
}