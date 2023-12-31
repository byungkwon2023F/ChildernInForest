/*
  * - Name: BrushYourTeeth_Virus2.cs
  *
  * - Content:
  * Bacteria 2 setup script
  * Set how many times to touch to kill
  * Set animation when touching and dying
  *
  * -Revision History-
  * 2021-07-07: Production completed
  * 2021-07-16: File encoding fix
  * 2021-07-20: TTS function added
  *
  * - Variable
  * mg_NumberOfVirusLeft: Object for connection to ControlUI.cs
  * man_OnClick: Variable for saving animation when clicked
  * man_Virus2_Die: Animation storage variable when dying
  * mn_Virus2_HP: Virus HP setting variable
  * mb_CheckFlag: Flag to prevent the count from increasing when touched during a dying animation.
  * vm: Object connection that handles voice TTS
  *
  * -Function()
  * OnMouseDown(): Function that operates when a virus is clicked
  *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushYourTeeth_Virus2 : MonoBehaviour
{
    GameObject mg_NumberOfVirusLeft;                                                        // ControlUI.cs에 연결을 위한 오브젝트

    public Animator man_OnClick;                                                            // 애니메이션
    public Animator man_Virus2_Die;                                                         // 애니메이션

    VoiceManager vm;                                                                        // 음성(TTS) 오브젝트 연결을 위한 변수

    private int mn_Virus2_HP = 3;                                                           // 세균 몇번 터치하면 없어질건지 설정하는 부분

    private bool mb_CheckFlag;                                                              // 죽는 애니메이션도중 터치시 카운트 올라가는것을 방지하기 위한 flag


    void Start()
    {
        this.mg_NumberOfVirusLeft = GameObject.Find("NumberOfVirusLeft");                   // 오브젝트 연결
        this.vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();             // 오브젝트 연결

        mb_CheckFlag = false;                                                               // false로 초기화
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (mn_Virus2_HP == 0)                                                              // 세균의 HP가 0이되어 죽는경우 설정
        {
            if (mb_CheckFlag == false)                                                      // flag를 두어 세균이 죽으면 한번만 작동하도록 설정
            {
                mb_CheckFlag = true;
                mg_NumberOfVirusLeft.GetComponent<BrushYourTeeth_ControlUI>().v_MinusVirus();   // 남은 세균 수 감소
            }
            man_Virus2_Die.SetTrigger("Virus2_Die");                                        // 죽는 에니메이션
            vm.playVoice(1);                                                                // 죽을때 음성
            Destroy(gameObject, 1f);                                                        // 오브젝트 삭제
        }
        else                                                                                // 세균을 터치하여 HP감소
        {
            man_OnClick.SetTrigger("OnClick");                                              // 클릭시 애니메이션 작동
            mn_Virus2_HP -= 1;
            Debug.Log("바이러스2 클릭성공");
            vm.playVoice(3);                                                                // 클릭시 음성 작동
        }
    }
}
