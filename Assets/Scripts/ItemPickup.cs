using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private QuestFloating theQuest;
    private DialogueManager theDM;
    private OrderManager theOrder;

    public Dialogue dialogue_1;


    public int itemID;
    public int _count;
    public string pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        theQuest = FindObjectOfType<QuestFloating>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            AudioManger.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count);
            theQuest.count += 1;
            Destroy(this.gameObject);        
        }
    }

}
