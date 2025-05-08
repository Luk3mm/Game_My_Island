using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        StartCoroutine(SpawnResources());
    }

    private void NewResource()
    {
        int idSlot = Random.Range(0, slot.Length);
        IslandSlotGrid s = slot[idSlot];

        if(s.isBusy == false)
        {
            if (CoreGame.instance.gameManager.PlayerDistance(s.transform.position) == true)
            {
                int idResource = Random.Range(0, dataBase.resourceIsland.Count);

                GameObject resource = Instantiate(dataBase.resourceIsland[idResource]);

                resource.GetComponent<Mine>().SetSlot(s);
                s.Busy(true);
            }
            else
            {
                NewResource();
            }

        }
        else if(s.isBusy == true)
        {
            NewResource();
        }
    }

    IEnumerator SpawnResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoreGame.instance.gameManager.timeToSpawnResource);

            int count = slot.Where(x => x.isBusy == true).Count();
            if(count < maxResources)
            {
                NewResource();
            }

            /*int isBusy = 0;
            foreach(IslandSlotGrid s in slot)
            {
                if(s.isBusy == true)
                {
                    isBusy++;
                }
            }

            if(isBusy < maxResources)
            {
                NewResource();
            }*/
        }
    }
}
