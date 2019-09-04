using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制层
/// </summary>
public class Control : MonoBehaviour {
    [HideInInspector]
    public Model model;
    [HideInInspector]        //在Inspector界面上隐藏
    public View view;
    private FSMSystem fsm;
    [HideInInspector]
    public CameraManager cameraManager;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public AudioManager audioManager;
    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        cameraManager = GetComponent<CameraManager>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }
    // Use this for initialization
    void Start () {
        MakeFSM();                    //创建有限状态机，不放入Awake中防止出现空指针异常

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (FSMState state in states)
        {
            fsm.AddState(state,this);
        }
        MenuState s = GetComponentInChildren<MenuState>();             //设置菜单为默认状态
        fsm.SetCurrentState(s);
    }
}
