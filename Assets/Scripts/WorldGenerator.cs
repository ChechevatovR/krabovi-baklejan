using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private ChunkGenerator ch;
    private List<Vector2> loadedChunks = new List<Vector2>();
    private Dictionary<Vector2, GameObject> gos = new Dictionary<Vector2, GameObject>();
    public GameObject Player;
    public int loadDistance;

    void Start()
    {
        ch = GetComponent<ChunkGenerator>();
    }
    Vector2 GetPlayerChunk()
    {
        return new Vector2((int) (Player.transform.position.z / 10), (int) (Player.transform.position.x / 10));
    }
    List<Vector2> around()
    {
        Vector2 c = GetPlayerChunk();
        List<Vector2> ret = new List<Vector2>();
        for (int x = (int) c.x - loadDistance; x <= c.x + loadDistance; x++) {
            for (int z = (int) c.y - loadDistance; z <= c.y + loadDistance; z++) {
                ret.Add(new Vector2(x, z));
            }
        }
        return ret;
    }

    void FixedUpdate()
    {
        List<Vector2> req = around();
        List<Vector2> toLoad = new List<Vector2>();
        List<Vector2> toUnoad = new List<Vector2>();
        foreach (Vector2 chunk in req)
        {
            if (!loadedChunks.Contains(chunk)) toLoad.Add(chunk);
        }
        
        foreach (Vector2 chunk in loadedChunks)
        {
            if (!req.Contains(chunk)) toUnoad.Add(chunk);
        }

        foreach (Vector2 chunk in toUnoad)
        {
            Destroy(gos[chunk]);
            loadedChunks.Remove(chunk);
            gos.Remove(chunk);
        }

        foreach (Vector2 chunk in toLoad)
        {
            gos[chunk] = ch.GenerateChunk(chunk.x, chunk.y);
            loadedChunks.Add(chunk);
        }
        
        toLoad.Clear();
        toUnoad.Clear();
    }
}
