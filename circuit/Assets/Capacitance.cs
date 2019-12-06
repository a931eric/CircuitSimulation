using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capacitance : electronicComponent
{
    #region calc
    public double c = 0.01,q=0,i;
    public override void Simulate(float t, float deltaT)
    {
        if (connect[0] == null || connect[1] == null) return;
        WireNet w1 = ((Wire)connect[0]).wireNet, w2 = ((Wire)connect[1]).wireNet;

        double c1 = w1.c, c2 = w2.c;
        double q1 = w1.q, q2 = w2.q;
        double deltaQ = (-c * c1 * q2 + c * c2 * q1 - c1 * c2 * q) / (c * c1 + c * c2 + c1 * c2);
        q += deltaQ;
        w1.nextQ -= deltaQ;
        w2.nextQ += deltaQ;
        i = deltaQ / deltaT;
    }
    public void Reset()
    {
        q = 0;
    }
    #endregion

}
