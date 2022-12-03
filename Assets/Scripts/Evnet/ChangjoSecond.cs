using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangjoSecond : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;


    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;


    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(SecondEventCoroutine());
        }
    }

    IEnumerator SecondEventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove(); //이벤트 시작시 이동불가

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("Player", "LEFT");
        theOrder.Turn("Player", "RIGHT");
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "RIGHT");

        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move(); //이벤트 종료시 이동가능
    }
}
