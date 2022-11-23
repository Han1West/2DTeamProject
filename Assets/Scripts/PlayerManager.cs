using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{

    // �� �̵� �ݺ��� ĳ�����ߺ����� ������ ���� ���� ����
    static public PlayerManager instance;
    public string currentMapName; // �� ��ũ��Ʈ�� �ִ� ���� �� ����


    // �޸��⸦ ���� ����
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
            DontDestroyOnLoad(this.gameObject); //�ٸ� ������ �Ѿ �� �ı� X
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator MoveCoroutine() // Ÿ���̵��� �ε巯���� ���� ����ó�� �ڵ�
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

            // ���ÿ� Ű�� �Է½� ����� �̻������� ���� �����ϴ� ���ǹ�
            if (vector.x != 0)
                vector.y = 0;


            //��Ű�� ������ -1���� ������vector.x�� Dirx�� ����
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollsionFlag = base.CheckCollsion();
            if (checkCollsionFlag)
                break;
            // ��ȯ�Ǵ� ���� true �� �� �Ʒ��ڵ� ���� X (�̵� �ڵ� ���� X)

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
                if (applyRunFlag) //����Ʈ�� ������� ��ĭ�� �����̴� ���� ����
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
