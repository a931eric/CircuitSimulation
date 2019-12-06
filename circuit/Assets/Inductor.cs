using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inductor : electronicComponent
{
    #region calc
    public double l,i=0;
    public override void Simulate(float t, float deltaT)
    {
        if (connect[0] == null || connect[1] == null) return;
        WireNet w1 = ((Wire)connect[0]).wireNet, w2 = ((Wire)connect[1]).wireNet;
        double c1 = w1.c, c2 = w2.c;
        double q1 = w1.q, q2 = w2.q;
        i += ( q1/c1 - q2/c2) / l;
        double deltaQ = i* deltaT;
        w1.nextQ -= deltaQ;
        w2.nextQ += deltaQ;
    }
    #endregion
}
