using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Vector2 lastVelocity;

    public movement LeftPlayer;
    public movement RightPlayer;

    public uicontroller uiController;
    private int RightPlayerScore;
    private int LeftPlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    public void SendBallInRandomDirection()
    {
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.isKinematic = true;
        transform.position = Vector3.zero;
        rigidbody2D.isKinematic = false;
        rigidbody2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 5f;
        lastVelocity = rigidbody2D.velocity;
        LeftPlayer.speed = LeftPlayer.defaultSpeed;
        RightPlayer.speed = RightPlayer.defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SendBallInRandomDirection();
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2D.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
        lastVelocity = rigidbody2D.velocity * 1.1f;
        RightPlayer.speed *= 1.1f;
        LeftPlayer.speed *= 1.1f; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            LeftPlayerScore++;
            uiController.SetLeftPlayerScore(LeftPlayerScore.ToString());
            Debug.Log("Left Player Scored");
        }
        if (transform.position.x < 0)
        {
            RightPlayerScore++;
            uiController.SetRightPlayerScore(RightPlayerScore.ToString());
            Debug.Log("Right Player Scored");
        }
        SendBallInRandomDirection();

    }
}
