using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : electronicComponent
{
    #region calc
    public double i;
    public double amp = 1,freq=1;
    public AnimationCurve curve;
    public double v(float t)
    {
        return amp*curve.Evaluate((float)(t*freq)%1);
        //return amp*(Mathf.Sin((float)(t * freq)) + Mathf.Sin((float)(t * freq * 5)) + Mathf.Sin((float)(t * freq * 10)));
    }
    //v: w1->w2
    public override void Simulate(float t, float deltaT)
    {

        if (connect[0] == null || connect[1] == null) return;
        WireNet w1= ((Wire)connect[0]).wireNet, w2= ((Wire)connect[1]).wireNet;
        double c1 = w1.c, c2 = w2.c;
        double q1 = w1.q, q2 = w2.q;
        
        double deltaQ = (c1 * c2 * v(t) + c2 * q1 - c1 * q2) / (c1 + c2);
        w1.nextQ -= deltaQ;
        w2.nextQ += deltaQ;
        i = deltaQ / deltaT;
        /*
        w1.nextQ = v(t)*c1;
        w2.nextQ = 0;*/
    }


    #endregion
}
