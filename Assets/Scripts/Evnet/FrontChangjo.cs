using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontChangjo : MonoBehaviour
{

    public Dialogue dialogue_01;
    public Dialogue dialogue_02;
    public Dialogue dialogue_03;
    public Dialogue dialogue_04;
    public Dialogue dialogue_05;
    public Dialogue dialogue_06;
    public Dialogue dialogue_07;
    public Dialogue dialogue_08;

    public Dialogue dialogue_11;
    public Dialogue dialogue_12;
    public Dialogue dialogue_13;
    public Dialogue dialogue_14;
    public Dialogue dialogue_15;
    public Dialogue dialogue_16;

    public Dialogue dialogue_21;
    public Dialogue dialogue_22;
    public Dialogue dialogue_23;
    public Dialogue dialogue_24;
    public Dialogue dialogue_25;
    public Dialogue dialogue_26;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private DatabaseManager theDatabase;
    private Menu theMenu;

    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;

    private bool flag;
    public bool branch;

    // Start is called before the first frame update
    void Start()
    {
        theMenu = FindObjectOfType<Menu>();
        theDatabase = FindObjectOfType<DatabaseManager>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }
    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        branch = theDatabase.switches[0];
        if (!flag)
        {
            if (branch)
            {
                flag = true;
                StartCoroutine(SecondEventCoroutine());
            }
            else
            {
                flag = true;
                StartCoroutine(FirstEventCoroutine());
            }

        }
    }

    IEnumerator FirstEventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove(); //이벤트 시작시 이동불가

        theOrder.Turn("NPC3", "RIGHT");
        yield return new WaitForSeconds(0.5f);
        theOrder.Turn("NPC3", "LEFT");
        yield return new WaitForSeconds(0.5f);
        theOrder.Turn("NPC3", "RIGHT");
        yield return new WaitForSeconds(0.5f);

        theDM.ShowDialogue(dialogue_01);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_02);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("NPC3", "RIGHT");
        yield return new WaitForSeconds(0.5f);
        theOrder.Turn("NPC3", "LEFT");
        yield return new WaitForSeconds(0.5f);
        theOrder.Turn("NPC3", "RIGHT");
        yield return new WaitForSeconds(0.5f);

        theDM.ShowDialogue(dialogue_03);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("Player", "RIGHT");
        yield return new WaitForSeconds(0.5f);
        theOrder.Turn("Player", "LEFT");
        yield return new WaitForSeconds(0.5f);
        theOrder.Move("Player", "DOWN");
        theOrder.Move("Player", "DOWN");
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_04);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_05);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_06);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_07);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_08);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move(); //이벤트 종료시 이동가능

        StartCoroutine(EndingEventCoroutine());
    }

    IEnumerator SecondEventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove(); //이벤트 시작시 이동불가

        theDM.ShowDialogue(dialogue_21);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_22);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("NPC4", "LEFT");

        theDM.ShowDialogue(dialogue_23);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_24);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("NPC3", "RIGHT");
        theDM.ShowDialogue(dialogue_25);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_26);
        yield return new WaitUntil(() => !theDM.talking);

        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);

        theOrder.Move(); //이벤트 종료시 이동가능
    }

    IEnumerator EndingEventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가


        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

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


        theOrder.Move(); //이벤트 종료시 이동가능
        theMenu.ToTitle();
    }
}
