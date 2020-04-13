
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmateAI : MonoBehaviour
{

    [SerializeField] float health = 100f;
    [SerializeField] float baseHealthMultiplier = 0f;
    [SerializeField] float hunger = 100f;
    [SerializeField] float hungerMultiplier = 1f;
    [SerializeField] float thirst = 100f;
    [SerializeField] float thirstMultiplier = 1f;
    [SerializeField] float anger = 100f;

    [SerializeField] float healthMultiplierIncrement = 0.5f;

    [SerializeField] float speed = 2.5f;
    [SerializeField] Transform[] wanderSpots;
    [SerializeField] float startwaitTime = 4f;

    [SerializeField] SpriteRenderer foodIndicator;
    [SerializeField] SpriteRenderer medsIndicator;
    [SerializeField] SpriteRenderer waterIndicator;

    [SerializeField] float foodThreshold = 50f;
    [SerializeField] float medsThreshold = 50f;
    [SerializeField] float waterThreshold = 50f;

    [SerializeField] int scoreXSecond = 10;

    private UIController ui;


    float healthMultiplier;
    float waitTime;
    int targetSpot;
    bool isAlive = true;

    private void Start() {
        ui = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        healthMultiplier = baseHealthMultiplier;
        GetRandomSpot();
        InvokeRepeating("AddScore", 1f, 1f);
    }

    private void Update() {
        Wander();
        DecreaseHunger();
        DecreaseThirst();
        DisplayNeeds();
    }

    private void AddScore() {
        ui.AddScore(Mathf.RoundToInt(scoreXSecond));
    }

    private void DisplayNeeds() {
        if (hunger <= foodThreshold) {
            EnableIndicator(foodIndicator);
        }
        else {
            DisableIndicator(foodIndicator);
        }
        if (health <= medsThreshold) {
            EnableIndicator(medsIndicator);
        }
        else {
            DisableIndicator(medsIndicator);
        }
        if (thirst <= waterThreshold) {
            EnableIndicator(waterIndicator);
        }
        else {
            DisableIndicator(waterIndicator);
        }
    }

    private void Wander() {


        transform.position = Vector2.MoveTowards(transform.position, wanderSpots[targetSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wanderSpots[targetSpot].position) < .2f ) {
            if (waitTime <= 0) {
                GetRandomSpot();
            }
            else {
                waitTime -= Time.deltaTime;
            }
            

        }
    }

    private void DecreaseHunger() {
        hunger -= hungerMultiplier * Time.deltaTime;
        if (hunger < foodThreshold) {
            DecreaseHealth();
        }
    }

    private void DecreaseThirst() {
        thirst -= thirstMultiplier * Time.deltaTime;
        if (thirst < waterThreshold) {
            DecreaseHealth();
        }
    }

    private void DecreaseHealth() {
        health -= healthMultiplier * Time.deltaTime;

        if (health<=0 && isAlive) {
            Die();
        }

    }

    private void Die() {

        FindObjectOfType<SoundManager>().PlaySound("death");
        transform.Rotate(0,0,90);
        isAlive = false;
        ui.RemoveInmate(this);
        Destroy(GetComponent<Animator>());
        Destroy(foodIndicator);
        Destroy(medsIndicator);
        Destroy(waterIndicator);
        Destroy(this);
    }

    private void GetRandomSpot() {

        waitTime = startwaitTime;
        targetSpot = UnityEngine.Random.Range(0, wanderSpots.Length);
    }

    private void DisableIndicator(SpriteRenderer indicator) {
        indicator.enabled = false;
    }
    private void EnableIndicator(SpriteRenderer indicator) {
        indicator.enabled = true;
    }

    public float GetHealth() {
        return health;
    }

    public float GetHunger() {
        return hunger;
    }

    public float GetThirst() {
        return thirst;
    }

    public float GetAnger() {
        return anger;
    }

    public void AddHealth(float amount) {
        health += amount;
    }

    public void AddHunger(float amount) {
        hunger += amount;
    }

    public void AddThirst(float amount) {
        thirst += amount;
    }

    public void AddAnger(float amount) {
        anger += amount;
    }

}
