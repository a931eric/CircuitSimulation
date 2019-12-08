using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Data
{
    public static List<WireNet> wireNet = new List<WireNet>();
    public static List<Resistance> resistance=new List<Resistance>();
    public static List<Capacitance> capacitance=new List<Capacitance>();
    public static List<Inductor> inductor=new List<Inductor>();
    public static List<Power> power=new List<Power>();
    public static void Add(Object c)
    {
        if (c.GetType().Equals(typeof(WireNet)))
        {
            wireNet.Add((WireNet)c);
        }
        if (c.GetType().Equals(typeof(Resistance)))
        {
            resistance.Add((Resistance)c);
        }
        if (c.GetType().Equals(typeof(Capacitance)))
        {
            capacitance.Add((Capacitance)c);
        }
        if (c.GetType().Equals(typeof(Inductor)))
        {
            inductor.Add((Inductor)c);
        }
        if (c.GetType().Equals(typeof(Power)))
        {
            power.Add((Power)c);
        }
    }
    public static void Remove(Object c)
    {
        if (c.GetType().Equals(typeof(WireNet)))
        {
            wireNet.Remove((WireNet)c);
        }
        if (c.GetType().Equals(typeof(Resistance)))
        {
            resistance.Remove((Resistance)c);
        }
        if (c.GetType().Equals(typeof(Capacitance)))
        {
            capacitance.Remove((Capacitance)c);
        }
        if (c.GetType().Equals(typeof(Inductor)))
        {
            inductor.Remove((Inductor)c);
        }
        if (c.GetType().Equals(typeof(Power)))
        {
            power.Remove((Power)c);
        }
    }
}
