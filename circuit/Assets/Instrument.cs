using UnityEngine;

public class Instrument : electronicComponent
{
    public TextMesh text;
    public LineRenderer line, wave;
    public bool drawWave = true;
    public float[] samples = new float[100];
    public int sampleIdx = 0;
    public float xs = 0.01f, maxY = 0.5f;
    public virtual double GetSample()
    {
        return 0;
    }
    public override void Update()
    {
        base.Update();
        samples[sampleIdx] = (float)(GetSample());
        sampleIdx = (sampleIdx + 1) % samples.Length;
        float max = Mathf.Max(Mathf.Max(Mathf.Max(samples), -Mathf.Min(samples)));
        float ys = max == 0 ? 1 : maxY / max;
        if (drawWave)
        {
            for (int i = 0; i < samples.Length; i++)
            {
                wave.positionCount = samples.Length;
                wave.SetPosition(i, new Vector3(i * xs, samples[(i + sampleIdx) % samples.Length] * ys, 0));
            }
        }
        Update_child_class();
    }
    public virtual void Update_child_class() { }
}
