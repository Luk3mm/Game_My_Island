using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item item;
    public Collider2D col;

    private bool isActive;
    private float startYPosition;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true && transform.position.y < startYPosition - (Random.Range(0.2f, 0.6f)))
        {
            rig.gravityScale = 0;
            rig.velocity = Vector2.zero;
            col.enabled = true;
            isActive = false;
        }
    }

    void Active(int dir)
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.enabled = false;

        startYPosition = transform.position.y;
        rig.gravityScale = 1.8f;
        rig.AddForce(Vector2.up * 250 + Vector2.right * (Random.Range(20, 35) * dir));
        isActive = true;
    }
}
