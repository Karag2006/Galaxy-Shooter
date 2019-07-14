using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;

    [SerializeField] //0 = tripleshot , 1 = Speed, 2 = Shields
    private int powerupID;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                  case 0:
                    player.ActivateTripleShot();
                    break;
                  case 1:
                    player.ActivateSpeedBoost();
                    break;
                  case 2:
                    Debug.Log("Collected Shields");
                    break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
