using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainWorld : MonoBehaviour
{
   void Awake()
   {
     SceneManager.LoadScene("MainWorld Temon" , LoadSceneMode.Single) ;
   }

}
