using System;
using System.Collections.Generic;
using UnityEngine;

public class awesomeTree : MonoBehaviour
{
    public GameObject[] tree;
    public Vector3[] questZones; // x = x; y = radius; z = z
    private Terrain terrain;
    private float x;
    private float z;
    private int weight;
    private int length;
    public int maxDist = 4;
    public int minDist = 3;
    public float maxTreeScale = 2;
    public float minTreeScale = 1;

    float[,] gTerrain()
    {
        System.Random rn = new System.Random();
        float[,] result = new float[weight, length];
        for (int x = 0; x < weight; x++)
        {
            for (int z = 0; z < length; z++)
            {
                result[x, z] = rn.Next(0, 30);
            }
        }
        return result;
    }

    bool isQZones(Vector2 coord)
    {
        bool result = false;
        for(int i = 0; i < questZones.Length; i++)
        {
            float r = questZones[i].y;
            if(questZones[i].x + r > coord.x && questZones[i].x - r < coord.x
            && questZones[i].z + r > coord.y && questZones[i].z - r < coord.y)
            {
                result = true;
            }
        }
        return result;
    }

    void gTree(float[,] ter, float xTer, float zTer)
    {
        terrain = GetComponent<Terrain>();
        System.Random rn = new System.Random();
        for (int x = 0; x < weight; x += rn.Next(minDist, maxDist))
        {
            for (int z = 0; z < length; z += rn.Next(minDist, maxDist))
            {
                Vector2 coordPoint = new Vector2(x+xTer,z+zTer);
                if (!isQZones(coordPoint))
                {
                    Vector3 position = new Vector3(x+xTer, ter[x, z], z+zTer);
                    int maxTS = (int)maxTreeScale * 100;
                    int minTS = (int)minTreeScale * 100;
                    float totalScale = rn.Next(minTS, maxTS) * 0.01f;
                    tree[0].transform.localScale = new Vector3(totalScale, totalScale, totalScale);
                    Instantiate(tree[0], position, Quaternion.identity);
                }
            }
        }
    }

    /*Не забивайте себе голову этим методом он в бетта версии*/
    void gTree(Terrain terrai, float xTer, float zTer)
    {
        terrain = GetComponent<Terrain>();
        System.Random rn = new System.Random();
        for (int x = 0; x < weight; x += rn.Next(3, 9))
        {
            for (int z = 0; z < length; z += rn.Next(4, 8))
            {
                var tree = new TreeInstance
                {
                    position = new Vector3(0.5f, 0, 0),
                    prototypeIndex = 0,
                    widthScale = 6f,
                    heightScale = 2f,
                    color = Color.white,
                    lightmapColor = Color.white
                };
                terrain.AddTreeInstance(tree);
                terrain.Flush();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        
        x = terrain.GetPosition().x;
        z = terrain.GetPosition().z;
        weight = (int)terrain.terrainData.size.x;
        length = (int)terrain.terrainData.size.z;
        float[,] terrainIvan = gTerrain();

        // Работает только тогда когда в массиве деревьев есть хотя бы одно дерево
        if (tree.Length > 0)
        {
            Vector3 position = new Vector3(x, 0, z);                     // Крайняя начальная точка ---- это нужно гашану

            Vector3 positionEnd = new Vector3(weight + x, 0, length + z);    // Крайняя конечная точка ---- это нужно гашану
            
            gTree(terrainIvan,x,z);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
