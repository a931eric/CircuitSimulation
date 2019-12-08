using UnityEngine;

public class Voltmeter : Instrument
{
    public WireNet c1, c2;
    public Wire w1, w2;
    public void create_(Main main, Wire target1, Wire target2)
    {
        
        w1 =target1; w2 = target2;
        c1 = w1.wireNet; c2 = w2.wireNet;
        transform.position = (w1.centerPos + w2.centerPos) / 2 + new Vector3(0, 2, 0);
        base.create(main);
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
        if (Mathf.Abs((float)GetSample()) < 0.00001)
            text.text = (GetSample() * 1000000).ToString("+0.00;-0.00") + "μV";
        if (Mathf.Abs((float) GetSample())<0.01)
            text.text = (GetSample()*1000).ToString("+0.00;-0.00") + "mV";
        else
            text.text = GetSample().ToString("+0.00;-0.00") + "V";
        Vector3[] p = { w1.centerPos, transform.position - new Vector3(0, 0.5f, 0), w2.centerPos };
        line.SetPositions(p);
    }
}
