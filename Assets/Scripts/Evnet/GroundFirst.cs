using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFirst : MonoBehaviour
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
    public Dialogue dialogue_12;
    public Dialogue dialogue_13;
    public Dialogue dialogue_14;
    public Dialogue dialogue_15;
    public Dialogue dialogue_16;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private FadeManager theFade;
    private Chase theZombie;

    public GameObject go;
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;



    private bool flag;
    public bool mon;

    // Start is called before the first frame update
    void Start()
    {

        theZombie = FindObjectOfType<Chase>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();

        npc2.SetActive(true);
        npc3.SetActive(true);
        npc4.SetActive(true);
        theOrder.NotMove(); //이벤트 시작시 이동불가
        mon = false;
        theZombie.NotMove();

        theOrder.Turn("NPC2", "RIGHT");
        theOrder.Turn("NPC3", "RIGHT");
        theOrder.Turn("NPC4", "RIGHT");


        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "UP");
        theOrder.Move("Player", "RIGHT");
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theOrder.Turn("NPC5", "LEFT");

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

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

        theDM.ShowDialogue(dialogue_11);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_12);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_13);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_14);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_15);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_16);
        yield return new WaitUntil(() => !theDM.talking);

        npc2.SetActive(false);
        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);

        go.SetActive(true);
        theZombie.CanMove();
        mon = true;
        theOrder.Move(); //이벤트 종료시 이동가능
    }
}
