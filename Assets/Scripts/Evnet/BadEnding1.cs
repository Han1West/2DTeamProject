using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEnding1 : MonoBehaviour
{

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;
    public Dialogue dialogue_3;
    public Dialogue dialogue_4;
    public Dialogue dialogue_5;
    public Dialogue dialogue_6;
  

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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag)
        {
            flag = true;
            StartCoroutine(EndingEventCoroutine());
        }
    }

    IEnumerator EndingEventCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove(); //이벤트 시작시 이동불가


        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_3);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_4);
        yield return new WaitUntil(() => !theDM.talking);

        theDM.ShowDialogue(dialogue_5);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Turn("NPC4", "UP");

        theDM.ShowDialogue(dialogue_6);
        yield return new WaitUntil(() => !theDM.talking);
        theOrder.Turn("NPC3", "DOWN");



        theOrder.Move(); //이벤트 종료시 이동가능
        //SceneManager.LoadScene("Start");
    }
}
