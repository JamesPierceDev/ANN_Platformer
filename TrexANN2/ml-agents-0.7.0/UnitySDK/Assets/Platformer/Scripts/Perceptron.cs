using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron
{
    public static string m_ID;
    System.Random rndgen;
    int activeInputs = 0;
    float[] m_inputs; //input neurons
    float m_output; //output neurons
    public int m_input_size;
    List<double> m_weights;
    readonly double learningRate; //Learning rate constant for learning rule
    float hx = 0;

    /// <summary>
    /// Perceptron constructor takes a number of inputs
    /// number of iterations and a learning rate as 
    /// params.
    /// </summary>
    /// <param name="n_inputs"></param>
    /// <param name="n_iter"></param>
    /// <param name="l_rate"></param>
    public Perceptron(int n_inputs, double l_rate, string id)
    {
        m_ID = id;
        m_input_size = n_inputs;
        m_inputs = new float[n_inputs];
        //m_weights = new double[n_inputs];
        m_weights = new List<double>();
        learningRate = l_rate;
        rndgen = new System.Random();
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
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    float Softmax(float x)
    {
        float xp = Mathf.Exp(x - Mathf.Max(x));
        return xp / (xp * x);
    }

    /// <summary>
    /// Generates random values for weights
    /// </summary>
    void RandomiseWeights()
    {
        //Loop through weights giving them random values
        for (byte i = 0; i < m_input_size; i++)
        {
            //m_weights.Add(rndgen.NextDouble());
           //m_weights.Add(rndgen.Next(-4, 4));
            m_weights.Add(GetRandom(0, 4));
        }
    }

    public void Sum()
    {
        activeInputs++;

        if (activeInputs == m_input_size)
        {
            //Return hx passed through sigmoid activation function
            hx += m_inputs[activeInputs - 1] * (float)m_weights[activeInputs - 1];
            Debug.Log("Weight values: " + m_weights[activeInputs - 1]);

            m_output = Softmax(hx);
            //m_output = Sigmoid(hx);
            //m_output = Relu(hx);

        }
        else if (activeInputs > m_input_size)
        {
        }
        else
        {
            //Sum the inputs and weights
            hx += m_inputs[activeInputs - 1] * (float)m_weights[activeInputs - 1];
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

    /// <summary>
    /// Generates a random double within
    /// the range specified
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public double GetRandom(double min, double max)
    {
        return rndgen.NextDouble() * (max - min) + min;
    }
}