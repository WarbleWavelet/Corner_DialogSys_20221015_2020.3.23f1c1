/****************************************************
    文件：People.cs
	作者：lenovo
    邮箱: 
    日期：2022/10/15 16:12:57
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
namespace Demo0_0
{
    public class Player : MonoBehaviour
    {
        public float speed = 10;                        //主角移动的速度
        float moveX;
        float moveY;

        Rigidbody2D rig;                                //主角的刚体
        Animator anim;                                  //主角的动画控制器
        Vector2 lookDirection = new Vector2(0, -1);     //主角默认朝下
        public AudioClip moveClip;                      //主角移动的音效
        public AudioSource audioSource;                 //音频源组件

        [Header("静止时的图")]
        public Sprite down;
        public Sprite up;
        public Sprite left;
        public Sprite right;

        void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
             moveX = Input.GetAxisRaw("Horizontal"); //返回水平的虚拟轴值//A:-1  D:1  0
             moveY = Input.GetAxisRaw("Vertical"); //返回垂直的虚拟轴值//W:1  S:-1  0

            if (moveX == 0 && moveY == 0)   //让静止时图片为最后一次运动方向的默认图
            {
                Dir dir = Dir.Down;
                if (anim.GetFloat("LookX") != 0f)
                {
                    dir = anim.GetFloat("LookX") < 0 ?  Dir.Left : Dir.Right;
                }
                else
                {
                    dir = anim.GetFloat("LookY") < 0 ?  Dir.Down : Dir.Up;  
                }

                SpriteRenderer sr = GetComponent<SpriteRenderer>();  
                anim.enabled = false;

                switch ( dir )
                {
                    case Dir.Up :
                        {
                             sr.sprite = up;
                        }
                        break;
                    case Dir.Left:
                        {
                            sr.sprite = left;
                        }
                        break;
                    case Dir.Right:
                        {
                            sr.sprite = right;
                        }
                        break;
                    case Dir.Down:
                    default:
                        {
                            sr.sprite = down;
                        }
                        break;
                }
                return;
            }
            else
            { 
                anim.enabled = true;
                Vector2 moveVector2 = new Vector2(moveX, moveY);
                Vector2 pos = rig.position; //保存当前刚体的位置
                pos += moveVector2 * speed * Time.deltaTime;
                rig.MovePosition(pos);//主角的移动
           
                if (moveVector2.x != 0 || moveVector2.y != 0)  //主角的动画
                {
                    lookDirection = moveVector2;
                    if (audioSource.isPlaying == false)
                        audioSource.Play();
                }
                anim.SetFloat("LookX", lookDirection.x);
                anim.SetFloat("LookY", lookDirection.y);
                anim.SetFloat("Speed", moveVector2.magnitude); //用向量的长度当成速度            
            }


        }
    }

    public enum Dir
    {
        Down,
        Up,
        Left,
        Right

    }
}