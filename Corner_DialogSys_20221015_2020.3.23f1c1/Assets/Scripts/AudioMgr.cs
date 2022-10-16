/****************************************************
    文件：AudioMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/16 3:56:16
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class AudioMgr : Singleton<AudioMgr>
    {
        #region 字段

        public AudioSource audioSource; //音频源组件


       

        #endregion

        #region 生命
       public void InitMgr()
        {
            audioSource = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<AudioSource>();
        }
        

        #endregion 

        #region 系统

        #endregion 

        #region 辅助
        public void PlayAudio(AudioClip clip)  //播放音频
        {
            audioSource.PlayOneShot(clip);
        }
        #endregion
    }


}