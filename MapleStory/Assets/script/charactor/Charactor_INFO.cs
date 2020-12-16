using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Charactor_INFO : MonoBehaviour
{
   public TextMeshProUGUI Char_name;
   public int Char_level;
   public TextMeshProUGUI Name, Job, HP, MP, AP, STR, DEX, INT;
   
   
   private void Awake()
   {
      if (GameObject.Find("Charactor_INFO") != null)
      {
         var charactor_info = GameObject.Find("Charactor_INFO").GetComponent<CharactorStat>();
         Char_name.text = charactor_info.Char_name;
         ////////스텟창///////
         Name.text = charactor_info.Char_name;
         if(charactor_info.Char_job==1)
            Job.text = "전사";
         else 
            Job.text = "마법사";

         HP.text = 100.ToString();
         MP.text = 100.ToString();
         AP.text = 100.ToString();

         STR.text = charactor_info.CharStat.STR.ToString();
         DEX.text = charactor_info.CharStat.DEX.ToString();
         INT.text = charactor_info.CharStat.INT.ToString();



         ////////////////////

      }
      
   }
   
}
