using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSlotGrid : MonoBehaviour
{
    public int line;
    public bool isBusy;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Busy(bool value)
    {
        isBusy = value;

        col.enabled = !isBusy;
    }
}
