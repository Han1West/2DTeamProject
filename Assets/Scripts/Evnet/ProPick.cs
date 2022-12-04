using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPick : MonoBehaviour
{
    private DialogueManager theDM;
    private OrderManager theOrder;
    private DatabaseManager theDatabase;

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;


    public int itemID;
    public int _count;
    public string pickUpSound;

    public bool ItemFlag;
    public bool DiaFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!ItemFlag && collision.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.Z))
        {
            ItemFlag = true;
            theDatabase.switches[0] = true;
            AudioManger.instance.Play(pickUpSound);
            Inventory.instance.GetAnItem(itemID, _count);
            DiaFlag = false;
        }

        if(!DiaFlag)
        {
            DiaFlag = true;
            StartCoroutine(DiaCoroutine());
        }
    }
    IEnumerator DiaCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();
    }

}
