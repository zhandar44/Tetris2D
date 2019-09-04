using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool isPause = true;
    public Shape[] shapes;
    public Color[] color;
    private Shape currentShape = null;
    private Control ctrl;
    private Transform blockHoder;
    //private PlayWithMobile playWithMobile;

    void Awake()
    {
        ctrl = GetComponent<Control>();
        //playWithMobile = GetComponent<PlayWithMobile>();
        blockHoder = transform.Find("BlockHolder");
    }

    // Update is called once per frame
    void Update() {
        if (isPause)  //游戏暂停
            return;
        if (currentShape == null)
        {
            SpawnShape();
        }

    }
    public  void ClearShape()
    {
        if (currentShape != null)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }
    public void StartGame()
    {
        isPause = false;
        if (currentShape != null)
            currentShape.Resume();
    }
    public void PauseGame()
    {
        isPause = true;
        if (currentShape != null)
            currentShape.Pause();
    }
    void SpawnShape()
    {
        int index = Random.Range(0, shapes.Length);
        int indexColor = Random.Range(0, shapes.Length);
        currentShape = Instantiate(shapes[index]);        //位置为prefab默认位置
        currentShape.transform.parent = blockHoder;
        currentShape.Init(color[indexColor],ctrl,this);

    }
    public void FallDown()
    {
        currentShape = null;
        if (ctrl.model.isDataUpdate)
        {
            ctrl.view.UpdateGameUI(ctrl.model.Score, ctrl.model.HighScore);
        }
        foreach(Transform t in blockHoder)//销毁物品，消除仅消除了block
        {
            if (t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
        if (ctrl.model.isGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }
}
