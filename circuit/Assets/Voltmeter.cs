using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voltmeter : Instrument
{
    public WireNet c1, c2;
    public Wire w1, w2;
    public override void create_child_class(electronicComponent[] args)
    {
        w1 =(Wire)args[0]; w2 = (Wire)args[1];
        c1 = w1.wireNet; c2 = w2.wireNet;
        transform.position = (w1.centerPos + w2.centerPos) / 2 + new Vector3(0, 2, 0);
    }
    public override double GetSample()
    {
        return c1.v-c2.v;
    }
    public override void Update_child_class()
    {
        if (w1 == null || w2 == null)//main.rearrangeWireNet會影響到wireNet但不影響wire
        {
            Destroy(gameObject);
            return;
        }
        else if (c1 == null || c2 == null)
        {
            c1 = w1.wireNet; c2 = w2.wireNet;
        }

        Vector3[] p = { w1.centerPos, transform.position - new Vector3(0, 0.5f, 0), w2.centerPos };
        line.SetPositions(p);
    }
}
