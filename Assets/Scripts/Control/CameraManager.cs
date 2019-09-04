using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 管理摄像机
/// </summary>
public class CameraManager : MonoBehaviour {

    // Use this for initialization
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 放大摄像机的摄影范围
    /// </summary>
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(13.43f, 0.5f);
    }
    /// <summary>
    ///缩小摄像机的摄影范围
    /// </summary>
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(19.39f, 0.5f);                 //控制大小
    }
    /// <summary>
    /// 手机端开始放大摄像头
    /// </summary>
    public void MobileZoomIn()
    {
        mainCamera.DOOrthoSize(15.03f, 0.5f);
    }
}
