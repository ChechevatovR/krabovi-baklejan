using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoiseGeneration : MonoBehaviour
{
    private PseudoRandom rng;
    public float scale = 1; // Размер ячейки в метрах

    void Start()
    {
        rng = GetComponent<PseudoRandom>();
    }

    Vector2 RandomVector2(float x, float y)
    {
        float val = rng.Get(x, y) * 671468435;
        return new Vector2((float) Math.Cos(val), (float) Math.Sin(val));
    }

    float Interpolate(float a0, float a1, float w)
    {
        return (float) (a1 - a0) * w + a0;
    }

    public float GetNoiseValue(float x, float y)
    {
        float x0 = (int)(x / scale) * scale;
        float x1 = x0 + 1;
        float y0 = (int)(y / scale) * scale;
        float y1 = y0 + 1;
        
        Vector2 d1 = new Vector2(x - x0, y - y0);
        Vector2 d2 = new Vector2(x - x1, y - y0);
        Vector2 d3 = new Vector2(x - x0, y - y1);
        Vector2 d4 = new Vector2(x - x1, y - y1);

        return Interpolate(
            Interpolate(Vector2.Dot(d1, RandomVector2(x0, y0)), Vector2.Dot(d2, RandomVector2(x1, y0)),x - x0),
            Interpolate(Vector2.Dot(d3, RandomVector2(x0, y1)), Vector2.Dot(d4, RandomVector2(x1, y1)),x - x0),
            y - y0);
    }

    public float GetStupidity(float x, float y)
    {
        //return x * y;
        return (float) Math.Pow(Math.Sin((x + y) * Math.PI) / 2, 2);
        //return (float) 0;
        //return (float) (Math.Sin(x) * Math.Cos(y));
    }
}