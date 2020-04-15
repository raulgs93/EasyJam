using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
   

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void ClickSound() {
        print("sound should be listened");
        GameObject.FindObjectOfType<SoundManager>().PlaySound("select");
    }


}
