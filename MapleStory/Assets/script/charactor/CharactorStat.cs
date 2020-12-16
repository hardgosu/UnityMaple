using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public struct CHARSTAT
{
   public int STR;
   public int DEX;
   public int INT;
}
public class CharactorStat : MonoBehaviour
{
   public TextMeshProUGUI NameBox;
 
   public string Char_name;
   public int Char_job;
   public CHARSTAT CharStat;

   public Text Text_STR,Tex_DEX,Text_int;
   public void Awake()
   {
      CharStat.STR = Random.Range(1, 9);
      Text_STR.text= CharStat.STR.ToString();
      CharStat.DEX = Random.Range(1, 9);
      Tex_DEX.text= CharStat.DEX.ToString();
      CharStat.INT = Random.Range(1, 9);
      Text_int.text= CharStat.INT.ToString();
      
     
   }

   public void Onclick()
   {
      if(NameBox.text!=null)  Char_name = NameBox.text;
      if(GameObject.Find("JobManager")!=null)  Char_job= GameObject.Find("JobManager").GetComponent<JobManager>().JobNum;
      
  
   }

   public void SetDIce()
   {
      CharStat.STR = Random.Range(1, 9);
      Text_STR.text= CharStat.STR.ToString();
      CharStat.DEX = Random.Range(1, 9);
      Tex_DEX.text= CharStat.DEX.ToString();
      CharStat.INT = Random.Range(1, 9);
      Text_int.text= CharStat.INT.ToString();
      
   }

   public void CharLog()
   {
      Debug.Log(Char_name+"+"+Char_job+"+"+CharStat.STR+"+"+CharStat.DEX+"+"+CharStat.INT);
   }
   
}
