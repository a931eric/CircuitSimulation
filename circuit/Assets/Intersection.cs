using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : electronicComponent {
    public WireNet wireNet;
    public override void create()
    {
        connect = new electronicComponent[16];
        for (int i = 0; i < connect.Length; i++) connect[i] = null;
        
    }
    public override void delete()
    {
        foreach (electronicComponent c in connect)
        {
            if (c != null)
            {
                for (int i = 0; i < c.connect.Length; i++)
                {
                    if (c.connect[i] == this) c.connect[i] = null;
                }
            }
        }
        wireNet.intersections.Remove(this);
        Destroy(gameObject);
        this.enabled = false;
    }
}
