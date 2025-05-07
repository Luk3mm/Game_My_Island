using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    private IslandPrefabDateBase dataBase;
    private IslandSlotGrid[] slot;

    public int initialResources;
    public int maxResources;

    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponentsInChildren<IslandSlotGrid>();

        dataBase = GetComponent<IslandPrefabDateBase>();
        dataBase.CreateIslandResources();

        if(initialResources > 0 && dataBase.resourceIsland.Count > 0)
        {
            for(int i = 0; i < initialResources; i++)
            {
                NewResource();
            }
        }
    }

    private void NewResource()
    {
        int idSlot = Random.Range(0, slot.Length);
        IslandSlotGrid s = slot[idSlot];

        if(s.isBusy == false)
        {
            int idResource = Random.Range(0, dataBase.resourceIsland.Count);

            GameObject resource = Instantiate(dataBase.resourceIsland[idResource]);

            resource.GetComponent<Mine>().SetSlot(s);
            s.Busy(true);
        }
        else if(s.isBusy == true)
        {
            NewResource();
        }
    }
}
