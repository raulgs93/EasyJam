using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] float startTimeBetweenDelivery = 10f;
    [SerializeField] float timeBetweenDelivery = 0f;

    [SerializeField] PickUp foodCrate;
    [SerializeField] PickUp medsCrate;
    [SerializeField] PickUp waterCrate;

    [SerializeField] TextMeshProUGUI timer;

    [SerializeField] int score = 0;


    private void Start() {
        timeBetweenDelivery = startTimeBetweenDelivery;
    }

    private void Update() {
        timeBetweenDelivery -= Time.deltaTime;
        timer.text = GetCurrentDeliveryTime().ToString();
    }

    public void RequestFood() {
        if (CanDeliver()) {
            Instantiate(foodCrate, GameObject.FindGameObjectWithTag("Crates").transform);
            ResetTime();
        }
    }

    public void RequestMeds() {
        if (CanDeliver()) {
            Instantiate(medsCrate, GameObject.FindGameObjectWithTag("Crates").transform);
            ResetTime();
        }
    }
    public void RequestWater() {
        if (CanDeliver()) {
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
}
