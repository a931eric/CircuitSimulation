using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electronicComponent : MonoBehaviour {
    Wire[] wires;
    public electronicComponent[] connect;
    
    public GameObject selectMarkPrefab, selectMark;
    public virtual void select()
    {
        selectMark = Instantiate(selectMarkPrefab,gameObject.transform);
        selectMark.transform.position = transform.position;
        selectMark.transform.Translate(0,0, -0.63f);
    }

    public virtual void unselect()
    {
        Destroy(selectMark);
    }
    public virtual void move(Vector3 movement)
    {
        transform.Translate(movement);
    }
    public virtual void create()
    {
        connect = new electronicComponent[2];
        for (int i = 0; i < connect.Length; i++) connect[i] = null;
    }
    public virtual void delete()
    {
        foreach(electronicComponent c in connect)
        {
            if (c != null)
            {
                for (int i = 0; i < c.connect.Length; i++)
                {
                    if (c.connect[i] == this) c.connect[i] = null;
                }
            }
        }
        Destroy(gameObject);
        this.enabled = false;
    }
    public virtual void addConnection(electronicComponent c)
    {
        for(int i = 0; i < connect.Length; i++)
        {
            if (connect[i] == null) { connect[i] = c; return; }
        }
    }
    public int n_emptyConnect()
    {
        int r = 0;
        for (int i = 0; i < connect.Length; i++) {
            if (connect[i] == null) r++;
        }
        return r;
    }
    public virtual void Simulate(float t,float deltaT)
    {
    }

}
