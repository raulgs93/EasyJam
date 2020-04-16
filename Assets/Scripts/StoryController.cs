using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    [SerializeField] float readTime = 5f;

    IEnumerator Start()
    {

        yield return new WaitForSeconds(readTime);
        FindObjectOfType<LevelLoader>().LoadNextLevel();

    }

    
}
