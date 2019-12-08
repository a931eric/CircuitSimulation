using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electronicComponent : MonoBehaviour {
 
    public electronicComponent[] connect;
    public double i ;
    public GameObject selectMarkPrefab, selectMark;
    public Vector3 targetPosition;
    public float snap;
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
    public virtual void move(Vector3 movement,float snap)
    {
        targetPosition  += movement;
        this.snap = snap;
    }
    public virtual void Start() { }
    public virtual void Update()
    {
        if (snap == 0)
        {
            transform.position = (targetPosition);
        }
        else
        {
            transform.position = new Vector3(Mathf.RoundToInt((targetPosition.x) / snap), transform.position.y, Mathf.RoundToInt((targetPosition.z) / snap)) * snap;
        }
    }
    public virtual void OnMouseRelease()
    {
        targetPosition = transform.position;
    }
    public virtual void create(Main main)
    {
        connect = new electronicComponent[2];
        for (int i = 0; i < connect.Length; i++) connect[i] = null;
        targetPosition = transform.position;
        snap = main.snapGap;
        Update();
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
