using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Wind : MonoBehaviour
{
    public Rigidbody playerRb;
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
    public int tornado = 0;
    public int lastTornado = -1;
    public ParticleSystem rainEffect;
    public TextMeshProUGUI stormText;
    private float stormMax = .5f;
    private int state = 0;
    private GameObject crosshair;
    private float lightningStrike = 0f;
    // Start is called before the first frame update
    void Start()
    {
        stormText.text = "STORM: " + (windSpeed * 100f);
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
            playerRb.AddForce(windVectL * windSpeed, ForceMode.Impulse);
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
            playerRb.AddForce(windVect * windSpeed, ForceMode.Impulse);
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
            playerRb.AddForce(Vector3.up * (windSpeed / 2f), ForceMode.Impulse);
            int xx = (int) (Random.Range(0, 10));
            if(xx == 1) {
                playerRb.AddForce(Vector3.left * 1f, ForceMode.Impulse);
            }
            if(xx == 2) {
                playerRb.AddForce(Vector3.right * 1f, ForceMode.Impulse);
            }
        }
    
        if(tornadoCooldown > 0f) {
            tornadoCooldown -= Time.deltaTime;
        }
        if(tornadoOverdrive > 0f) {
            tornadoOverdrive -= Time.deltaTime;
            if(tornadoOverdrive < 2f && tornadoActive) {
                tornadoActive = false;
                windSpeed -=.4f;
                rainForce = windSpeed / .06f;
                stormText.text = "STORM: " + (int) (windSpeed * 100f);

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
            if(windSpeed > .5f) {
                stormText.text = "STORM: " + (int) (windSpeed * 1000f) + "!!";
                rainForce = 15f;
                tornadoActive = true;
            }
            tornadoCooldown = 0f;
            tornado = 0;
            tornadoOverdrive = 10f;
            lastTornado = -1;
        }


        if(Input.GetKeyDown(KeyCode.S) && windSpeed >= .2f) {
            playerRb.AddForce(Vector3.down * rainForce, ForceMode.Impulse);
            if(!tornadoActive) {
                windSpeed -= .2f;
                rainForce = windSpeed / .06f;
                stormText.text = "STORM: " + (int) (windSpeed * 100f);
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
            stormText.text = "STORM: " + (int) (windSpeed * 100f);
            windSpeed -= .3f;
            rainForce = windSpeed / .06f;
        }
        break;
        }

        
    }

    void stormIncrease() {
        if(windSpeed < stormMax) {
            windSpeed += .05f;
            rainForce = windSpeed / .06f;
            stormText.text = "STORM: " + (int) (windSpeed * 100f);
        }
    }


}
