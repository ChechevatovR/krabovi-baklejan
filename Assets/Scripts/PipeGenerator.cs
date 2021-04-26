using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

enum Dir
{
    TOP, // +Y
    BOTTOM, // -Y
    FRONT, // +X
    BACK, // -X
    RIGHT, // -Z
    LEFT // +z
}

public class PipeGenerator : MonoBehaviour
{
    public GameObject PipeRoot;
    public GameObject spheresParent;

    public GameObject PipeStraight; // Default: TOP-BOTTOM
    public GameObject PipeStraightBroken;
    public GameObject PipeCurved; // Default: TOP-RIGHT
    public GameObject PipeCurvedBroken;
    
    public GameObject minimapSphere;

    public Color[] colors;
    public GameObject[] minimapSpheres;
    private int sphereIndex = 0;

    public float proba; // Probability of generating a broken pipe
    private float deltaHeight = 1f;
    private bool[,,] f;
    private bool makeSphere = false;
    //comment

    private Dictionary<Dir, Vector3> dirs = new Dictionary<Dir, Vector3>()
    {
        {Dir.TOP, new Vector3(0, 1, 0)},
        {Dir.BOTTOM, new Vector3(0, -1, 0)},
        {Dir.RIGHT, new Vector3(0, 0, -1)},
        {Dir.LEFT, new Vector3(0, 0, 1)},
        {Dir.FRONT, new Vector3(1, 0, 0)},
        {Dir.BACK, new Vector3(-1, 0, 0)}
    };
    
    private Dictionary<Dir, Dir> opp = new Dictionary<Dir, Dir>()
    {
        {Dir.TOP, Dir.BOTTOM},
        {Dir.BOTTOM, Dir.TOP},
        {Dir.BACK, Dir.FRONT},
        {Dir.FRONT, Dir.BACK},
        {Dir.LEFT, Dir.RIGHT},
        {Dir.RIGHT, Dir.LEFT},
    };

    void Start()
    {
        f = new bool[10, 10, 10];
        int x = Random.Range(0, 11);
        int z = Random.Range(0, 11);
        Color col = colors[Random.Range(0, 3)];

        /*
        PlacePipe(0, 2, 0, Dir.BOTTOM, Dir.TOP, col);
        PlacePipe(0, 2, 1, Dir.FRONT, Dir.BACK, col);
        PlacePipe(0, 2, 2, Dir.LEFT, Dir.RIGHT, col);

        PlacePipe(4, 4, 0, Dir.TOP, Dir.FRONT, col);
        PlacePipe(4, 4, 1, Dir.TOP, Dir.RIGHT, col);
        PlacePipe(4, 4, 2, Dir.TOP, Dir.LEFT, col);
        PlacePipe(4, 4, 3, Dir.TOP, Dir.BACK, col);

        PlacePipe(4, 5, 0, Dir.BOTTOM, Dir.FRONT, col);
        PlacePipe(4, 5, 1, Dir.BOTTOM, Dir.RIGHT, col);
        PlacePipe(4, 5, 2, Dir.BOTTOM, Dir.LEFT, col);
        PlacePipe(4, 5, 3, Dir.BOTTOM, Dir.BACK, col);

        PlacePipe(3, 4, 0, Dir.LEFT, Dir.BACK, col);
        PlacePipe(2, 4, 0, Dir.LEFT, Dir.FRONT, col);
        PlacePipe(2, 4, 1, Dir.RIGHT, Dir.FRONT, col);
        PlacePipe(3, 4, 1, Dir.RIGHT, Dir.BACK, col);
        //Generate(x, y, 1, Dir.BOTTOM, mat);
        */
        Generate(new Vector3(5, 0, 5), Dir.BOTTOM, col, 0);
    }

    void PlacePipe(Vector3 pos, Dir d1, Dir d2, Color col)
    {
        f[(int) pos.x, (int) pos.y, (int) pos.z] = true;
        var v = GetRotationAndPrefabFromDirections(d1, d2);
        GameObject pipe = Instantiate(v.Item1,
            new Vector3(pos.x + .5f, pos.y + .5f, pos.z + .5f),
            v.Item2,
            PipeRoot.transform
        );
        pipe.GetComponent<MeshRenderer>().materials[1].color = col;
        pipe.transform.localScale = Vector3.one;
        
        if (makeSphere)
        {
            GameObject sphere = Instantiate(minimapSphere, new Vector3(pos.x + .5f, pos.y + .5f, pos.z + .5f),
                Quaternion.identity);
            sphere.transform.parent = spheresParent.transform;
            sphere.transform.Rotate(90, 0, 0);
            //sphere.GetComponent<MeshRenderer>().materials[1].color = col;
            makeSphere = false;
            //minimapSpheres[sphereIndex] = sphere;
            sphereIndex++;
        }
    }

