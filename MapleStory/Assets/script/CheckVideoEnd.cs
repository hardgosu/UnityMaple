using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CheckVideoEnd : MonoBehaviour
{
   public VideoPlayer intro;
   void Awake () 
   {
      intro = GetComponent<VideoPlayer>();
      intro.loopPointReached += OnMovieFinished; // loopPointReached is the event for the end of the video
   }

   void OnMovieFinished(VideoPlayer player)
   {
      Debug.Log("Event for movie end called");
      player.Stop();
      SceneManager.LoadScene("1_MenuScene");
   }
}
