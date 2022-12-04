using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTet : MonoBehaviour
{
    [SerializeField]
    public Choice choice;


    private OrderManager theOrder;
    private ChoiceManager theChoice;
    private TransferMap theTranfer;

    public int result;

    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        theTranfer = FindObjectOfType<TransferMap>();
        theOrder = FindObjectOfType<OrderManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    public void TranferToResult(int _result)
    {
        switch (_result)
        {
            case 0:
                StartCoroutine(theTranfer.TranferCoroutine("changjo"));
                break;
            case 1:
                StartCoroutine(theTranfer.TranferCoroutine("Mirae"));
                break;
            case 2:
                StartCoroutine(theTranfer.TranferCoroutine("ChungSong"));
                break;
            case 3:
                StartCoroutine(theTranfer.TranferCoroutine("JungUi"));
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!flag && collision.gameObject.name == "Player")
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choicing);
        theOrder.Move();
        result = theChoice.GetResult();
        Debug.Log(theChoice.GetResult());
        TranferToResult(result);
    }
}
