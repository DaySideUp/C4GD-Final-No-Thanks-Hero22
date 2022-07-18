using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Wind : MonoBehaviour
{
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
    public TextMeshProUGUI stormText;
    private float stormMax = 50f;
    private int state = 0;
    private GameObject crosshair;
    private float lightningStrike = 0f;
    // Start is called before the first frame update
    void Start()
    {
        stormText.text = "STORM: " + (windSpeed);
        baseVect = windVect;
        baseVectL = windVectL;
        playerRb = GetComponent<Rigidbody>();
        InvokeRepeating("stormIncrease", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case 0:if(Input.GetKey(KeyCode.A)) {
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
        if(Input.GetKey(KeyCode.D)) {
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
            windVect = baseVect;
        }
    }

        if(tornadoActive) {
            playerRb.AddForce(Vector3.up * (windSpeed / 4f) * Time.deltaTime, ForceMode.Impulse);
            int xx = (int) (Random.Range(0, 10));
            if(xx == 1) {
                playerRb.AddForce(Vector3.left * 10f * Time.deltaTime, ForceMode.Impulse);
            }
            if(xx == 2) {
                playerRb.AddForce(Vector3.right * 10f * Time.deltaTime, ForceMode.Impulse);
            }
        }
    
        if(tornadoCooldown > 0f) {
            tornadoCooldown -= Time.deltaTime;
        }
        if(tornadoOverdrive > 0f) {
            tornadoOverdrive -= Time.deltaTime;
            if(tornadoOverdrive < 2f && tornadoActive) {
                tornadoActive = false;
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
            }
            tornadoCooldown = 0f;
            tornado = 0;
            tornadoOverdrive = 10f;
            lastTornado = -1;
        }


        if(Input.GetKeyDown(KeyCode.S) && windSpeed >= 20f) {
            playerRb.AddForce(Vector3.down * rainForce * Time.deltaTime, ForceMode.Impulse);
            if(!tornadoActive) {
                windSpeed -= 10f;
                rainForce = windSpeed * 10f;
                stormText.text = "STORM: " + (int) (windSpeed);
            }
            
            rainEffect.Play();
        }
        break;
        case 1: 
        if(Input.GetKey(KeyCode.A)) {
            crosshair.transform.Translate(Vector3.left * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D)) {
            crosshair.transform.Translate(Vector3.right * Time.deltaTime);
        }
        if(lightningStrike <= 0f) {
            lightningStrike = 0f;
            state = 0;
            windSpeed -= 30f;
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
        }
        break;
        }

        
    }

    void stormIncrease() {
        if(windSpeed < stormMax) {
            windSpeed += 5f;
            rainForce = windSpeed * 10f;
            stormText.text = "STORM: " + (int) (windSpeed);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Hazard")) {
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
        }
    }


}
