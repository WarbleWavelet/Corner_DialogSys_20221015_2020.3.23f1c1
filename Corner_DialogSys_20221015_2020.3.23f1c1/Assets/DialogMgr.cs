/****************************************************
    文件：DialogMgr.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/15 23:58:47
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class DialogMgr : Singleton<DialogMgr>
    {
        #region 字属


        public string dialogDataFilePath="Data/Txt/Dialog";                     //不加后缀
        string[] dialogRows;                                                    //对话内容的每一行
        public List<Sprite> headLst = new List<Sprite>();                       //保存角色的所有头像
        Dictionary<string, Sprite> headDic = new Dictionary<string, Sprite>(); //保存角色名字和头像的对应关系




       public int m_tarId = 1;                                              //当前对话的ID
       /** [HideInInspector]//**/ public string m_tarNpc= "同龄人";         //按主线当前要找的npc
       /** [HideInInspector]//**/ public string m_speaker = "小白";         //每行说话的人
       /** [HideInInspector]//**/ public const string m_player = "小白";    //主角



        public GameObject optionButtonPrefab;       //选项按钮的预制体
        public Transform buttonGroup;               //选项按钮的父物体
        public float textSpeed = 0.01f;             //文字显示的速度
        bool cancelTyping = false;                  //有没有取消打字的方式显示
        bool textFinish = true;                     //文本有没有显示完成
        Role role = new Role();                     //主角的属性类
        //public TMP_Text growValueText;              //成长值属性的UI

        public AudioClip growClip;                  //成长值增加的音效


        MonoBehaviour m_GameStart;

        string dialogText = "";
        #endregion

        #region 生命
        public void InitMgr(MonoBehaviour m_GameStart)
        {
            this.m_GameStart = m_GameStart;//我喜欢写一样名字，改动方便
            ReadText();

            LoadAllSprite( headLst, "Textures/People/", "-Head");
            InitDic(headLst, headDic, "-Head");
           

        }



        #endregion

        #region 系统

        #endregion

        #region 辅助


        void Log()
        {

            Debug.LogFormat("m_speaker:{0}\tm_tarNpc:{1}\tm_curId:{2}", m_speaker,m_tarNpc,m_tarId );
        }


        public  void ShowDialogRow()         //用文件中的每一行显示对话框中的内容
        {
            /**
                标志|ID|人物|内容|NPC|跳转ID|效果|目标
                #|1|小白|你知道哪里有比较好的Unity视频教程吗？|同龄人|2||
                #|2|同龄人|SiKi学院就有啊|同龄人|3||
                #|3|小白|视频内容适合入门吗？|同龄人|4||
                #|4|同龄人|Unity入门、进阶都有，有学习路线，找SiKi客服要学习路线，根据学习路线学习|同龄人|5||
                #|5|小白|好的，谢谢！|同龄人|6|成长值@10|小白
                #|6|小白|老师，请问有Unity的学习路线吗？|SiKi客服|7||
                #|7|SiKi客服|https://zhuanlan.zhihu.com/p/540245002|SiKi客服|8|成长值@10|小白
                #|8|小白|SiKi学院的视频有免费的吗？|SiKi客服|9||
                #|9|SiKi客服|有免费的，也有收费的，收费需要加入A计划，你要加入A计划吗？|SiKi客服|10||
                &|10||加入|SiKi客服|12|成长值@30|小白
                &|11||不加入|SiKi客服|13|成长值@5|
                #|12|SiKi客服|加入A计划可以观看所属方向的所有收费课程以及有效期内更新的所有收费课程！|SiKi客服|13||
                #|13|SiKi客服|有问题可以QQ咨询客服喔，QQ:88137944，接下来你可以先找小Y老师学习|小Y老师|14|成长值@10|小白
                #|14|小Y老师|来认识Unity的吗？|小Y老师|15||
                #|15|小白|我想了解Unity的历史、Unity的职位和Unity的安装等信息|小Y老师|16||
                #|16|小Y老师|恭喜你初步认识Unity！可以再去找Mono老师学习|Mono老师|17|成长值@10|小白
                #|17|Mono老师|来学习C#编程语言吗？|Mono老师|18||
                #|18|小白|我想学C#语言基础、面向对象编程和C#高级编程的知识|Mono老师|19||
                #|19|Mono老师|恭喜你完成C#编程语言的学习！可以再去找稀粥老师学习|稀粥老师|20|成长值@10|小白
                #|20|稀粥老师|来学习Unity引擎知识吗？|稀粥老师|21||
                #|21|小白|我想学习Unity引擎界面、常用组件、UGUI、动画系统、物理系统、Shder等等|稀粥老师|22||
                #|22|稀粥老师|恭喜你完成Unity引擎的学习！可以再去找Trigger老师学习|Trigger老师|23|成长值@10|小白
                #|23|Trigger老师|来学习Unity项目知识吗?|Trigger老师|24||
                #|24|小白|2D、3D、VR和AR项目知识我都想学|Trigger老师|25||
                #|25|Trigger老师|恭喜你完成Unity的实战练习！可以再去找SiKi老师学习|SiKi老师|26|成长值@10|小白
                #|26|SiKi老师|你已经可以工作了，还想学习吗？|SiKi老师|27||
                #|27|小白|还有吗？|SiKi老师|28||
                #|28|SiKi老师|最后一阶段是Unity高级图形学，会有点难|SiKi老师|29||
                #|29|小白|我很感兴趣，不怕难|SiKi老师|30||
                #|30|SiKi老师|加油！SiKi学院时刻欢迎你！|SiKi老师|31||
                END|31||||||
            **/
            if (m_tarId >= dialogRows.Length)//测试时由外界控制m_curId
            {
                m_tarId = dialogRows.Length - 1;
                return;
            }
            List<int> optIdLst = new List<int>();
            for (int i = 0; i < dialogRows.Length; i++)     //遍历每一行中的内容
            {
                if (m_tarId != i) //m_curId初始=1，跳掉标题行
                {
                    continue;
                }
                string[] cells = dialogRows[i].Split('|');  //读取每一行用逗号分割的内容
                string tag = cells[0];
                int curId = int.Parse(cells[1]);
                if (tag == "END")
                {
                    Debug.Log("END");
                    UIMgr.Instance.DialogPanel_Show(false);
                    return;
                }
                else
                { 
                    string speaker = cells[2];     //说话的
                    string dialog = cells[3];       //话
                    string nxtNpc = cells[4];       //tarNpc
                    int nxtId=int.Parse(cells[5]);
                    string eft = cells[6];      //作用
                    string eftTar = cells[7];   //作用对象
                    if (curId != m_tarId && (speaker!=m_tarNpc || speaker!=m_player))//还没到说这句话的时候，或人
                    {
                        return;
                    }
                    if (tag == "#")
                    {
                        if(speaker==m_tarNpc)
                        UIMgr.Instance.DialogPanel_Dialog(headDic[speaker]);
                        UpdateText(speaker, dialog);
                        m_speaker = speaker;
                        m_tarId = nxtId; //更新当前的对话索引
                        m_tarNpc = nxtNpc;
                        Log();
                        return;
                    }
                    else if (tag == "&")
                    {
                        Debug.Log("&");
                        string[] opt1 = cells;
                        string[] opt2 = dialogRows[i+1].Split('|');
                        UIMgr.Instance.DialogPanel_Chose(true, opt1[3], opt2[3],
                            () => {
                                // 选项时speaker不变
                                m_tarNpc = nxtNpc; //选项中人名不可能是主角，所以不判断
                                m_tarId = nxtId;
                                 OnNextBtnClick();
                                UIMgr.Instance.DialogPanel_Chose(false);
                            },
                            () => {
                                // 选项时speaker不变
                                m_tarNpc = opt2[4];
                                m_tarId =int.Parse( opt2[5]);
                                OnNextBtnClick();
                                UIMgr.Instance.DialogPanel_Chose(false); 
                            });
                        i++; //跳过
                        Log();
                        return;
                    }
                }
            }
        }




        public void OnNextBtnClick() //显示下一个对话内容按钮被点击
        {
            Log();
            if (m_speaker == m_tarNpc || m_speaker==m_player) //对应的npc说话||本人说话
            {
                ShowDialogRow();
            }
            else
            {
                UIMgr.Instance.DialogPanel_Show(false);
            }
        }               
           void UpdateText(string name, string text) //更新对话框的文本和头像
           {
              
               if (textFinish == true && cancelTyping == false)  //如果上一个文本显示完成，没有取消打字显示
               {
                   m_GameStart.StartCoroutine(SetTextUI(text));//使用协程来显示文字，文字会像打字的方式一样出现
               }
               else if (textFinish == false && cancelTyping == false)
               {
                   cancelTyping = true;
               }

           }               
        
        IEnumerator SetTextUI(string text)
           {
               textFinish = false;
               dialogText = "";
               UIMgr.Instance.DialogPanel_Show(dialogText); //清空文本框中的内容
               int letter = 0;
               while (!cancelTyping && letter < text.Length)
               {   
                   dialogText+= text[letter];
                   UIMgr.Instance.DialogPanel_Show(dialogText); 
                   letter++;
                   yield return new WaitForSeconds(textSpeed);
               }      
               dialogText =text ;
               UIMgr.Instance.DialogPanel_Show(dialogText);  //如果取消打字输入，直接给对话框赋值
               cancelTyping = false;
               textFinish = true;
           }
        /**



          


          
           void GenerateOptionButton(int index)   //产生分支的选项按钮
           {
              
               string[] cells = dialogRows[index].Split(',');   //读取index所在行的内容
               if (cells[0] == "&")
               {
                   GameObject btn = Instantiate(optionButtonPrefab, buttonGroup);
                   btn.GetComponentInChildren<TMP_Text>().text = cells[3];
                   btn.GetComponent<Button>().onClick.AddListener(
                       delegate
                       {
                           OnOptionClick(int.Parse(cells[5]));
                           if (cells[6] != "")
                           {
                               string[] effect = cells[6].Split('@');
                               ShowEffectAttri(effect[0], int.Parse(effect[1]));
                           }
                       });
                   GenerateOptionButton(index + 1);
               }
           }

          
           void OnOptionClick(int index)  //选项按钮的点击事件
           {
               curDialogIndex = index; //更新当前的对话索引
               ShowDialogRow(); //显示最新的对话内容
                              
               for (int i = 0; i < buttonGroup.childCount; i++)    //销毁选项按钮
               {
                    Destroy(buttonGroup.GetChild(i).gameObject);
               }
           }

           //显示效果属性值
           void ShowEffectAttri(string effectName, int param)
           {
               if (effectName == "成长值")
               {
                   role.growValue += param;
                   growValueText.text = "成长值：" + role.growValue.ToString();
                   AudioManager.instance.PlayAudio(growClip);
               }
           }
           //**/



        /// <summary>
        ///  到 path下加载所有名字有nameContains的sprite，放到headLst
        /// </summary>
        /// <param name="headLst"></param>
        /// <param name="path"></param>
        /// <param name="nameContains"></param>
        void LoadAllSprite(List<Sprite> headLst, string path, string nameContains)
        {
            List <Sprite>  tmpLst = new List<Sprite>();
            tmpLst.AddRange(Resources.LoadAll<Sprite>(path));
            foreach (Sprite item in tmpLst)
            {
                if (item.name.Contains(nameContains))
                {
                    headLst.Add(item);
                }
            }
        }


        /// <summary>
        ///  headLst中，name去掉subName作为key，值作为value，放到 headDic
        /// </summary>
        /// <param name="headLst"></param>
        /// <param name="headDic"></param>
        /// <param name="subName"></param>
        void InitDic(List<Sprite> headLst, Dictionary<string, Sprite> headDic, string subName)
        {

            foreach (var item in headLst)
            {
                string key = item.name.Replace(subName, ""); //小白-Head => 小白
                headDic.Add(key, item);
            }
        }


        void ReadText()//读取文件中的文本
        {

            TextAsset dialogDataFile = Resources.Load<TextAsset>(dialogDataFilePath);
            dialogRows = dialogDataFile.text.Split('\n');
        }
        #endregion




    }


     public class Role
    {
    public string name; 
    public int growValue; 
}
}