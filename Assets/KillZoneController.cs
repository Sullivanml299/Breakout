using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{
    public BallController ball;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ball.nextBall();
    }
}
