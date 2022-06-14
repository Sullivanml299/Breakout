using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float minYSpeed;
    public GameObject paddle;
    public Score scoreController;
    public float xMin;
    public float xMax;
    public Transform exploderTransform;
    public ParticleSystem exploder;
    public ParticleSystem trail;

    private SpriteRenderer thisRenderer;
    private Rigidbody2D thisRigidBody;
    private Vector3 lastVelocity;
    private float paddleLength;
    private int blockLayer;
    private float currentSpeed;
    private bool attached = true;
    private float attachedOffset;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();
        thisRenderer = GetComponent<SpriteRenderer>();
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
            currentSpeed = Mathf.Max(currentSpeed, minSpeed);
            var v = thisRigidBody.velocity.normalized;
            if(Mathf.Abs(thisRigidBody.velocity.x) < 0.1 && Mathf.Abs(lastVelocity.normalized.x)<0.1)
            {
                v = new Vector3(v.x + 0.1f * randomSign(), v.y);
            }
            if (Mathf.Abs(thisRigidBody.velocity.y) < 0.1 && Mathf.Abs(lastVelocity.normalized.y) < 0.1)
            {
                v = new Vector3(v.x, v.y + 0.1f * randomSign());
            }
            v = v * currentSpeed;
            thisRigidBody.velocity = v;
            lastVelocity = v;
            if(transform.position.x < xMin || transform.position.x > xMax)
            {
                nextBall();
            }
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
            newDirection = newDirection * currentSpeed;
            if(Mathf.Abs(newDirection.y)< minYSpeed)
            {
                var yComponent = Mathf.Sign(newDirection.y) * minYSpeed;
                newDirection = new Vector3(newDirection.x, yComponent);
            }
            thisRigidBody.velocity = newDirection;
        }
        
        if(col.gameObject.layer == blockLayer)
        {   
            var color = col.gameObject.GetComponent<SpriteRenderer>().color;
            explode(col.gameObject.transform.position, color);
            takeColor(color);
            Destroy(col.gameObject);
            scoreController.addOne();
        }
    }

    public void nextBall()
    {
        attached = true;
        scoreController.lifeDown();
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

    private float randomSign()
    {
        return Random.value < .5 ? 1 : -1;
    }

    private void explode(Vector3 pos, Color color)
    {
        exploderTransform.position = pos;
        var main = exploder.main;
        main.startColor = color;
        exploder.Play();
    }

    private void takeColor(Color color){
        thisRenderer.color = color;
        var main = trail.main;
        main.startColor = color;
    }
}
