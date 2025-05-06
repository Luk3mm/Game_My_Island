using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPrefabDateBase : MonoBehaviour
{
    public ResourceLoot[] resourceLV1;
    public List<GameObject> resourceIsland = new List<GameObject>();

    public void CreateIslandResources()
    {
        resourceIsland.Clear();

        if (resourceLV1.Length == 0)
        {
            return;
        }

        foreach(ResourceLoot l in resourceLV1)
        {
            for(int i = 0; i < l.amount; i++)
            {
                resourceIsland.Add(l.resource);
            }
        }
    }
}
