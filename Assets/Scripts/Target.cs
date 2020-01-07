using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;

    private Rigidbody targetRb;
    private float minSpeed = 10;
    private float maxSpeed = 15;
    private float maxTorque = 10;
    private float xRange = 2;
    private float ySpawnPos = 0;
    private int pointValue = 5;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomAddForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomAddForce() 
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque() 
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos() 
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateScore(pointValue);
            }
            else
            {
                gameManager.UpdateScore(-2 * pointValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
}
