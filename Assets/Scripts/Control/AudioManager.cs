using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {


    //设置备忘录
    //设置slider控制音效大小，音效真的烦人！

    public AudioClip cursor;//点击声音 

    public AudioClip drop;//砖块掉落声音

    public AudioClip control;//砖块移动声音

    private AudioSource audiosource;//audiosourse组件

    public AudioClip lineClear;

    private Slider slider;          //控制音量大小

    private bool isMute = false;//判断是否静音
    public Control ctrl;

    void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        slider = transform.Find("/View/Canvas/SettingUI/AudioCtl/Slider").GetComponent<Slider>();
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Control>();
    }



    /// <summary>
    /// 控制鼠标音效
    /// </summary>
	public void PlayCursor()
    {
        PlayAudio(cursor);
    }

    public void PlayDrop()
    {
        PlayAudio(drop);
    }
    public void PlayControl()
    {
        PlayAudio(control);
    }
    public void PlayLineClear()
    {
        PlayAudio(lineClear);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    private void PlayAudio(AudioClip clip)
    {
        if (isMute)
            return;
        audiosource.clip = clip;
        audiosource.Play();

    }
    /// <summary>
    /// 声音开关按钮点击
    /// </summary>
    public void OnAudioButtonClick()
    {
        isMute = !isMute;
        ctrl.view.SetMuteActive(isMute);
        slider.value = 0;
        //CtrlAudioButtonAndSlider();
        if (isMute == false)
        {
            PlayCursor();
        }
    }
    /// <summary>
    /// 控制音量大小
    /// </summary>
    public void OnSliderValueChange()
    {
        audiosource.volume = slider.value;
        CtrlAudioButtonAndSlider();
        PlayCursor();

    }
    /// <summary>
    /// 设置音量大小和声音开关
    /// </summary>
    private void CtrlAudioButtonAndSlider()
    {
        
        if (slider.value==0)
        {
            isMute = true;           
            ctrl.view.SetMuteActive(isMute);
            return;
        }
        if (slider.value != 0&&isMute == true)
        {
            isMute = false;
            ctrl.view.SetMuteActive(isMute);
            return;
        }


    }
}
