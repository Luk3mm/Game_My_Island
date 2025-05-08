using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private int hitAmount;
    public Item item;

    public IslandSlotGrid slot;

    // Start is called before the first frame update
    void Start()
    {
        hitAmount = item.hitAmount;
    }

    public void SetSlot(IslandSlotGrid s)
    {
        slot = s;
        transform.position = slot.transform.position;
        GetComponent<SpriteRenderer>().sortingOrder = s.line;
    }

    private void OnMouseOver()
    {
        CoreGame.instance.gameManager.ActiveCursor(this.gameObject);
    }

    private void OnMouseExit()
    {
        CoreGame.instance.gameManager.DisableCursor();
    }

    private void OnHit()
    {
        hitAmount--;

        if (hitAmount <= 0)
        {
            CoreGame.instance.gameManager.Loot(item, transform.position);
            slot.Busy(false);
            Destroy(this.gameObject);
        }
    }
}
