using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeItem : MonoBehaviour
{
    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerStat thePlayerStat;

    public Dialogue dialogue_1;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DiaCoroutine());
            theOrder.Move();
            thePlayerStat.Hit(2);
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
