using UnityEngine;

public class Inductor : electronicComponent
{
    #region calc
    public double l;
    public override void Simulate(float t, float deltaT)
    {
        if (connect[0] == null || connect[1] == null) { i = 0; return; }
        WireNet w1 = ((Wire)connect[0]).wireNet, w2 = ((Wire)connect[1]).wireNet;
        double c1 = w1.c, c2 = w2.c;
        double q1 = w1.q, q2 = w2.q;
        i += ( q1/c1 - q2/c2) * deltaT / l;
        double deltaQ = i* deltaT;
        w1.nextQ -= deltaQ;
        w2.nextQ += deltaQ;
    }
    #endregion
    public Light light_;
    public float intensity = 1;
    public override void Start()
    {
        light_ = GetComponentInChildren<Light>();
    }
    public override void Update()
    {
        base.Update();
        light_.intensity = 0.1f+intensity * Mathf.Abs((float)i);
        GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", (intensity * Mathf.Abs((float)i) + 0.1f) * light_.color);
    }
}
