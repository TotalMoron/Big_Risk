using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSetter : MonoBehaviour
{
    public Texture2D heightMap;
    public float dampener, maxHeight, waterLevel;
    Terrain terr; // terrain to modify
    int hmWidth; // heightmap width
    int hmHeight; // heightmap height
    void Start()
    {
        terr = Terrain.activeTerrain;
        hmWidth = terr.terrainData.heightmapResolution;
        hmHeight = terr.terrainData.heightmapResolution;
        Terrain.activeTerrain.heightmapMaximumLOD = 0;

        terr.terrainData.size = new Vector3(heightMap.width, maxHeight, heightMap.height);

        // get the heights of the terrain under this game object
        float[,] heights = terr.terrainData.GetHeights(0, 0, hmWidth, hmHeight);
        bool[,] holes = terr.terrainData.GetHoles(0, 0, hmWidth-1, hmHeight-1);
        // we set each sample of the terrain in the size to the desired height
        for (int i = 0; i < hmWidth; i++)
        {
            for (int j = 0; j < hmHeight; j++)
            {
                float pixHeight = heightMap.GetPixel(j, i).grayscale / dampener;
                heights[i, j] = pixHeight;


                if ((i > hmHeight/2 || pixHeight < waterLevel) && (j < hmHeight - 1 && i < hmWidth - 1))
                {
                    holes[i, j] = false;
                } else if((j < hmHeight - 1 && i < hmWidth - 1))
                {
                    holes[i, j] = true;
                }
            }
        }
        // set the new height
        terr.terrainData.SetHeights(0, 0, heights);
        terr.terrainData.SetHoles(0, 0, holes);
    }
}
