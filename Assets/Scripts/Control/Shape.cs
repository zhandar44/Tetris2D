using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    private Transform pivot;
    private bool isPause = false;
    private float timer = 0;
    private float stepTime = 0.5f;
    private Control ctrl;
    private GameManager gameManager;
    //用于移动端移动按钮
    private bool UpButtonClick = false;
    private bool LeftButtonClick = false;
    private bool RightButtonClick = false;
    private bool DownButtonClick = false;

    private bool isclick = false;
    private bool isSpeedUp = false;
    //private PlayWithMobile playWithMobile;
    private int multiple = 20;    //下落下速倍数

    void Awake()
    {
        pivot = transform.Find("Pivot");
        transform.Find("/View/Canvas/GameUI/UpButton").GetComponent<Button>().onClick.AddListener(OnUpButtonClick);
        transform.Find("/View/Canvas/GameUI/LeftButton").GetComponent<Button>().onClick.AddListener(OnLeftButtonClick);
        transform.Find("/View/Canvas/GameUI/RightButton").GetComponent<Button>().onClick.AddListener(OnRightButtonClick);
        transform.Find("/View/Canvas/GameUI/DownButton").GetComponent<Button>().onClick.AddListener(OnDownButtonClick);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPause)
            return;
        timer += Time.deltaTime;
        if (timer > stepTime)
        {
            timer = 0;
            Fall();
        }
        InputControl();

    }
    private void InputControl()
    {
       // if (isSpeedUp)
            //return;
        float h = 0;
        
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)||LeftButtonClick)
        {
            h = -1;
        LeftButtonClick = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)||RightButtonClick)
        {
            h = 1;
        RightButtonClick = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)||UpButtonClick)
        {
            //Debug.Log("keyup");
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
            UpButtonClick = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)||DownButtonClick)
        {
            isSpeedUp = true;
            stepTime /= multiple;
            DownButtonClick = false;
        }       
        if (h != 0)
        {
            Vector2 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (ctrl.model.IsValidMapPosition(this.transform) == false)//判断是否遇到边界或者碰到其他方块
            {
                pos.x -= h;
                transform.position = pos;
                
            }
            else
            {
                ctrl.audioManager.PlayControl();//移动成功播放音效
            }
            
            
        }
        

    }

    public void Init(Color color, Control ctrl, GameManager gameManager)
    {
        foreach (Transform t in transform)//遍历孩子
        {
            if (t.tag == "Block")
                t.GetComponent<SpriteRenderer>().color = color;
        }
        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }
    private void Fall()
    {
        Vector2 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if (ctrl.model.IsValidMapPosition(this.transform) == false)
        {
            pos.y += 1;
            transform.position = pos;//目标位置不可动，需返回上个位置
            isPause = true;
            
            //
            bool isLineClear=ctrl.model.PlaceShape(this.transform);
            if (isLineClear) ctrl.audioManager.PlayLineClear();
            gameManager.FallDown();
            return;
            
        }
        ctrl.audioManager.PlayDrop();

    }
    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }
    public void OnUpButtonClick()
    {
        UpButtonClick = true;
    }
    public void OnLeftButtonClick()
    {
        LeftButtonClick = true;
    }
    public void OnRightButtonClick()
    {
        RightButtonClick = true;
    }
    public void OnDownButtonClick()
    {
        DownButtonClick = true;
    }
}
