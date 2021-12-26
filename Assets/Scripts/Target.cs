using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    float maxForce = 16;
    float minForce = 12;
    float torqueRange = 10;
    float positionRangeX = 4;
    float positionRangeY = -3;

    public ParticleSystem explosionParticle;
    Rigidbody targetRigidbody;
    GameManager gameManager;
    void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        targetRigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
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

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }
    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-positionRangeX, positionRangeX), positionRangeY);
    }
}
