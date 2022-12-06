using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truth : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;
    public Dialogue dialogue_6;
    public Dialogue dialogue_7;

    public Dialogue dialogue_11;
    public Dialogue dialogue_22;
    public Dialogue dialogue_33;
    public Dialogue dialogue_44;
    public Dialogue dialogue_55;

    public Dialogue dialogue_01;
    public Dialogue dialogue_02;
    public Dialogue dialogue_03;
    public Dialogue dialogue_04;
    public Dialogue dialogue_05;
    public Dialogue dialogue_06;
    public Dialogue dialogue_07;
    public Dialogue dialogue_08;
    public Dialogue dialogue_09;
    public Dialogue dialogue_010;
    public Dialogue dialogue_011;
    public Dialogue dialogue_012;
    public Dialogue dialogue_013;
    public Dialogue dialogue_014;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;
    private DatabaseManager theDatabase;
    private Menu theMenu;

    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;
    public GameObject PlayerNPC;

    private bool flag;
    //private bool AnotherFlag;

    // Start is called before the first frame update
    void Start()
    {
        theMenu = FindObjectOfType<Menu>();
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theDatabase = FindObjectOfType<DatabaseManager>();
    }

    void Update()
    {
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

        theDM.ShowDialogue2(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue2(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue2(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue2(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue2(dialogue_5);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue2(dialogue_6);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue2(dialogue_7);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Move(); //이벤트 종료시 이동가능

    
        StartCoroutine(AnotherEventCoroutine());
      
    }

    IEnumerator AnotherEventCoroutine()
    {
        //AnotherFlag = true;
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가

        theOrder.SetTransparent("Player");
        npc3.SetActive(true);
        npc4.SetActive(true);
        npc5.SetActive(true);
        PlayerNPC.SetActive(true);
        

        theDM.ShowDialogue(dialogue_11);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Turn("NPC31", "DOWN");
        yield return new WaitForSeconds(0.1f);

        theOrder.Turn("NPC41", "DOWN");
        yield return new WaitForSeconds(0.1f);

        theOrder.Turn("NPC51", "DOWN");
        yield return new WaitForSeconds(0.1f);

        theOrder.Turn("PlayerNPC", "UP");
        yield return new WaitForSeconds(0.1f);

        theDM.ShowDialogue(dialogue_22);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_33);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_44);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_55);
        yield return new WaitUntil(() => !theDM.talking);


        npc3.SetActive(false);
        npc4.SetActive(false);
        npc5.SetActive(false);
        PlayerNPC.SetActive(false);
        theOrder.UnSetTransparent("Player");

        theOrder.Move(); //이벤트 종료시 이동가능

        StartCoroutine(RealEndingCoroutine());
    }
    IEnumerator RealEndingCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가

        theDM.ShowDialogue(dialogue_01);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_02);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_03);
        yield return new WaitUntil(() => !theDM.talking);

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

        theDM.ShowDialogue(dialogue_09);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_010);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_011);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_012);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_013);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_014);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move(); //이벤트 종료시 이동가능

        StartCoroutine(RealEndingCoroutine());
        theMenu.ToTitle();
    }
}
