using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Population : ScriptableObject{ 

    List<GameAgent> m_agents;
    int size = 0;
    private const float mutationRate = 0.2f;
    private const float crossoverRate = 0.8f;
    int generation = 0;
    int tournamentSize = 5;
    private readonly bool elitism = false;

    /// <summary>
    /// Create a new population
    /// </summary>
    /// <param name="populationSize"></param>
    /// <param name="initialise"></param>
    public Population(int populationSize, Boolean initialise)
    {
        m_agents = new List<GameAgent>();
        size = populationSize;
        if (initialise)
        {
            for (int i = 0; i < Size(); i++)
            {
                m_agents.Add(new GameAgent());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    void Evolve()
    {
        generation++;
        Debug.Log("Generation: " + generation);
        int fittest;
        GameAgent a;
        GameAgent b;

        fittest = GetFittest();
        Debug.Log("Fitness: " + m_agents[fittest].GetFitness());
        List<GameAgent> newAgentSet = new List<GameAgent>();

        for (int i = 0; i < Size(); i++)
        {
            int indexA = TournamentSelect();
            int indexB = TournamentSelect();
            a = new GameAgent();
            a.SetNetwork(CrossOver(m_agents[indexA].GetNetwork(), m_agents[indexB].GetNetwork()));
            a.SetNetwork(Mutate(a.GetNetwork()));
            newAgentSet[i] = a;
        }

        for (int i = 0; i < Size(); i++)
        {
            m_agents[i] = newAgentSet[i];
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="net"></param>
    /// <returns></returns>
    MLP Mutate(MLP net)
    {
        MLP newNet = net;

        List<double> w = new List<double>();
        System.Random rndgen = new System.Random();
        w = net.GetNetworkWeights();
        for (byte i = 0; i < newNet.n_input; i++)
        {
            if (rndgen.NextDouble() <= mutationRate)
            {
                w[i] = Math.Round(rndgen.NextDouble());
            }
        }
        newNet.SetNetworkWeights(w);
        return newNet;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    MLP CrossOver(MLP a, MLP b)
    {
        MLP newNet = a;
        System.Random rndgen = new System.Random();

        List<double> w1 = new List<double>();
        List<double> w2 = new List<double>();
        w1 = newNet.GetNetworkWeights();
        w2 = b.GetNetworkWeights();

        for (byte i = 0; i < a.GetNetworkWeights().Count; i++)
        {
            if (rndgen.NextDouble() <= crossoverRate)
            {
                w1[i] = w2[i];
            }
        }
        newNet.SetNetworkWeights(w1);
        return newNet;
    }

    /// <summary>
    /// Return the individual at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GameAgent GetAgent(int index)
    {
        return m_agents[index];
    }

    int TournamentSelect()
    {
        Population tournament = new Population(size, true);
        System.Random rndgen = new System.Random();
        for (int i = 0; i < tournamentSize; i++)
        {
            int randomID = rndgen.Next() % size;
            tournament.m_agents[i] = m_agents[i];
        }
        int fittest = tournament.GetFittest();
        return fittest;
    }

    /// <summary>
    /// Gets the fittest gene in the population
    /// </summary>
    /// <returns></returns>
    public int GetFittest()
    {
        int fittest = m_agents[0].GetFitness();
        int index = 0;

        for (int i = 0; i < Size(); i++)
        {
            if (m_agents[i].GetFitness() > fittest)
            {
                fittest = m_agents[i].GetFitness();
                index = i;
            }
        }
        return index;
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
        m_agents[index] = a;
    }
}