using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    private QuestFloating theQuest;
    private DialogueManager theDM;
    private OrderManager theOrder;

    public Dialogue dialogue_1;

    // Start is called before the first frame update
    void Start()
    {
        theQuest = FindObjectOfType<QuestFloating>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(EventCoroutine());
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Move();
        yield return new WaitForSeconds(1f);
    }
    // Update is called once per frame
    void Update()
    {
        if(theQuest.count == 10)
            Destroy(this.gameObject);
    }
}
