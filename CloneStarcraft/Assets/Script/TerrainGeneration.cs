using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

    public int NbPasses;
    private float Ecart;
    
    float brownianNoise(float number)
    {
        return (1f / ( (Ecart == 0 ? 1 : Ecart) * Mathf.Sqrt(2 * Mathf.PI)) ) * Mathf.Exp(-Mathf.Pow(number - Mathf.Pow(Ecart, 2), 2) / (Ecart == 0 ? 1 : Ecart));
    }

    // Use this for initialization
    void Start () {
        TerrainData datas = ((TerrainCollider) gameObject.GetComponent("TerrainCollider")).terrainData;
        int nbDivision = 1;
        Ecart = 0;
        float[,] maps = datas.GetHeights(0, 0, datas.heightmapWidth, datas.heightmapHeight);

        for (int i = 0; i < NbPasses; ++i)
        {

            int unitX = datas.heightmapWidth / ((nbDivision == 1) ? 2 : nbDivision);
            int unitY = datas.heightmapHeight / ((nbDivision == 1) ? 2 : nbDivision);

            for (int j = 1; j < nbDivision; ++j)
            {
                int y = j * unitY;
                for (int k = 1; k < nbDivision; ++k)
                {
                    int x = k * unitX;
                    maps[y, x] = brownianNoise(Random.value);
                }
            }
            nbDivision *= 4;

            float moyenne = 0;
            for (int j = 0; j < datas.heightmapHeight; ++j)
                for (int k = 0; k < datas.heightmapHeight; k++)
                    moyenne += maps[k, j];
            moyenne /= (datas.heightmapHeight * datas.heightmapWidth);
            Ecart = 0;
            for (int j = 0; j < datas.heightmapHeight; ++j)
                for (int k = 0; k < datas.heightmapHeight; k++)
                    Ecart += Mathf.Pow((maps[k, j] - moyenne), 2);
            Ecart /= (datas.heightmapHeight * datas.heightmapWidth);
        }
        datas.SetHeights(0, 0, maps);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
