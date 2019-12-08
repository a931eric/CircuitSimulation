using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammeter : Instrument
{
    public electronicComponent target;
    public override void create_child_class(electronicComponent[] args)
    {
        target = args[0];
        transform.position =target.transform.position + new Vector3(0, 2, 0);
    }
    public override double GetSample()
    {
        return target.i;
    }
    public override void Update_child_class()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        text.text = GetSample().ToString("0.00") + "A";
        Vector3[] p = { target.transform.position, transform.position - new Vector3(0, 0.5f, 0) };
        line.SetPositions(p);
    }
}
