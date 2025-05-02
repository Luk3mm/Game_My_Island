using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    public Image itemImage;
    public Text amountText;

    public Image deleteBar;

    private bool isDelete;
    private float deltaTime;
    private float perc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDelete == true)
        {
            deltaTime += Time.deltaTime;

            perc = deltaTime / CoreGame.instance.gameManager.timeToDelete;

            deleteBar.fillAmount = perc;
        }

        if(deltaTime >= CoreGame.instance.gameManager.timeToDelete)
        {
            CoreGame.instance.inventory.DeleteItem(item);
        }
    }

    public void UpdateSlot(Item i, int amount)
    {
        deleteBar.gameObject.SetActive(false);
        item = i;
        itemImage.sprite = item.itemSprite;
        amountText.text = amount.ToString();
    }

    public void OnSlotClick(BaseEventData data)
    {
        PointerEventData pointData = data as PointerEventData;

        if (pointData.button == PointerEventData.InputButton.Left)
        {
            if(item.itemCategory == ItemCategory.COMSUMABLE)
            {
                CoreGame.instance.inventory.UseItem(item);
            }
        }

        if(pointData.button == PointerEventData.InputButton.Right)
        {
            isDelete = true;
            deltaTime = 0;
            deleteBar.fillAmount = 0;
            deleteBar.gameObject.SetActive(true);
        }
    }

    public void OnSlotUp(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            isDelete = false;
            deleteBar.gameObject.SetActive(false);
        }
    }

    public void MouseEnter()
    {
        CoreGame.instance.inventory.ShowItemInfo(item);
    }

    public void MouseExit()
    {
        CoreGame.instance.inventory.DisableItemInfo();
        isDelete = false;
    }
}
