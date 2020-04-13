using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip death, newDelivery, select, powerUp;
    
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string clip) {

        switch (clip) {

            case "death":
                audioSource.PlayOneShot(death);
                break;
            case "newDelivery":
                audioSource.PlayOneShot(newDelivery);
                break;
            case "select":
                audioSource.PlayOneShot(select);
                break;
            case "powerUp":
                audioSource.PlayOneShot(powerUp);
                break;


            default:
                break;
        }
    }
}
