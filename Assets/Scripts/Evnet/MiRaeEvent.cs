using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiRaeEvent : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;
    public Dialogue dialogue_6;
    public Dialogue dialogue_7;
    public Dialogue dialogue_8;
    public Dialogue dialogue_9;
    public Dialogue dialogue_10;

    public Dialogue dialogue_11;
    public Dialogue dialogue_22;
    public Dialogue dialogue_33;
    public Dialogue dialogue_44;
    public Dialogue dialogue_55;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private Menu theMenu;

    public GameObject npc11;


    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theMenu = FindObjectOfType<Menu>();
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
            StartCoroutine(FirstEventCoroutine());
        }
    }

    IEnumerator FirstEventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove(); //이벤트 시작시 이동불가


        theOrder.Turn("NPC3", "UP");
        theOrder.Turn("NPC4", "UP");
        theOrder.Turn("NPC5", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "UP");

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);
        npc11.SetActive(true);
        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_5);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_6);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_7);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_8);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_9);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_10);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move(); //이벤트 종료시 이동가능

        StartCoroutine(EndingEventCoroutine());
    }

    IEnumerator EndingEventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가


        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_11);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_22);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_33);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_44);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_55);
        yield return new WaitUntil(() => !theDM.talking);


        theOrder.Move(); //이벤트 종료시 이동가능
        theMenu.ToTitle();
    }
}
