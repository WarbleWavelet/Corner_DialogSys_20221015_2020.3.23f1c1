/****************************************************
    文件：GameStart.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/15 23:41:30
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class GameStart : MonoBehaviour
    {
        #region 字段

        public int idx=2;
        [Header("Mgr")]
        public UIMgr m_UIMgr;
        public DialogMgr m_DialogMgr;
        public AudioMgr m_AudioMgr;
        #endregion

        #region 生命

        private void Awake()
        {
            m_UIMgr = UIMgr.Instance;
            m_DialogMgr = DialogMgr.Instance;
            m_AudioMgr = AudioMgr.Instance;

            m_UIMgr.InitMgr();
            m_DialogMgr.InitMgr(this);
            m_AudioMgr.InitMgr();
        }

        void Start()
        {
            
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
              
                DialogMgr.Instance.OnNextBtnClick();
            }
        }
        #endregion 

        #region 系统

        #endregion 

        #region 辅助

        #endregion
    }
}