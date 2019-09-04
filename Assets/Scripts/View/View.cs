using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;          //动画制作
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 视图层
/// </summary>
public class View : MonoBehaviour {
    private RectTransform logo;
    private RectTransform menuUI;
    private RectTransform gameUI;
    public GameObject restartButton;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject rankUI;
    //private GameObject starUI;

    private Text score;
    private Text highScore;
    private Text gameOverScore;
    private Text rankScore;
    private Text rankHighScore;
    private Text rankNumsGame;
    //private Text StarName;

    public Control ctrl;
    private GameObject mute;
	// Use this for initialization
	void Awake () {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Control>();
        logo = transform.Find("Canvas/Logo") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;
        //starUI = transform.Find("Canvas/StarUI").gameObject;
        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScoreLabel/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Score").GetComponent<Text>();

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/Score").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighScoreLabel/Score").GetComponent<Text>();
        rankNumsGame = transform.Find("Canvas/RankUI/NumberGamesLabel/Score").GetComponent<Text>();
        //StarName = transform.Find("Canvas/StarUI/StarName").GetComponent<Text>();
    }
	
	

	public void ShowMenu()
    {
        logo.gameObject.SetActive(true);
        logo.DOAnchorPosY(275f, 0.5f);                        //前者为目标位置，后者为移动速度
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(61.1f, 0.5f);
    }
    public void HideMenu()
    {
        logo.DOAnchorPosY(384f, 0.3f).OnComplete(delegate { logo.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-70f, 0.3f).OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }
    public void UpdateGameUI(int score = 0, int highScore = 0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();

    }
    public void ShowGameUI(int score = 0,int highScore=0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-86.1f, 0.5f);

    }
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(94.32f, 0.3f).OnComplete(delegate { gameUI.gameObject.SetActive(false); });
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
    public void ShowGameOverUI(int score=0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    //主页按钮点击
    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//加载当前场景
    }

    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }
    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }
    public void OnSettingUIClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }
    public void ShowRankUI(int score,int  highScore,int numsGame)
    {
        
        this.rankScore.text= score.ToString();
        this.rankHighScore.text= highScore.ToString();
        this.rankNumsGame.text = numsGame.ToString();
        rankUI.SetActive(true);
    }
    public void OnRankUIClick()
    {
        ctrl.audioManager.PlayCursor();
        rankUI.SetActive(false);
    }
    //public void ShowStarUI(string name)
    //{
    //    StarName.text = name;
    //    starUI.SetActive(true);
    //}
    //public void OnStarUIClick()
    //{
    //    ctrl.audioManager.PlayCursor();
    //    starUI.SetActive(false);
    //}
}
