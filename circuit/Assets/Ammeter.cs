using UnityEngine;

public class Ammeter : Instrument
{
    public electronicComponent target;
    public void create_(Main main, electronicComponent target)
    {
        this.target = target;
        transform.position =target.transform.position + new Vector3(0, 2, 0);
        base.create(main);
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
        if (Mathf.Abs((float)GetSample()) < 0.00001)
            text.text = (GetSample() * 1000000).ToString("+0.00;-0.00") + "μA";
        else if (Mathf.Abs((float)GetSample()) < 0.01)
            text.text = (GetSample() * 1000).ToString("+0.00;-0.00") + "mA";
        else
            text.text = GetSample().ToString("+0.00;-0.00") + "A";
        Vector3[] p = { target.transform.position, transform.position - new Vector3(0, 0.5f, 0) };
        line.SetPositions(p);
    }
}
