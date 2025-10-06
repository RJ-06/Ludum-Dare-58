using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //TODO: The first pause doesn't work, calls it twice haven't figured it out yet (likely to do with push vs release)
    public static bool paused = false;
    
    public GameObject menu;

    PauseSystem action;


    //These functions enable input 
    private void Awake(){
        action = new PauseSystem();
    }

    private void OnEnable(){
        action.Enable();
    }

    private void OnDisable(){
        action.Disable();
    }

    private void Start(){
        action.Game.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause(){
        if(paused){
            Debug.Log("resume");
            Debug.Log(paused);
            ResumeGame();
            
        }else{
            Debug.Log("pause");
            Debug.Log(paused);
            PauseGame();
        }
    }
    public void PauseGame(){
        Time.timeScale = 0f;
        paused = true;
        menu.SetActive(true);
        Debug.Log("hey im paused");
        Debug.Log(paused);
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        paused = false;
        menu.SetActive(false);
        Debug.Log("unpaused");
        Debug.Log(paused);
    }
}
