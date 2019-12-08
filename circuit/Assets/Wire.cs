using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : electronicComponent
{
    public WireNet wireNet;
    public Vector3 centerPos;//給伏特計用的
    public override void select()
    {
        selectMark = Instantiate(selectMarkPrefab, gameObject.transform);
        selectMark.transform.position = transform.position;
        selectMark.transform.Translate(-0.5f * transform.localScale.x, 0, -0.63f);
    }
    
    public override void create(Main main)
    {
        connect = new electronicComponent[2];
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
        wireNet.wires.Remove(this);
        DestroyImmediate(gameObject);
    }

    public override void Update()
    {
        foreach (electronicComponent c in connect)//wire must be connected both sides
        {
            if (c == null) {delete();return;}
        }
        centerPos = (connect[0].gameObject.transform.position + connect[1].gameObject.transform.position) / 2;
        transform.position = connect[0].gameObject.transform.position;
        transform.LookAt(connect[1].gameObject.transform.position);
        transform.Rotate(0, 90, 0);
        transform.localScale=new Vector3(Vector3.Distance(connect[0].gameObject.transform.position, connect[1].gameObject.transform.position),0.2f,0.2f);
    }


}
