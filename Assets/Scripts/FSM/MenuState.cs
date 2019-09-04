using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState {
    private void Awake()
    {
        stateID = StateID.Menu;
        AddTransition(Transition.StartButtonClick, StateID.Play);//状态转换
    }
    //进入该脚本时调用
    public override void DoBeforeEntering() 
    {
        ctrl.view.ShowMenu();
        ctrl.cameraManager.ZoomOut();
    }
    //离开该脚本时调用
    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenu();
    }
    public void OnStartButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        fsm.PerformTransition(Transition.StartButtonClick);
    }
    public void OnRankButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.view.ShowRankUI(ctrl.model.Score, ctrl.model.HighScore, ctrl.model.NumbersGame);
    }
    public void OnDeleteButtonClick()
    {
        ctrl.model.ClearData();
        OnRankButtonClick();
    }
    public void OnRestartButtonClick()
    {
        ctrl.model.Restart();
        ctrl.gameManager.ClearShape();
        fsm.PerformTransition(Transition.StartButtonClick);
    }
    
}
