using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
    public int line;
    public bool isBusy;
    public Collider2D col;
    public SpriteRenderer spriteRender;

    public void Busy(bool value)
    {
        isBusy = value;

        col.enabled = !isBusy;
    }

    public void ShowIcon(bool value)
    {
        spriteRender.enabled = value;
    }

    private void OnMouseDown()
    {
        if(CoreGame.instance.gameManager.gamestate == GameState.CRAFT && isBusy == false)
        {
            CoreGame.instance.gameManager.SetCraftObject(this);
        }
    }
}
