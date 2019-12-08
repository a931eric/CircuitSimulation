using System.Collections.Generic;
using UnityEngine;

public class WireNet:MonoBehaviour{
    public List<Wire> wires=new List<Wire>();
    public List<Intersection> intersections = new List<Intersection>();
    public void Update()
    {
        int count=0;
        foreach(Wire w in wires)
        {
            if (w != null) { count++; break; }
        }
        foreach (Intersection w in intersections)
        {
            if (w != null) { count++; break; }
        }
        if (count == 0) { Data.Remove(this); Destroy(gameObject); }
    }


    #region calc
    public double c = 0.01;
    public double q = 0,nextQ=0;
    public double v { get { return q / c; }set { q = c * value; } }
    public void Simulate(float t,float deltaT)
    {
        q = nextQ;
    }
    public void Reset()
    {
        q = 0;
        nextQ = 0;
    }
    #endregion
}
