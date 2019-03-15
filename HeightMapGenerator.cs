using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var terrain = GetComponent<Terrain>();
        terrain.terrainData.size = new Vector3(128, 50, 128);
        terrain.terrainData.heightmapResolution = 128;

        HabrHeightMap hm = new HabrHeightMap();
        hm.GenerateHeightMap(); //генерируем карту высот
        terrain.terrainData.SetHeights(0, 0, hm.HMArray); //применяем карту высот к terrain
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class HabrHeightMap
{
    //размеры карты (terrain)
    public int mapSizex = 128;
    public int mapSizey = 128;

    public int genStep = 1024; //количество прямоугольников
    public float zScale = 512; // коэффициент высоты карты

    //размеры прямоугольника
    public int recSizex = 10;
    public int recSizey = 10;

    public float[,] HMArray; //двумерный массив = карта высот

    public void GenerateHeightMap()
    {
        HMArray = new float[mapSizex, mapSizey];
        for (int i = 0; i < genStep; i++)
        {
            int x1 = Random.Range(0, 123456789) % mapSizex;
            int y1 = Random.Range(0, 123456789) % mapSizey;
            int x2 = x1 + recSizex / 4 + Random.Range(0, 123456789) % recSizex;
            int y2 = y1 + recSizey / 4 + Random.Range(0, 123456789) % recSizey;
            if (y2 > mapSizey) y2 = mapSizey;
            if (x2 > mapSizex) x2 = mapSizex;
            for (int j = x1; j < x2; j++)
                for (int k = y1; k < y2; k++)
                    HMArray[j,k] += ((zScale) / (genStep) + Random.Range(0, 123456789) % 50 / 50.0f)*0.006f; 
                    //последний коэффициент 0.006f добавил от себя, чтобы по высоте выглядело нормально
        }
    }
}