using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Wind : MonoBehaviour
{
    public bool bossFight = false;
    public Rigidbody playerRb;
    public Vector3 spawnPosition;
    public Vector3 windVect;
    public Vector3 windVectL;
    private Vector3 baseVect;
    private Vector3 baseVectL;
    private float prevPosX;
    public float windSpeed;
    public float rainForce;
    public float tornadoCooldown = 0f;
    public float tornadoOverdrive = 0f;
    public bool tornadoActive = false;
    public float windMod = .75f;
    public int tornado = 0;
    public int lastTornado = -1;
    public ParticleSystem rainEffect;
    public ParticleSystem electrocution;
    public TextMeshProUGUI stormText;
    public TextMeshProUGUI timeText;
    private float stormMax = 50f;
    public int state = 0;
    public GameObject crosshair;
    public GameObject lightning;
    public GameObject clearText;
    private float lightningStrike = 0f;
    public bool Electrocuted = false;
    public bool raining = false;
    public bool blowLeft = false;
    public bool blowRight = false;
    private float electrocutionTimer = 5f;
    public float levelTimer = 0f;
    public float bottleBoost = 20f;
    public float chargeDown = .5f;
    public List<GameObject> bottles;
    public AudioSource bounce;
    public AudioSource rainSound;
    public AudioSource thunderSound;
    public AudioSource pickup;
    public AudioSource perish;
    public AudioSource windSound;
    public AudioSource BGMusic;
    public int level;

    public GameObject[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        BGMusic.Play();
        stormText.text = "STORM: " + (windSpeed);
        baseVect = windVect;
        baseVectL = windVectL;
        playerRb = GetComponent<Rigidbody>();
        InvokeRepeating("stormIncrease", 5f, 5f);
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        int j = 0;
        for(int i = 0; i < (int) (windSpeed /10); i++) {
            //if(i + 1  <= (int) (windSpeed/10)) {
                //Debug.Log((int) (windSpeed/10));
                clouds[i].SetActive(true);
                j = i;
            //} else {
            }
            for(int x = j; x < 4; x++) {
                clouds[x].SetActive(false);
            }
        
        if(!blowLeft && !blowRight && !tornadoActive && Input.GetKey(KeyCode.C)) {
            chargeDown -= Time.deltaTime;
        } else {
            chargeDown = .3f;
        }

        if(chargeDown <= 0f) {
            if(windSpeed < stormMax) {
                windSpeed++;
                rainForce += 10;
                stormText.text = "STORM: " + (int) (windSpeed);
            }
            chargeDown = .3f;
        }
        if(Input.GetKeyDown(KeyCode.S) && windSpeed >= 20f && state == 0) {
            raining = true;
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            transform.position = spawnPosition;
            playerRb.velocity = Vector3.zero;
            tornadoActive = false;
            tornadoCooldown = 0f;
            tornado = 0;
            tornadoOverdrive = 0f;
            lastTornado = -1;
            windSpeed = 30f;
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
            for(int i = 0; i < bottles.Count; i++) {
                bottles[i].SetActive(true);
            }

        }
    }
    void FixedUpdate()
    {
        if(state != 2 && state != -1) {
            levelTimer += Time.deltaTime;
        }
        switch(state) {
            
            case 0:

            if(electrocutionTimer >= 0f) {
                electrocutionTimer -= Time.deltaTime;
            } else {
                electrocutionTimer = -1f;
                Electrocuted = false;
            }
            if(raining) {
                playerRb.AddForce(Vector3.down * rainForce * Time.deltaTime, ForceMode.Impulse);
                rainSound.Play();
                raining = false;
            if(!tornadoActive) {
                windSpeed -= 10f;
                rainForce = windSpeed * 10f;
                stormText.text = "STORM: " + (int) (windSpeed);
            }
            
                rainEffect.Play();
            }
            if(Input.GetKey(KeyCode.A)) {
            blowLeft = true;
            playerRb.AddForce(windVectL * windSpeed * windMod * Time.deltaTime, ForceMode.Impulse);
            //if(windVectL.y <= .3f) {
                    //windVectL = windVectL + new Vector3(0f, .0005f, 0f);
                //}
            
            if(tornadoOverdrive == 0f && lastTornado != 0) {
                tornadoCooldown = .3f;
                tornado++;
                lastTornado = 0;
            }
        } 
        else {
        windVectL = baseVectL;
        blowLeft = false;
        if(Input.GetKey(KeyCode.D)) {
            blowRight = true;
            playerRb.AddForce(windVect * windSpeed * windMod * Time.deltaTime, ForceMode.Impulse);
                //if(windVect.y <= .3f) {
                    //windVect = windVect + new Vector3(0f, .0005f, 0f);
                //}

            
            if(tornadoOverdrive == 0f && lastTornado != 1) {
                tornadoCooldown = .3f;
                tornado++;
                lastTornado = 1;
            }
        } else {
            blowRight = false;
            windVect = baseVect;
        }
    }

        if(tornadoActive) {
            playerRb.AddForce(Vector3.up * (windSpeed / 4f) * Time.deltaTime, ForceMode.Impulse);
            int xx = (int) (Random.Range(0, 10));
            if(xx == 1) {
                playerRb.AddForce(Vector3.left * 40f * Time.deltaTime, ForceMode.Impulse);
            }
            if(xx == 2) {
                playerRb.AddForce(Vector3.right * 40f * Time.deltaTime, ForceMode.Impulse);
            }
        }
    
        if(tornadoCooldown > 0f) {
            tornadoCooldown -= Time.deltaTime;
        }
        if(tornadoOverdrive > 0f) {
            tornadoOverdrive -= Time.deltaTime;
            if(tornadoOverdrive < 2f && tornadoActive) {
                tornadoActive = false;
                windSound.Stop();
                windSpeed -= 40f;
                rainForce = windSpeed * 10f;
                stormText.text = "STORM: " + (int) (windSpeed);

            }
        } else {
            tornadoOverdrive = 0f;
        }



        if(tornadoCooldown <= 0f) {
            tornadoCooldown = 0f;
            tornado = 0;
            lastTornado = -1;
        }

        if(tornado >= 8) {
            if(windSpeed >= 50f) {
                stormText.text = "STORM: " + (int) (windSpeed * 10f) + "!!";
                rainForce = 500f;
                tornadoActive = true;
                windSound.Play();
            }
            tornadoCooldown = 0f;
            tornado = 0;
            tornadoOverdrive = 10f;
            lastTornado = -1;
        }

        if(Input.GetKeyDown(KeyCode.W) && !tornadoActive && windSpeed >= 35) {
            state = 1;
            lightningStrike = 2f;
            crosshair.SetActive(true);
            crosshair.transform.position = transform.position - new Vector3(0, .3f, 0);
        }

        break;
        case 1: 
        lightningStrike -= Time.deltaTime;
        if(Input.GetKey(KeyCode.A)) {
            crosshair.transform.Translate(Vector3.left * 10f * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D)) {
            crosshair.transform.Translate(Vector3.right * 10f * Time.deltaTime);
        }
        if(lightningStrike <= 0f) {
            Instantiate(lightning, crosshair.transform.position + new Vector3(0, 4.5f, 0), lightning.transform.rotation);
            thunderSound.Play();
            lightningStrike = 0f;
            state = 0;
            windSpeed -= 30f;
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
            crosshair.SetActive(false);
        }
        break;
        case 2:
            playerRb.AddForce(windVect * windSpeed * windMod * Time.deltaTime, ForceMode.Impulse);
            
        break;
        }

        
    }

    public void resetVelocity() {
        playerRb.velocity = Vector3.zero;
    }

    void stormIncrease() {
        if(windSpeed < stormMax) {
            if(windSpeed + 5 <= stormMax) {
                windSpeed += 5f;
            } else {
                windSpeed = 50f;
            }
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Hazard")) {
                perish.Play();
                transform.position = spawnPosition;
                playerRb.velocity = Vector3.zero;
                tornadoActive = false;
                tornadoCooldown = 0f;
                tornado = 0;
                tornadoOverdrive = 0f;
                lastTornado = -1;
                windSpeed = 30f;
                rainForce = windSpeed * 10f;
                stormText.text = "STORM: " + (int) (windSpeed);
                for(int i = 0; i < bottles.Count; i++) {
                    bottles[i].SetActive(true);
                }
        }

        if(other.gameObject.CompareTag("Bottle") && !tornadoActive) {
            pickup.Play();
            if(windSpeed + bottleBoost <= 50f) {
                windSpeed += bottleBoost;
                } else {
                     windSpeed = 50f;
                }
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
            other.gameObject.SetActive(false);

        }

        if(other.gameObject.CompareTag("Checkpoint")) {
            pickup.Play();
            spawnPosition = transform.position;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Lightning")) {
            Electrocuted = true;
            electrocutionTimer = 5f;
            electrocution.Play();
        }

        if(other.gameObject.CompareTag("WeakPoint")) {
            GameObject.Find("WeaknessSpot").SetActive(false);
            GameObject.Find("BigBoss").GetComponent<BossEnem>().Hit();
        }
    }
    IEnumerator LevelClearText() {
        clearText.SetActive(true);
        timeText.text = "Time: " + (int) (levelTimer);
        yield return new WaitForSeconds(5f);
        Destroy(clearText);
        switch(level) {
            case 0: SceneManager.LoadScene("BossStage");
            break;
            case 1: SceneManager.LoadScene("MainMenu");
            break;
            case 2: SceneManager.LoadScene("Victory");
            break;
        }
    }
    void OnCollisionEnter(Collision other) {

        if(other.gameObject.CompareTag("Enemy")) {
                        if(!Electrocuted) {
            perish.Play();
            transform.position = spawnPosition;
            playerRb.velocity = Vector3.zero;
            tornadoActive = false;
            tornadoCooldown = 0f;
            tornado = 0;
            tornadoOverdrive = 0f;
            lastTornado = -1;
            windSpeed = 30f;
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
             for(int i = 0; i < bottles.Count; i++) {
                bottles[i].SetActive(true);
            }
        }
           else {
                Destroy(other.gameObject);
                perish.Play();
            }
        } else {
            bounce.Play();
        }
    }


}
