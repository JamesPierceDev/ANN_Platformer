using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{   
    public int populationSize;
    public bool initialise;
    //List<GameAgent> m_agents = new List<GameAgent>();
    //public List<GameObject> m_objs = new List<GameObject>();
    public List<GameObject> m_agents = new List<GameObject>();
    int size = 0;
    private const float mutationRate = 0.2f;
    private const float crossoverRate = 0.8f;
    int generation = 0;
    int tournamentSize = 3;
    private readonly bool elitism = false;
    System.Random rndgen = new System.Random();

    void Start()
    {
        //m_agents = new List<GameAgent>();
        size = populationSize;

        for (int i = 0; i < size; i++)
        {
           // m_agents.Add(m_objs[i].GetComponent<GameAgent>());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        for (int i = 0; i < size; i++)
        {
            m_agents[i].GetComponent<GameAgent>().UpdateAgent();
        }
        Evolve();
    }

    /// <summary>
    /// 
    /// </summary>
    void Evolve()
    {
        generation++;
        //System.Console.WriteLine("Generation: " + generation);
        int fittest;

        fittest = GetFittest();
        //Debug.Log("Fittness: " + GetFittness());

        for (int i = 0; i < size; i++)
        {
            //int indexA = TournamentSelect();
            //int indexB = TournamentSelect();
            int indexA = rndgen.Next(0, size);
            int indexB = rndgen.Next(0, size);

            if (indexA != indexB)
            {
                CrossOver(m_agents[indexA].GetComponent<GameAgent>().GetNetwork(), m_agents[indexB].GetComponent<GameAgent>().GetNetwork());
            }
            Mutate(m_agents[i].GetComponent<GameAgent>().GetNetwork());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="net"></param>
    /// <returns></returns>
    void Mutate(MLP net)
    {
        List<double> w = new List<double>();
        System.Random rndgen = new System.Random();
        w = net.GetNetworkWeights();
        for (byte i = 0; i < net.n_input; i++)
        {
            if (rndgen.NextDouble() <= mutationRate)
            {
                w[i] = Math.Round(rndgen.NextDouble());
            }
        }
        net.SetNetworkWeights(w);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    void CrossOver(MLP a, MLP b)
    {
        System.Random rndgen = new System.Random();

        List<double> w1 = new List<double>();
        List<double> w2 = new List<double>();
        w1 = a.GetNetworkWeights();
        w2 = b.GetNetworkWeights();

        for (byte i = 0; i < a.GetNetworkWeights().Count; i++)
        {
            if (rndgen.NextDouble() <= crossoverRate)
            {
                w1[i] = w2[i];
            }
        }
        a.SetNetworkWeights(w1);
    }

    /// <summary>
    /// Return the individual at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GameAgent GetAgent(int index)
    {
        //return m_agents[index];
        return m_agents[index].GetComponent<GameAgent>();
    }

    /// <summary>
    /// Needs fixing to return actual tournament fitness
    /// </summary>
    /// <returns></returns>
    int TournamentSelect()
    {
        List<int> fitnesses = new List<int>();

        for (int i = 0; i < tournamentSize; i++)
        {
            fitnesses.Add(m_agents[i].GetComponent<GameAgent>().GetFitness());
        }

        int fittest = 0;
        for (int i = 0; i < fitnesses.Count; i++)
        {
            if (fitnesses[i] > fittest)
            {
                fittest = i;
            }
        }
        System.Random rndgen = new System.Random();
        int num = rndgen.Next(0, size);
        return num;
    }

    /// <summary>
    /// Gets the fittest gene in the population
    /// </summary>
    /// <returns></returns>
    public int GetFittest()
    {
        //int fittest = m_agents[0].GetFitness();
        int fittest = m_agents[0].GetComponent<GameAgent>().GetFitness();
        int index = 0;

        for (int i = 0; i < Size(); i++)
        {
            if (m_agents[i].GetComponent<GameAgent>().GetFitness() > fittest)
            {
                fittest = m_agents[0].GetComponent<GameAgent>().GetFitness();
                index = i;
            }
        }
        return index;
    }

    public int GetFittness()
    {
        //return m_agents[GetFittest()].GetFitness();
        return m_agents[0].GetComponent<GameAgent>().GetFitness();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int Size()
    {
        return size;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="indiv"></param>
    public void SaveAgent(int index, GameAgent a)
    {
        //m_agents[index] = a;
        //m_agents[index].GetComponent<GameAgent>() = a;
    }
}