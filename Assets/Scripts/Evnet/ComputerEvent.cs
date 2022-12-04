using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEvent : MonoBehaviour
{
    [SerializeField]
    public Choice choice;


    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private TransferMap theTranfer;
    private DialogueManager theDM;

    public Dialogue dialogue_1;

    public int result;

    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theTranfer = FindObjectOfType<TransferMap>();
        theOrder = FindObjectOfType<OrderManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    public void TranferToResult(int _result)
    {
        switch (_result)
        {
            case 0:
                flag = true;
                StartCoroutine(theTranfer.TranferCoroutine("FrontChangjo"));
                break;
            case 1:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag && collision.gameObject.name == "Player")
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);
        yield return new WaitUntil(() => !theDM.talking);

        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choicing);
        theOrder.Move();
        result = theChoice.GetResult();
        Debug.Log(theChoice.GetResult());
        TranferToResult(result);
    }
}
