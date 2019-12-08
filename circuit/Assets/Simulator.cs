using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator:MonoBehaviour{
    public float t,deltaT=0.001f,timePerFrame=1;
    void Update()
    {
        for (long i = 0; i < timePerFrame/deltaT; i++)
        {
            
            foreach (WireNet c in Data.wireNet)
            {
                c.Simulate(t, deltaT);
            }
            foreach (Resistance c in Data.resistance)
            {
                c.Simulate(t, deltaT);
            }
            foreach (Capacitance c in Data.capacitance)
            {
                c.Simulate(t, deltaT);
            }
            foreach (Inductor c in Data.inductor)
            {
                c.Simulate(t, deltaT);
            }
            foreach (Power c in Data.power)
            {
                c.Simulate(t, deltaT);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
            {
                foreach (WireNet c in Data.wireNet)
                {
                    c.Reset();
                }
            }
            t += deltaT;
        }
    }
}
