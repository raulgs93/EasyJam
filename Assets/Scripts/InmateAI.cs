
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

        string[] names = {"Raul", "Galo", "Jordi", "Lee", "Jimmy", "Johnny", "Francesco", "Luigi", "Tanaka", "Rob", "Roy", "James", "Albert", "Whitey", "Billy", "Ricardo", "Adolf", "Carl", "Timmy", "Luca", "Dale", "Rick", "Morty", "Jason", "Rudolph", "Rex", "Stan", "Elvis", "Kane", "Sidney", "Ezequiel", "Tylor", "Aron", "Dashawn", "Devyn", "Mike", "Silas", "Jaiden", "Jayce", "Deonte", "Romeo", "Deon", "Cristopher", "Freddy", "Kurt", "Kolton", "River", "August", "Roderick", "Clarence", "Derick", "Jamar", "Raphael", "Rohan", "Kareem", "Muhammad", "Demarcus", "Sheldon", "Markus", "Cayden", "Luca", "Tre", "Jamison", "Jean", "Rory", "Brad", "Clinton", "Jaylan", "Titus", "Emiliano", "Jevon", "Julien", "Alonso", "Lamar", "Cordell", "Gordon", "Ignacio", "Jett", "Keon", "Baby", "Cruz", "Rashad", "Tariq", "Armani", "Deangelo", "Milton", "Geoffrey", "Elisha", "Moshe", "Bernard", "Asa", "Bret", "Darion", "Darnell", "Izaiah", "Irvin", "Jairo", "Howard", "Aldo", "Zechariah", "Ayden", "Garrison", "Norman", "Stuart", "Kellen", "Travon", "Shemar", "Dillan", "Junior", "Darrius", "Rhett", "Barry", "Kamron", "Jude", "Rigoberto", "Amari", "Jovan", "Octavio", "Perry", "Kole", "Misael", "Hassan", "Jaren", "Latrell", "Roland", "Quinten", "Ibrahim", "Justus", "German", "Gonzalo", "Nehemiah", "Forrest", "Mackenzie", "Anton", "Chaz", "Talon", "Guadalupe", "Austen", "Brooks", "Conrad", "Greyson", "Winston", "Antwan", "Dion", "Lincoln", "Leroy", "Earl", "Jaydon", "Landen", "Gunner", "Brenton", "Jefferson", "Fredrick", "Kurtis", "Maximillian", "Stephan", "Stone", "Shannon", "Shayne", "Karson", "Stephon", "Nestor", "Frankie", "Aurelius", "Avelardo", "Avishai", "Avontay", "Ayad", "Ayodeji", "Aza", "Azam", "Azarias", "Azlan", "Azzam", "Bartosz", "Bassam", "Baudel", "Bayler", "Bear", "Becker", "Bee", "Benedikt", "Benjimin", "Bevin", "Bishoy", "Bowdrie", "Bowie", "Braedin", "Braidyn", "Braijon", "Bralen", "Brandis", "Brantson", "Braxtin", "Braxtyn", "Brayn", "Brek", "Brelan", "Bren", "Brenndan", "Brennden", "Brentton", "Breton", "Brewster", "Breydan", "Breylan", "Briana", "Brinden", "Brit", "Broderic", "Brogen", "Broghan", "Bryam", "Brycin", "Brylon", "Buckley", "Bud", "Burl", "Byren", "Cailen", "Calixto", "Callaghan", "Calyn", "Camara", "Camaren", "Camiren", "Cardarius", "Carlee", "Carlie", "Carlisle", "Carlosantonio", "Carlous", "Carmeron", "Casanova", "Cashmere", "Castle", "Caton", "Caven", "Cayetano", "Cedarius", "Cedrik", "Cequan", "Cervando", "Cesareo", "Cevin", "Chalen", "Chalmer", "Chalmers", "Chancy", "Chang", "Channon", "Chao", "Chaquan", "Charlee", "Chatham", "Chavon", "Chevelle", "Chevez", "Chigozie", "Chipper", "Christipher", "Chukwubuikem", "Cicero", "Clancey", "Claudell", "Claudius", "Cleon", "Cloud", "Clovis", "Coletin", "Colum", "Cordarrius", "Corley", "Cormick", "Corran", "Cosimo", "Costas", "Coulson", "Coulton", "Couy", "Crandall", "Cristan", "Cristofher", "Cully", "Cyron", "Dacotah", "Daequon", "Daeron", "Daevin", "Dailan", "Daimeon", "Daimien", "Daiveon", "Daiyon", "Dajean", "Dajhon", "Dakhari", "Daleon", "Dalonta", "Damacio", "Damek", "Damere", "Damiano", "Damichael", "Damitri", "Danel", "Danh", "Danian", "Daniele", "Danley", "Dannyel", "Danon", "Danta", "Danylo", "Danzell", "Daquain", "Daquez", "Daquion", "Daqwon", "Darcy", "Darias", "Dariyan", "Darlyn", "Darold", "Darragh", "Darryle", "Darryll", "Darwyn", "Davell", "Daveonte", "Daveyon", "Davidanthony", "Davidlee", "Daykota", "Dayle", "Daz", "Dazhon", "Dazion", "Deaaron", "Deago", "Deaire", "Deandrick", "Deanta", "Dearies", "Deauntae", "Decklin", "Decorey", "Deejay", "Deen", "Deiontay", "Dekevion", "Delontae", "Delvion", "Delvis", "Delwin", "Demaje", "Demarlo", "Demetrus", "Demontrae", "Demorea", "Deng", "Deni", "Denise", "Dennie", "Dentrell", "Denys", "Deondrea", "Deone", "Deontra", "Dequantae", "Dequavion", "Derald", "Derien", "Deris", "Derrious", "Desani", "Deshown", "Detavius", "Deterrion", "Deuntae", "Devang", "Devansh", "Devantae", "Devere", "Devlon", "Devvon", "Dexton", "Dick", "Dickson", "Dilraj", "Dilynn", "Dimetri", "Dimetrius", "Dimitrious", "Dishan", "Django", "Djon", "Dmarkus", "Domanik", "Dondrell", "Donje", "Donnis", "Dontrez", "Donya", "Donzel", "Dovber", "Dragan", "Draper", "Dravon", "Drayson", "Drennan", "Dresean", "Drevan", "Dreyden", "Dreylon", "Duante", "Dubois", "Dwaine", "Eann", "Eathen", "Eaton", "Ebon", "Ebony", "Eean", "Egypt", "Ej", "Elante", "Elchonon", "Eliberto", "Elicio", "Elijio", "Elisah", "Eliut", "Elizah", "Elkin", "Ellian", "Elmar", "Elston", "Elwyn", "Emaan", "Emanuele", "Emigdio", "Emmanuell", "Equan", "Erie", "Erikson", "Eriverto", "Erlin", "Erminio", "Errin", "Eryn", "Estefano", "Ethanjames", "Eziah", "Famous", "Faruq", "Fernie", "Flynt", "Fong", "Frances", "Fraser", "Friedrich", "Furkan", "Gabrien", "Gadge", "Gael", "Gaelan", "Ganon", "Garik", "Garrion", "Gavinn", "Geoff", "Geovannie", "Geran", "Gerasimos", "Geric", "Gerod", "Gershom", "Gervais", "Ghassan", "Gill", "Gillian", "Girard", "Giulio", "Givanni", "Glynn", "Godswill", "Gohan", "Gonsalo", "Gryffin", "Guillaume", "Gurman", "Hagop", "Haidar", "Haleem", "Halid", "Halim", "Hamdi", "Hamish", "Hanan", "Harlee", "Harsha", "Harsimran", "Hartley", "Hassen", "Hayward", "Haziel", "Helder", "Henson", "Hiep", "Higinio", "Hillard", "Hiren", "Hobert", "Holdan", "Holdon", "Holdyn", "Honorio" };

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
