/****************************************************
    文件：NPC.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/15 23:30:17
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class NPC : MonoBehaviour
    {
        #region 字段


        #endregion

        #region 生命
        void Start()
        {
            
        }
        
        void Update()
        {
            
        }
        #endregion

        #region 系统
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
               //DialogMgr.Instance.ShowDialogRow();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                UIMgr.Instance.DialogPanel_Show(false);
            }
        }
        #endregion 

        #region 辅助

        #endregion
    }
}