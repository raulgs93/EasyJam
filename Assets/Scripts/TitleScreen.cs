using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
   

    public void PlayGame() {
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    public void ClickSound() {
        GameObject.FindObjectOfType<SoundManager>().PlaySound("select");
    }


}
