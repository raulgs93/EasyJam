
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InmateAI : MonoBehaviour
{

    [SerializeField] bool isReal = true;
    [SerializeField] bool hasName = true;

    [SerializeField] float health = 100f;
    [SerializeField] float baseHealthMultiplier = 0f;
    [SerializeField] float hunger = 100f;
    [SerializeField] float hungerMultiplier = 1f;
    [SerializeField] float thirst = 100f;
    [SerializeField] float thirstMultiplier = 1f;
    [SerializeField] float anger = 100f;

    [SerializeField] float statRandomizerValue = 25;
    
    [SerializeField] float speed = 2.5f;
    [SerializeField] Transform[] wanderSpots;
    [SerializeField] float startwaitTime = 4f;

    [SerializeField] SpriteRenderer foodIndicator = null;
    [SerializeField] SpriteRenderer medsIndicator= null;
    [SerializeField] SpriteRenderer waterIndicator = null;

    [SerializeField] float foodDisplayThreshold = 75f;
    [SerializeField] float medsDisplayThreshold = 75f;
    [SerializeField] float waterDisplayThreshold = 75f;

    [SerializeField] float foodThreshold = 50f;
    [SerializeField] float waterThreshold = 50f;

    [SerializeField] float deathPenalty = 1;
    [SerializeField] float deathPenaltyIncrement = 0.5f;

    [SerializeField] int scoreXSecond = 10;

    private UIController ui;

    [SerializeField] TextMeshPro nameText = null;

    float healthMultiplier;
    float waitTime;
    int targetSpot;
    bool isAlive = true;

    private void Start() {

        if (isReal) {
            ui = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
            healthMultiplier = baseHealthMultiplier;
            InvokeRepeating("AddScore", 1f, 1f);
            RandomizeStats();
        }
        GetRandomSpot();
        if (hasName) {
            nameText.text = GetRandomName();
        }
    }

    private void Update() {
        Wander();

        if (isReal) {
            DecreaseHunger();
            DecreaseThirst();
        }

        DisplayNeeds();

    }

    
    private void AddScore() {
        ui.AddScore(Mathf.RoundToInt(scoreXSecond));
    }
    

    private void DisplayNeeds() {
        if (hunger <= foodDisplayThreshold) {
            EnableIndicator(foodIndicator);
        }
        else {
            DisableIndicator(foodIndicator);
        }
        if (health <= medsDisplayThreshold) {
            EnableIndicator(medsIndicator);
        }
        else {
            DisableIndicator(medsIndicator);
        }
        if (thirst <= waterDisplayThreshold) {
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

    internal void AddDeathPenalty() {
        deathPenalty += deathPenaltyIncrement;
    }

    private void DecreaseHunger() {
        hunger -= hungerMultiplier * Time.deltaTime * deathPenalty;
        if (hunger < foodThreshold) {
            DecreaseHealth();
        }
    }

    private void DecreaseThirst() {
        thirst -= thirstMultiplier * Time.deltaTime * deathPenalty;
        if (thirst < waterThreshold) {
            DecreaseHealth();
        }
    }

    private void DecreaseHealth() {
        health -= healthMultiplier * Time.deltaTime * (deathPenalty/2) ;

        if (health<=0 && isAlive) {
            Die();
        }

    }

    private void Die() {

        FindObjectOfType<SoundManager>().PlaySound("death");
        transform.Rotate(0,0,90);
        isAlive = false;
        nameText.text = "";
        ui.RemoveInmate(this);
        Destroy(GetComponent<Animator>());
        Destroy(foodIndicator);
        Destroy(medsIndicator);
        Destroy(waterIndicator);
        Destroy(this);
    }

    private void RandomizeStats() {
        health += UnityEngine.Random.Range(-statRandomizerValue, statRandomizerValue);
        hunger += UnityEngine.Random.Range(-statRandomizerValue, statRandomizerValue);
        thirst += UnityEngine.Random.Range(-statRandomizerValue, statRandomizerValue);
        anger += UnityEngine.Random.Range(-statRandomizerValue, statRandomizerValue);
    }

    private string GetRandomName() {

        string[] names = {"Raul", "Galo", "Jordi", "Lee", "Jimmy", "Johnny", "Francesco", "Luigi", "Tanaka", "Rob", "Roy", "James", "Albert", "Whitey", "Billy", "Ricardo", "Adolf", "Carl", "Timmy", "Luca", "Dale", "Rick", "Morty", "Jason", "Rudolph", "Rex", "Stan"};

        return names[UnityEngine.Random.Range(0, names.Length)];
    }

    private void GetRandomSpot() {

        waitTime = UnityEngine.Random.Range(1,startwaitTime);
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
