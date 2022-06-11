using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float minYSpeed;
    public GameObject paddle;
    public float paddleLength;

    private Rigidbody2D thisRigidBody;
    private Vector3 lastVelocity;
    private int blockLayer;
    private float currentSpeed;
    private bool attached = true;
    private float attachedOffset;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        blockLayer = LayerMask.NameToLayer("Blocks");
        paddleLength = paddle.transform.localScale.x/2;
        attachedOffset = (paddle.transform.localScale.y/2) + (transform.localScale.y / 2);
        ballSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (attached) moveWithPaddle();
        else
        {
            var v = thisRigidBody.velocity.normalized;
            v = v * currentSpeed;
            thisRigidBody.velocity = v;
            lastVelocity = v;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float paddleAdjustment;
        Vector3 newDirection;

        if(col.gameObject == paddle){
            paddleAdjustment = scaleDirection(transform.position.x - paddle.transform.position.x);
            newDirection = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);
            newDirection = new Vector3(paddleAdjustment, newDirection.y, newDirection.z);
            currentSpeed = Mathf.Max(maxSpeed * Mathf.Abs(paddleAdjustment), minSpeed);
            var newVelocity = newDirection * currentSpeed;
            newVelocity = new Vector3(newVelocity.x, Mathf.Max(newVelocity.y, minYSpeed), newVelocity.z);
            thisRigidBody.velocity = newVelocity;
        }
        else
        {
            newDirection = Vector3.Reflect(lastVelocity.normalized, col.contacts[0].normal);
            thisRigidBody.velocity = newDirection * currentSpeed;
        }
        
        if(col.gameObject.layer == blockLayer)
        {
            Destroy(col.gameObject);
        }
    }

    public void nextBall()
    {
        attached = true;
        //TODO: Decrease ball count
    }

    private void moveWithPaddle()
    {
        transform.position = paddle.transform.position + new Vector3(0, attachedOffset, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            attached = false;
            ballSetup();
        }
    }

    private void ballSetup()
    {
        thisRigidBody.velocity = new Vector3(0, 1, 0) * minSpeed;
        currentSpeed = minSpeed;
    }

    private float scaleDirection(float direction)
    {
        return direction / paddleLength;
    }

}
