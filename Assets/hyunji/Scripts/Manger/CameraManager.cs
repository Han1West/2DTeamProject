using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target; //쫓을 대상
    public float moveSpeed; //카메라 속도
    private Vector3 targetPosition; // 대상의 현재 위치값


    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;
    // 카메라 영역을위한 변수

    private float halfWidth;
    private float halfHeight;
    // 카메라 영역이 벗어나는 경우 반높이, 반너비값을 위해 지닌 변수

    private Camera theCamera;
    // 카메라의 반높이값을 구할 속성을 이용하기 위한 변수


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); //다른 씬으로 넘어갈 때 파괴 X
            instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); //1초의 movespeed만큼 이동

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
