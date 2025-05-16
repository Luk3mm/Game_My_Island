using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public GameObject inventoryPanel;
    public GameObject[] subPanel;
    public int idSubPanel;
    public GameObject slotPrefab;

    [Header("Item Infos")]
    public GameObject itemInfoWindow;
    public Image itemImage;
    public Text itemName;
    public Text itemDescription;
    public Text itemBenefict;
    public Text itemCategory;

    public RectTransform slotGrid;

    private List<GameObject> inventorySlot = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory.Add(item, amount);
        }

        Debug.Log(inventory[item]);
    }

    public void UseItem(Item item)
    {
        if (inventory.ContainsKey(item))
        {
            switch (item.itemCategory)
            {
                case ItemCategory.COMSUMABLE:

                    if(CoreGame.instance.gameManager.IsNeedLife() == true && item.isRecoveryEnergy == true)
                    {
                        UpdateItemIventory(item);

                        CoreGame.instance.gameManager.SetPlayerEnergy(item.energyAmount);
                    }

                    if(CoreGame.instance.gameManager.IsNeedEnergy() == true && item.isRecoveryLife == true)
                    {
                        UpdateItemIventory(item);

                        CoreGame.instance.gameManager.SetPlayerEnergy(item.lifeAmount);
                    }

                    break;
            }
        }
    }

    private void UpdateItemIventory(Item item)
    {
        inventory[item] -= 1;

        if (inventory[item] <= 0)
        {
            DeleteItem(item);
        }
        else
        {
            UpdateInventory();
        }
    }

    public void DeleteItem(Item item)
    {
        inventory.Remove(item);

        UpdateInventory();

        DisableItemInfo();
    }

    public void ShowInventory()
    {
        DisableItemInfo();
        InventoryTabs(0);

        bool isActive = !inventoryPanel.activeSelf;

        inventoryPanel.SetActive(isActive);

        if(isActive == true)
        {
            CoreGame.instance.gameManager.ChangeGameState(GameState.INVENTORY);
            UpdateInventory();
        }
        else
        {
            CoreGame.instance.gameManager.ChangeGameState(GameState.GAMEPLAY);
        }
    }

    void UpdateInventory()
    {
        foreach(GameObject s in inventorySlot)
        {
            Destroy(s);
        }

        inventorySlot.Clear();

        foreach(KeyValuePair<Item, int> item in inventory)
        {
            GameObject i = Instantiate(slotPrefab, slotGrid);
            inventorySlot.Add(i);
            i.GetComponent<InventorySlot>().UpdateSlot(item.Key, item.Value);
        }
    }

    public void ShowItemInfo(Item item)
    {
        itemImage.sprite = item.itemSprite;
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;
        itemBenefict.text = "";
        string itemCategory = "";

        switch (item.itemCategory)
        {
            case ItemCategory.MATERIAL:
                itemCategory = "Material";
                break;
            case ItemCategory.COMSUMABLE:
                itemCategory = "Consumivel";
                break;
        }

        this.itemCategory.text = itemCategory;

        if(item.isRecoveryEnergy == true)
        {
            itemBenefict.text = "Recupera " + item.energyAmount.ToString() + " de energia.";
        }

        if(item.isRecoveryLife == true)
        {
            itemBenefict.text += "\nRecupera <color=green>" + item.lifeAmount.ToString() + "</color> de vida.";
        }

        itemInfoWindow.SetActive(true);
    }

    public void DisableItemInfo()
    {
        itemInfoWindow.SetActive(false);
    }

    public void InventoryTabs(int idTab)
    {
        foreach(GameObject t in subPanel)
        {
            t.SetActive(false);
        }

        subPanel[idTab].SetActive(true);
    }
}
