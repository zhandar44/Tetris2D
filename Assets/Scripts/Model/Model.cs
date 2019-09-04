using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public const int MAX_ROWS = 23;   //多加三行判断当达到顶部时是否还被占用
    public const int MAX_COLUMNS = 10;
    public const int NORMAL_ROWS = 20;

    private Transform [,]  map = new  Transform[MAX_COLUMNS, MAX_ROWS];//默认类型为float
    //

    private int score = 0;//当前分数
    private int highScore=0;//最高分数
    private int numsGame = 0;//游戏局数

    public bool isDataUpdate = false;

    public int Score { get { return score; }}
    public int HighScore { get { return highScore; } }
    public int NumbersGame { get { return numsGame; } }
    void Awake()
    {
        LoadData();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        numsGame=PlayerPrefs.GetInt("NumbersGame", 0);

    }
    /// <summary>
    /// 存储数据
    /// </summary>
    private void SaveData()
    {
        //存储与注册表中
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumbersGame", numsGame);
    }
    public void ClearData()
    {
        score = 0;
        highScore = 0;
        numsGame = 0;
        SaveData();
    }
    public bool IsValidMapPosition (Transform t)
    {
        foreach(Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();//拓展方法Vector3Exception,获取当前砖块位置
            if (IsInsideMap(pos) == false) return false;//判断是否找过边界
            //Debug.Log(pos.x);
            //Debug.Log(pos.y);
            if (map[(int)pos.x,(int)pos.y] != null) return false;//判断该区域是否有其他图形
            
        }
        return true;//该区域可移动
    }

    public bool isGameOver()
    {
        for(int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for(int j = 0; j < MAX_COLUMNS; j++)
            {
                if (map[j, i] != null)//上三行存在方块无法堆放
                {
                    numsGame++;
                    SaveData();
                    return true;
                }
                    

            }

        }
        return false;

    }
    public bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
        //判断是否超过左边界，右边界，下边界
    }
    //用于掉落砖块后对地图的赋值
    public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }
       return CheckMap();
    }
    //用于消除行
    public bool CheckMap()
    {
        int count = 0;//判断是否有行销毁
        for(int i=0; i < MAX_ROWS; i++)
        {
            bool isFull=CheckIsRowFull(i);
            //若该行满
            if (isFull)
            {
                count++;
                DeleteRow(i);
                MoveDownRowsAbove(i+1);
                i--;
            }

        }
        if (count > 0)
        {

            score += (count * 10);//分数增加
            if (score > highScore)
            {
                highScore = score;
            }
            isDataUpdate = true;
                
            return true;

        }
            
        else
            return false;


    }
    //判断该行是否填满
    private bool CheckIsRowFull(int row)
    {
        for(int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] == null)
                return false;

        }
        return true;

    }
    private void DeleteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
                

        }

    }
    //消除后将上方行向下移动 
    private void MoveDownRowsAbove(int row)
    {
        for(int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);

        }
    }
    //仅移动传入行
    private void MoveDownRow(int row)
    {
        for(int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i,row] != null)
            {
                map[i, row - 1] = map[i, row];//索引修改
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);//地图修改

            }
            
           
        }
    }
    public void Restart()
    {
        //遍历所有地图销毁其中的砖块
        for(int i = 0; i < MAX_COLUMNS; i++)
        {
            for(int j = 0; j < MAX_ROWS; j++)
            {
                if (map[i, j] != null)
                {
                    Destroy(map[i, j].gameObject);//销毁砖块
                    map[i, j] = null;
                }

            }

        }
        score = 0;
    }

}
