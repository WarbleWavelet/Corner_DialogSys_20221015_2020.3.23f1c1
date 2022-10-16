/****************************************************
    文件：UIMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/15 23:31:53
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

namespace Demo0_0
{
    public class UIMgr :Singleton<UIMgr>
    {
        #region 字段


        DialogPanel dialogPanel;
        Transform canvas;





        #endregion

        #region 生命

        public void InitMgr()
        { 
            canvas = GameObject.Find("Canvas").transform;                    
            dialogPanel = canvas.transform.Find("UIRoot/DialogPanel").GetComponent<DialogPanel>();    
        }
        #endregion 

        #region 系统

        #endregion 

        #region 辅助
        public void DialogPanel_Show(bool state=true)
        {
            dialogPanel.gameObject.SetActive(state);
        }



        public void DialogPanel_Show(string dialog)
        {
            DialogPanel_Show();
            dialogPanel.SetText(String.Format("{0}", dialog));

        }

        public void DialogPanel_NextBtn(bool state ,UnityAction btnNxt_action=null)
        {
            dialogPanel.m_btnNext.gameObject.SetActive(state);
        }

        public void DialogPanel_NextBtn_Awake(UnityAction btnNxt_action = null)
        {
                    dialogPanel.m_btnNext.onClick.AddListener(btnNxt_action);
        }

            /// <summary>
            /// 对话
            /// </summary>
            /// <param name="sprite"></param>
            /// <param name="name"></param>
            /// <param name="dialog"></param>
            public void DialogPanel_Dialog(Sprite sprite, string name,string dialog)
        {
            DialogPanel_Show();
            dialogPanel.SetHead(sprite);
            dialogPanel.SetText(  String.Format("{0}：“{1}”", name,dialog));
         
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="state"></param>
        /// <param name="btn1_tmp"></param>
        /// <param name="btn2_tmp"></param>
        /// <param name="btn1_Click"></param>
        /// <param name="btn2_Click"></param>
        public void DialogPanel_Chose(bool state ,
            string btn1_tmp=null, 
            string btn2_tmp = null,
            UnityAction btn1_Click = null,
            UnityAction btn2_Click = null
            )
        {
            DialogPanel_Show();
            DialogPanel_NextBtn(!state);
            dialogPanel.m_btnNext.gameObject.SetActive(!state);
            dialogPanel.m_btnRight.gameObject.SetActive(state);
            dialogPanel.m_btnLeft.gameObject.SetActive(state);
            dialogPanel.m_btnRight.onClick.RemoveAllListeners();
            dialogPanel.m_btnLeft.onClick.RemoveAllListeners();

            if (state == false)
            {
                return;
            }

            dialogPanel.m_btnRight.GetComponentInChildren<TextMeshProUGUI> ().text = btn1_tmp;
            dialogPanel.m_btnLeft.GetComponentInChildren<TextMeshProUGUI>().text = btn2_tmp;
           
            dialogPanel.m_btnRight.onClick.RemoveAllListeners();
            dialogPanel.m_btnRight.onClick.AddListener(btn1_Click);

            dialogPanel.m_btnLeft.onClick.RemoveAllListeners();
            dialogPanel.m_btnLeft.onClick.AddListener(btn2_Click);
        }
        #endregion




    }
}