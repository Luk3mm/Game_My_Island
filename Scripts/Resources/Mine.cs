using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private int hitAmount;
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        hitAmount = item.hitAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Destroy(this.gameObject);
        }
    }
}
