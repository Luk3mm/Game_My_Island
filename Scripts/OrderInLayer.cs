using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    private SpriteRenderer mSpriteRender;
    private float playerY;

    public float offSet;

    // Start is called before the first frame update
    void Start()
    {
        mSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if(mSpriteRender == null)
        {
            return;
        }

        playerY = CoreGame.instance.playerController.positionY;

        if(transform.position.y < playerY - offSet)
        {
            mSpriteRender.sortingLayerName = "FistPlan";
        }
        else
        {
            mSpriteRender.sortingLayerName = "SecondPlan";
        }
    }
}
