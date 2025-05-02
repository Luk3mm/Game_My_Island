using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool isWalk;
    private bool isLookLeft;
    private bool isAction;
    private bool isActionButton;

    private Rigidbody2D rig;
    private Animator animController;

    private Vector2 movimentInput;
    private Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CoreGame.instance.inventory.ShowInventory();
        }

        if(CoreGame.instance.gameManager.gamestate != GameState.GAMEPLAY)
        {
            return;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePosition.x < transform.position.x && isLookLeft == false)
        {
            Flip();
        }

        if(mousePosition.x > transform.position.x && isLookLeft == true)
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1") && isAction == false)
        {
            isActionButton = true; 
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isActionButton = false;
        }

        if(isActionButton == true && isAction == false)
        {
            isAction = true;
            animController.SetTrigger("axe");
        }

        movimentInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isWalk = movimentInput.magnitude != 0;

        if(isAction == false)
        {
            rig.velocity = movimentInput * speed;
        }
        else if(isAction == true)
        {
            rig.velocity = Vector2.zero;
            isWalk = false;
        }
        
        animController.SetBool("walk", isWalk);
    }

    void Flip()
    {
        if(isAction == true)
        {
            return;
        }

        isLookLeft = !isLookLeft;

        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, 1, 1);
    }

    void AxeHit()
    {
        CoreGame.instance.gameManager.ObjectHit();
    }

    void ActionFinish()
    {
        isAction = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Loot":

                Item item = collision.gameObject.GetComponent<Loot>().item;

                CoreGame.instance.inventory.GetItem(item, 1);

                Destroy(collision.gameObject);
                break;
        }
        
    }
}
