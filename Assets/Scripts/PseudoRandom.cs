using System;
using UnityEngine;

public class PseudoRandom : MonoBehaviour
{
    private float k1, k2, k3, k4, k5, k6;

    public bool randomizeSeed = true;
    public int seed = 424242424;

    public void Start()
    {
        k1 = seed % 164627;
        k2 = seed % 100559;
        k3 = seed % 135719;
        k4 = seed % 111949;
        k5 = seed % 142867;
        k6 = seed % 86311;
    }

    public float Get(float x, float y)
    {
        return (float) (Math.Sin(k1 * x + k2 * y + k3) * Math.Cos(k4 * x + k5 * y + k6));
    }
}