    Tuple<GameObject, Quaternion> GetRotationAndPrefabFromDirections(Dir d1, Dir d2)
    {
        GameObject instancerStraight = PipeStraight;
        GameObject instancerCurved = PipeCurved;
        if (Random.value <= proba)
        {
            instancerStraight = PipeStraightBroken;
            instancerCurved = PipeCurvedBroken;
            makeSphere = true;
        }
        else makeSphere = false;

        // ПРЯМЫЕ ТРУБЫ
        if ((d1 == Dir.TOP && d2 == Dir.BOTTOM) || (d1 == Dir.BOTTOM && d2 == Dir.TOP))
            return Tuple.Create(instancerStraight, Quaternion.Euler(90, 0, 0));

        if ((d1 == Dir.RIGHT && d2 == Dir.LEFT) || (d1 == Dir.LEFT && d2 == Dir.RIGHT))
            return Tuple.Create(instancerStraight, Quaternion.Euler(0, 0, 0));

        if ((d1 == Dir.FRONT && d2 == Dir.BACK) || (d1 == Dir.BACK && d2 == Dir.FRONT))
            return Tuple.Create(instancerStraight, Quaternion.Euler(0, 90, 0));

        // ТРУБЫ С ПОВОРОТОМ
        if ((d1 == Dir.TOP && d2 == Dir.BACK) || (d1 == Dir.BACK && d2 == Dir.TOP))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 90, 0));

        if ((d1 == Dir.TOP && d2 == Dir.RIGHT) || (d1 == Dir.RIGHT && d2 == Dir.TOP))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 0, 0));


        if ((d1 == Dir.TOP && d2 == Dir.FRONT) || (d1 == Dir.FRONT && d2 == Dir.TOP))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, -90, 0));

        if ((d1 == Dir.TOP && d2 == Dir.LEFT) || (d1 == Dir.LEFT && d2 == Dir.TOP))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 180, 0));

        if ((d1 == Dir.BOTTOM && d2 == Dir.BACK) || (d1 == Dir.BACK && d2 == Dir.BOTTOM))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 90, 180));

        if ((d1 == Dir.BOTTOM && d2 == Dir.RIGHT) || (d1 == Dir.RIGHT && d2 == Dir.BOTTOM))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 0, 180));

        if ((d1 == Dir.BOTTOM && d2 == Dir.FRONT) || (d1 == Dir.FRONT && d2 == Dir.BOTTOM))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, -90, 180));

        if ((d1 == Dir.BOTTOM && d2 == Dir.LEFT) || (d1 == Dir.LEFT && d2 == Dir.BOTTOM))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 180, 180));

        if ((d1 == Dir.RIGHT && d2 == Dir.BACK) || (d1 == Dir.BACK && d2 == Dir.RIGHT))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 90, -90));

        if ((d1 == Dir.LEFT && d2 == Dir.BACK) || (d1 == Dir.BACK && d2 == Dir.LEFT))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, 180, -90));

        if ((d1 == Dir.FRONT && d2 == Dir.RIGHT) || (d1 == Dir.RIGHT && d2 == Dir.FRONT))
            return Tuple.Create(instancerCurved, Quaternion.Euler(0, -90, 90));

        if ((d1 == Dir.FRONT && d2 == Dir.LEFT) || (d1 == Dir.LEFT && d2 == Dir.FRONT))
            return Tuple.Create(instancerCurved, Quaternion.Euler(180, 90, 90));

        Debug.Assert(false);
        return null;
    }

    void Generate(Vector3 pos, Dir prev, Color col, int l)
    {
        List<Dir> able = new List<Dir>();
        foreach (Dir dir in Dir.GetValues(typeof(Dir)))
        {
            if (dir == prev) continue;
            Vector3 p = pos + dirs[dir];
            if (p.x < 10 && p.y < 10 && p.z < 10 && p.x >= 0 && p.y >= 0 && p.z >= 0 && !f[(int) p.x, (int) p.y, (int) p.z])
            {
                able.Add(dir);
            }
        }
        if (able.Count == 0) return;

        Dir go = able[Random.Range(0, able.Count)];
        if (l >= 10 && able.Contains(Dir.BOTTOM)) go = Dir.BOTTOM;
        PlacePipe(pos, prev, go, col);
        Generate(pos + dirs[go], opp[go], col, l + 1);
    }
    
}