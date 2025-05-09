using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPrefabDateBase : MonoBehaviour
{
    public ResourceLoot[] resourceLV1;
    public ResourceLoot[] resourceLv2;
    public ResourceLoot[] resourceLv4;
    public List<GameObject> resourceIsland = new List<GameObject>();

    public void CreateIslandResources()
    {
        resourceIsland.Clear();

        if (resourceLV1.Length > 0)
        {
            foreach(ResourceLoot l in resourceLV1)
            {
                for(int i = 0; i < l.amount; i++)
                {
                    resourceIsland.Add(l.resource);
                }
            }
        }

        if(CoreGame.instance.gameManager.playerLevel >= 2)
        {
            if(resourceLv2.Length > 0)
            {
                foreach(ResourceLoot l in resourceLv2)
                {
                    for (int i = 0; i < l.amount; i++)
                    {
                        resourceIsland.Add(l.resource);
                    }
                }
            }
        }

        if (CoreGame.instance.gameManager.playerLevel >= 4)
        {
            if (resourceLv4.Length > 0)
            {
                foreach (ResourceLoot l in resourceLv4)
                {
                    for (int i = 0; i < l.amount; i++)
                    {
                        resourceIsland.Add(l.resource);
                    }
                }
            }
        }
    }
}
