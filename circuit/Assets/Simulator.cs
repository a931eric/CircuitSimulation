using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator:MonoBehaviour{
    public List<WireNet> wireNets=new List<WireNet>();
    public float t,deltaT=0.001f,timePerFrame=1;
    void Update()
    {
        for (int i = 0; i < timePerFrame/deltaT; i++)
        {
            
            foreach (WireNet c in Object.FindObjectsOfType<WireNet>())
            {
                c.Simulate(t, deltaT);
            }
            
            foreach (Resistance c in Object.FindObjectsOfType<Resistance>())
            {
                c.Simulate(t, deltaT);
            }
            foreach (Capacitance c in Object.FindObjectsOfType<Capacitance>())
            {
                c.Simulate(t, deltaT);
            }
            foreach (Inductor c in Object.FindObjectsOfType<Inductor>())
            {
                c.Simulate(t, deltaT);
            }
            foreach (Power c in Object.FindObjectsOfType<Power>())
            {
                c.Simulate(t, deltaT);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
            {
                foreach (WireNet c in Object.FindObjectsOfType<WireNet>())
                {
                    c.Reset();
                }
            }
            t += deltaT;
        }
    }
}
