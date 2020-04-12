using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentObjectSpawner : MonoBehaviour
{

    [SerializeField] GameObject persistentPrefab;

    public static bool hasSpawned = false;

    private void Awake() {
        if (hasSpawned) { return; }

        SpawnPersistentObjects();
        hasSpawned = true;

    }

    private void SpawnPersistentObjects() {

        GameObject persistentObject = Instantiate(persistentPrefab);
        DontDestroyOnLoad(persistentObject);

    }
}
