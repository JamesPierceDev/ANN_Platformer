using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class MLP
    {
        readonly int n_input;
        readonly int n_hidden;
        readonly int n_output;
        readonly double learning_rate = 0.2;

        List<Perceptron> m_inputs_layer = new List<Perceptron>();
        List<Perceptron> m_hidden_layer = new List<Perceptron>();
        List<Perceptron> m_output_layer = new List<Perceptron>();
        public float m_out;

        /// <summary>
        /// Multilayer perceptron constructor
        /// </summary>
        public MLP(int n_in, int n_h, int n_out)
        {
            n_input = n_in;
            n_hidden = n_h;
            n_output = n_out;

            //Populate lists for input, hidden, and output layers
            for (byte i = 0; i < n_input; i++)
            {
                m_inputs_layer.Add(new Perceptron(1, learning_rate));
            }
            for (byte i = 0; i < n_hidden; i++)
            {
                m_hidden_layer.Add(new Perceptron(n_input, learning_rate));
            }
            for (byte i = 0; i < n_output; i++)
            {
                m_output_layer.Add(new Perceptron(n_hidden, learning_rate));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateOutput()
        {
            for (byte i = 0; i < n_input; i++)
            {
                for (byte j = 0; j < m_inputs_layer[i].m_input_size; j++)
                {
                    m_inputs_layer[i].Sum();
                }
            }

            for (byte i = 0; i < n_hidden; i++)
            {
                for (byte j = 0; j < n_input; j++)
                {
                    m_hidden_layer[i].SetInput(j, m_inputs_layer[j].GetOutput());
                }

            }

            for (byte i = 0; i < n_hidden; i++)
            {
                for (byte j = 0; j < m_hidden_layer[i].m_input_size; j++)
                {
                    m_hidden_layer[i].Sum();
                }
            }

            for (byte i = 0; i < n_output; i++)
            {
                for (byte j = 0; j < n_hidden; j++)
                {
                    m_output_layer[i].SetInput(j, m_hidden_layer[j].GetOutput());
                }
            }

            for (byte i = 0; i < n_output; i++)
            {
                for (byte j = 0; j < m_output_layer[i].m_input_size; j++)
                {
                    m_output_layer[i].Sum();
                }
            }
            m_out = m_output_layer[0].GetOutput();   
        }

        /// <summary>
        /// 
        /// </summary>
        public void TrainNetwork()
        {
            //Change fitnessCalculator function
            int populationSize = n_input + n_hidden + n_output;
            Population p = new Population(populationSize, true);

        }

        /// <summary>
        /// Returns a list<double> of all the input,
        /// hidden, and output layer weights in the network
        /// </summary>
        /// <returns></returns>
        List<double> GetNetworkWeights()
        {
            List<double> temp = new List<double>();
            for (byte i = 0; i < n_input; i++)
            {
                temp.Concat(m_inputs_layer[i].GetWeights());
            }
            for (byte i = 0; i < n_hidden; i++)
            {
                temp.Concat(m_hidden_layer[i].GetWeights());
            }
            for (byte i = 0; i < n_output; i++)
            {
                temp.Concat(m_output_layer[i].GetWeights());
            }
            return temp;
        }

        double[] ConcatenateWeightArrays(double[] a, double[] b)
        {
            double[] x = new double[a.Length + b.Length];
            a.CopyTo(x, 0);
            b.CopyTo(x, a.Length);
            return x;
        }

        double[] ConcatenateWeightArrays(double[] a, double[] b, double[] c)
        {
            double[] x = new double[a.Length + b.Length + c.Length];
            a.CopyTo(x, 0);
            b.CopyTo(x, a.Length);
            c.CopyTo(x, b.Length);
            return x;
        }

        /// <summary>
        /// Adds value in the input layer at the
        /// index specified
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void AddInput(int index, float value)
        {
            m_inputs_layer[index].SetInput(0, value);
        }
    }
}