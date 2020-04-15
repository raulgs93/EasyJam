using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    [SerializeField] float startTimeBetweenDelivery = 10f;
    [SerializeField] float timeBetweenDelivery = 0f;

    [SerializeField] PickUp foodCrate;
    [SerializeField] PickUp medsCrate;
    [SerializeField] PickUp waterCrate;

    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI scoreValue;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject hud;

    [SerializeField] float minX;
    [SerializeField] float minY;
    [SerializeField] float maxX;
    [SerializeField] float maxY;

    [SerializeField] int score = 0;

    InmateAI[] inmates;


    private void Start() {
        timeBetweenDelivery = startTimeBetweenDelivery;
        inmates = FindObjectsOfType<InmateAI>();
    }

    private void Update() {
        timeBetweenDelivery -= Time.deltaTime;
        timer.text = String.Format("{0:0}", GetCurrentDeliveryTime());
    }

    public void RequestFood() {
        if (CanDeliver()) {
            FindObjectOfType<SoundManager>().PlaySound("newDelivery");
            Instantiate(foodCrate, GetRandomSpawn(), Quaternion.identity);
            ResetTime();
        }
    }

    public void RequestMeds() {
        if (CanDeliver()) {
            FindObjectOfType<SoundManager>().PlaySound("newDelivery");
            Instantiate(medsCrate, GetRandomSpawn(), Quaternion.identity);
            ResetTime();
        }
    }
    public void RequestWater() {
        if (CanDeliver()) {
            FindObjectOfType<SoundManager>().PlaySound("newDelivery");
            Instantiate(waterCrate, GetRandomSpawn(), Quaternion.identity);
            ResetTime();
        }
    }

    bool CanDeliver() {
        return timeBetweenDelivery <= 0;
    }

    public float GetCurrentDeliveryTime() {
        return Mathf.Clamp(timeBetweenDelivery, 0, startTimeBetweenDelivery);
    }

    private void ResetTime() {
        timeBetweenDelivery = startTimeBetweenDelivery;
    }

    public void AddScore(int amount) {
        score += amount;
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
    }

    public void GameOver() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMover>().DisableControl();
        scoreValue.SetText(score.ToString());
        gameOverScreen.SetActive(true);
        hud.SetActive(false);

    }

    public void RemoveInmate(InmateAI inmate) {
        List<InmateAI> list = new System.Collections.Generic.List<InmateAI>(inmates);
        list.Remove(inmate);
        inmates = list.ToArray();

        if (inmates.Length == 0) {
            GameOver();
        }

        foreach (InmateAI inm in inmates) {
            inm.AddDeathPenalty();
        }
    }

    private Vector3 GetRandomSpawn() {
        float x = UnityEngine.Random.Range(minX, maxX);
        float y = UnityEngine.Random.Range(minY, maxY);

        return new Vector3(x, y, 0);
    }
}
