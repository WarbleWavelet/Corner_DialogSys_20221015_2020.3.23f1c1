/****************************************************
    文件：NPC_TMP.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/15 23:0:54
	功能：控制NPC头上的TextMeshPro
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class NPC_TMP : MonoBehaviour
    {
        #region 字段
        MeshRenderer mr;

        #endregion

        #region 生命
        void Start()
        {
             mr= GetComponent<MeshRenderer>();
            SpriteRenderer parentSr = transform.parent.gameObject.GetComponent<SpriteRenderer>();
            mr.sortingLayerName = parentSr.sortingLayerName;
            mr.sortingOrder = parentSr.sortingOrder;
        }
        
        void Update()
        {
            
        }
        #endregion 

        #region 系统

        #endregion 

        #region 辅助

        #endregion
    }
}