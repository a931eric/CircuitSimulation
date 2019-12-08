using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistance : electronicComponent {
    #region calc
    public double r = 1;
    public override void Simulate(float t, float deltaT)
    {
        if (connect[0] == null || connect[1] == null) { i = 0; return; }
        WireNet w1 = ((Wire)connect[0]).wireNet, w2 = ((Wire)connect[1]).wireNet;
        
        double c1 = w1.c, c2 = w2.c;
        double q1 = w1.q, q2 = w2.q;
        double deltaQ = (q1/c1-q2/c2)/r*deltaT;
        w1.nextQ -= deltaQ;
        w2.nextQ += deltaQ;
        i = deltaQ / deltaT;
    }
    #endregion

}
