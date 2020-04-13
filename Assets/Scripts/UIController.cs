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

    [SerializeField] int score = 0;

    InmateAI[] inmates;


    private void Start() {
        timeBetweenDelivery = startTimeBetweenDelivery;
        inmates = FindObjectsOfType<InmateAI>();
    }

    private void Update() {
        timeBetweenDelivery -= Time.deltaTime;
        timer.text = GetCurrentDeliveryTime().ToString();
    }

    public void RequestFood() {
        if (CanDeliver()) {
            FindObjectOfType<SoundManager>().PlaySound("newDelivery");
            Instantiate(foodCrate, GameObject.FindGameObjectWithTag("Crates").transform);
            ResetTime();
        }
    }

    public void RequestMeds() {
        if (CanDeliver()) {
            FindObjectOfType<SoundManager>().PlaySound("newDelivery");
            Instantiate(medsCrate, GameObject.FindGameObjectWithTag("Crates").transform);
            ResetTime();
        }
    }
    public void RequestWater() {
        if (CanDeliver()) {
            FindObjectOfType<SoundManager>().PlaySound("newDelivery");
            Instantiate(waterCrate, GameObject.FindGameObjectWithTag("Crates").transform);
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

        scoreValue.SetText(score.ToString());
        gameOverScreen.SetActive(true);

    }

    public void RemoveInmate(InmateAI inmate) {
        List<InmateAI> list = new System.Collections.Generic.List<InmateAI>(inmates);
        list.Remove(inmate);
        inmates = list.ToArray();

        if (inmates.Length == 0) {
            GameOver();
        }
    }
}
