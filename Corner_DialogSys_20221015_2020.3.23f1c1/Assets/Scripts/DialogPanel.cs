/****************************************************
    文件：DialogPanel.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/16 1:22:28
	功能：会话面板
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class DialogPanel : MonoBehaviour
    {
        #region 字段
        public Image m_image;
        public TextMeshProUGUI m_tmp; //用这类型
        public Button m_btnLeft; 
        public Button m_btnRight; 
        public Button m_btnNext; 

        #endregion

        #region 生命
        private void Awake()
        {
            m_image = transform.Find("LeftPin/Image").GetComponent<Image>();
            m_tmp = transform.Find("RightTopPin/Tmp").GetComponent<TextMeshProUGUI>();
            m_btnLeft = transform.Find("RightBottomPin/ButtonLeft").GetComponent<Button>();
            m_btnRight = transform.Find("RightBottomPin/ButtonRight").GetComponent<Button>();
            m_btnNext = transform.Find("RightBottomPin/ButtonNext").GetComponent<Button>();
        }
        void Start()
        {
            
        }
        
        void Update()
        {
            
        }
        #endregion

        #region 系统

        #endregion

        #region 辅助
        public void SetText(string str)
        {
            m_tmp.text = str;
        }


        public void SetHead(Sprite sprite)
        {
            m_image.sprite = sprite;
        }

        public void SetLeftBtn(bool state=true, string str="否")
        {
            m_btnLeft.gameObject.SetActive(state);
            m_btnLeft.GetComponent<TextMeshProUGUI>().text = str;
        }

        public void SetRightBtn(bool state = true, string str = "是")
        {
            m_btnRight.gameObject.SetActive(state);
            m_btnRight.GetComponent<TextMeshProUGUI>().text = str;
        }

        public void SetLeftBtn(bool state = true)
        {
            m_btnNext.gameObject.SetActive(state);
        }
        #endregion
    }
}