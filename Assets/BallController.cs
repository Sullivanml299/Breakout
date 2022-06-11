using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public GameObject paddle;
    private Rigidbody2D thisRigidBody;
    private Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        thisRigidBody.velocity = new Vector3(1,1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var v = thisRigidBody.velocity.normalized;
        v = v*speed;
        thisRigidBody.velocity = v;
        lastVelocity = v;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float paddleAdjustment;
        Vector3 newDirection;

        if(col.gameObject == paddle){
            paddleAdjustment = transform.position.x - paddle.transform.position.x;
            newDirection = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);
            newDirection = new Vector3(paddleAdjustment, newDirection.y, newDirection.z);
        }
        else{
            newDirection = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);
        }
        thisRigidBody.velocity = newDirection*speed;

    }
}
