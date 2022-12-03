using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealItemPickup : MonoBehaviour
{
    private DialogueManager theDM;
    private OrderManager theOrder;

    public Dialogue dialogue_1;


    public int itemID;
    public int _count;
    public string pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DiaCoroutine());
            theOrder.Move();
            AudioManger.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count);
            Destroy(this.gameObject);
        }
    }
    IEnumerator DiaCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);      
    }

}
