using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D thisRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        var left = Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
        var right = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        var direction = left + right;
        var xComponent =moveSpeed*direction;//*Time.deltaTime;
        var moveVector = new Vector2(xComponent,0);
        thisRigidBody.velocity = moveVector;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("OnCollisionEnter2D");
    }
}
