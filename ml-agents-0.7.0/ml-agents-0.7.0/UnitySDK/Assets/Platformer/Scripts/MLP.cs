using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MLP
{
    public readonly int n_input;
    public readonly int n_hidden;
    public readonly int n_output;
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
    public float GenerateOutput()
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
        return m_output_layer[0].GetOutput();
    }

    /// <summary>
    /// Returns a list<double> of all the input,
    /// hidden, and output layer weights in the network
    /// </summary>
    /// <returns></returns>
    public List<double> GetNetworkWeights()
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="w"></param>
    public void SetNetworkWeights(List<double> w)
    {
        for (byte i = 0; i < n_input; i++)
        {
            List<double> iw = new List<double>();
            iw[i] = w[i];
            m_inputs_layer[i].UpdateWeights(iw);
        }
        for (byte i = 0; i < n_hidden; i++)
        {
            List<double> hw = new List<double>();
            hw[i] = w[i];
            m_hidden_layer[i].UpdateWeights(hw);
        }
        for (byte i = 0; i < n_output; i++)
        {
            List<double> ow = new List<double>();
            ow[i] = w[i];
            m_output_layer[i].UpdateWeights(ow);
        }
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
