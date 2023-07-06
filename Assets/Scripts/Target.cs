using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public bool isLightningTargeted = false;
    public int score;
    private GameManager manager;
    public ParticleSystem explosionParticles;
    public ParticleSystem syntyCubeEffectedExplosionParticles;
    public AudioClip destroySound;
    private CameraShake cameraShake;

    void Start()
    {
        cameraShake = GameObject.Find("Main Camera").ConvertTo<CameraShake>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), ForceMode.Impulse);
        transform.position = new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
    }

    private void OnMouseDown()
    {
        if(manager.isGameRunning && !manager.isGamePaused)
        {
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
            Destroy(gameObject);
            manager.effectSoundSource.PlayOneShot(destroySound);
            manager.updateScore(score);

            if (gameObject.CompareTag("QuestionCube"))
            {
            }
            else if (gameObject.CompareTag("SyntyCube"))
            {
                //StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                Target[] objs = GameObject.FindObjectsOfType<Target>();
                foreach (Target obj in objs)
                {
                    Instantiate(syntyCubeEffectedExplosionParticles, obj.transform.position, syntyCubeEffectedExplosionParticles.transform.rotation);
                    //manager.effectSoundSource.PlayOneShot(destroySound);
                    manager.updateScore(obj.score);
                    Destroy(obj.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good"))
        {
            if(manager.score - 5 < 0)
                manager.GameOver();
            manager.updateScore(-5);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    void Update()
    {
    }
}
