using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Item/New Item", order = 1)]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public ItemCategory itemCategory;
    public string itemName;
    public Sprite itemSprite;
    [TextArea(2, 4)]
    public string itemDescription;
    public string itemBenefict;
    public int hitAmount;
    public int lootAmount;
    public int lifeAmount;
    public int energyAmount;

    public bool isRecoveryEnergy;
    public bool isRecoveryLife;

    public GameObject lootPrefab;
}
