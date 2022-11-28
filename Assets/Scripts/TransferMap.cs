using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;
    private PlayerManager thePlayer;
    private FadeManager theFade;
    private OrderManager theOrder;
    private AudioManger theAudio;

    [SerializeField]
    public string dooropen;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("UP, DOWN, LEFT, RIGHT")]
    public string direction; //캐릭터가 보는 방향
    private Vector2 vector; //gerFloat("DirX")

    [Tooltip("문이 있으면: ture, 문이 없으면: false")]
    public bool door; //문이 있냐 없냐 체크

    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (door)
        {
            if (collision.gameObject.name == "Player")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
                    switch (direction)
                    {
                        case "UP":
                            if(vector.y == 1f)
                                StartCoroutine(TranferCoroutine());
                            break;
                        case "DOWN":
                            if (vector.y == -1f)
                                StartCoroutine(TranferCoroutine());
                            break;
                        case "RIGHT":
                            if (vector.x == 1f)
                                StartCoroutine(TranferCoroutine());
                            break;
                        case "LEFT":
                            if (vector.x == -1f)
                                StartCoroutine(TranferCoroutine());
                            break;
                        default:
                            StartCoroutine(TranferCoroutine());
                            break;
                    }   
                }                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!door)
        {
            if(collision.gameObject.name == "Player")
            {
                StartCoroutine(TranferCoroutine());
            }
        }
    }


    IEnumerator TranferCoroutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.NotMove();
        theFade.FadeOut();
        if(door)
        {
            anim_1.SetBool("Open", true);
            theAudio.Play(dooropen);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", true);
            }
        }
        yield return new WaitForSeconds(0.5f);

        theOrder.SetTransparent("player");
        if (door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", false);
            }
        }

        yield return new WaitForSeconds(0.5f);
        theOrder.UnSetTransparent("player");

        thePlayer.currentMapName = transferMapName;
        SceneManager.LoadScene(transferMapName);
        theFade.FadeIn();
        theOrder.Move();
    }
}
