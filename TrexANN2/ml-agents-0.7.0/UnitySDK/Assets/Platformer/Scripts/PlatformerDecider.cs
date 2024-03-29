﻿using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlatformerDecider : Decision {

    private float increment = 2f;

    public override float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        if (brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            List<float> act = new List<float>();

            //Jump threshold
            act.Add(vectorObs[0] * increment);
            //Jump value
            act.Add(vectorObs[1] * increment);
        
            //Return array
            return act.ToArray();
        }
        return new float[1] { 1f };
    }

    public override List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        return new List<float>();
    }
}