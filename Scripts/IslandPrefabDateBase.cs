using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPrefabDateBase : MonoBehaviour
{
    public ResourceLoot[] resourceLV1;
    public ResourceLoot[] resourceLv2;
    public ResourceLoot[] resourceLv3;
    public ResourceLoot[] resourceLv4;
    public ResourceLoot[] resourceLv5;

    public int[] levelUpgrade;

    public List<GameObject> resourceIsland = new List<GameObject>();

    public void CreateIslandResources()
    {
        resourceIsland.Clear();

        for(int i = 0; i < levelUpgrade.Length; i++)
        {
            if(CoreGame.instance.gameManager.playerLevel >= levelUpgrade[i])
            {
                switch (i)
                {
                    case 0:
                        ResourceLevel(resourceLV1);
                        break;

                    case 1:
                        ResourceLevel(resourceLv2);
                        break;

                    case 2:
                        ResourceLevel(resourceLv3);
                        break;

                    case 3:
                        ResourceLevel(resourceLv4);
                        break;

                    case 4:
                        ResourceLevel(resourceLv5);
                        break;
                }
            }
        }
    }

    public void ResourceLevel(ResourceLoot[] res)
    {
        if(res.Length > 0)
        {
            foreach(ResourceLoot l in res)
            {
                for(int i = 0; i < l.amount; i++)
                {
                    resourceIsland.Add(l.resource);
                }
            }
        }
    }
}
