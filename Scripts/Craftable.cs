using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour
{
    public GameObject[] craftItemList;

    public void Crafting(int idItemCraft)
    {
        CoreGame.instance.gameManager.StartCraftMode(craftItemList[idItemCraft]);
    }
}
