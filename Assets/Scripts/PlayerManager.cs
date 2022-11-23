using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{

    // 맵 이동 반복시 캐릭터중복생성 방지를 위한 정적 변수
    static public PlayerManager instance;
    public string currentMapName; // 맵 스크립트에 있는 변수 값 저장


    // 달리기를 위한 변수
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = true;


    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); //다른 씬으로 넘어갈 때 파괴 X
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator MoveCoroutine() // 타일이동의 부드러움을 위한 다중처리 코드
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

            // Using shift to run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }



            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // 동시에 키를 입력시 모션이 이상해지는 것을 방지하는 조건문
            if (vector.x != 0)
                vector.y = 0;


            //좌키를 누르면 -1값을 가지는vector.x가 Dirx에 저장
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollsionFlag = base.CheckCollsion();
            if (checkCollsionFlag)
                break;
            // 반환되는 값이 true 일 때 아래코드 실행 X (이동 코드 실행 X)

            animator.SetBool("walking", true);

            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag) //쉬프트를 누를경우 두칸씩 움직이는 것을 방지
                    currentWalkCount++;
                currentWalkCount++;
                if (currentWalkCount == 12)
                    boxCollider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {


            // moving input and apply
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());

            }
        }
    }

}
