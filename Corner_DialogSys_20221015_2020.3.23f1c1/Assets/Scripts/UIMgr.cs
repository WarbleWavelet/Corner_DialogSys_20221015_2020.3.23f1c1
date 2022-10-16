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
using UnityEngine;
using Random = UnityEngine.Random;
 
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
           // dialogPanel.m_btnNext.gameObject.SetActive(state);

        }

        public void DialogPanel_Show(Sprite sprite, string name,string dialog)
        {
            DialogPanel_Show();
            dialogPanel.SetHead(sprite);
            dialogPanel.SetText(  String.Format("{0}：“{1}”", name,dialog));

        }

        public void DialogPanel_Show(string dialog)
        {
            DialogPanel_Show();
            dialogPanel.SetText(String.Format("{0}", dialog));

        }

        public void DialogPanel_NextBtn(bool state = true)
        {
            dialogPanel.m_btnNext.gameObject.SetActive(state);


        }

        public void DialogPanel_Chose(bool state = true)
        {

            dialogPanel.m_btnNext.gameObject.SetActive(!state);
            dialogPanel.m_btnRight.gameObject.SetActive(state);
            dialogPanel.m_btnLeft.gameObject.SetActive(state);


        }
        #endregion




    }
}