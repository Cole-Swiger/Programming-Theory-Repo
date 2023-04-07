using System.Collections;
using UnityEngine;

public class Soccer : Ball
{
    [SerializeField]
    private float puntForce = 5;
    
    void Start()
    {
        //abstraction
        Setup();
    }

    //Polymorphism
    //Drop ball to ground, then kick towards target
    public override void Pass()
    {
        //abstraction
        StartCoroutine(BallForce(defaultForce, upForce, 1));
    }

    public override void Pass(float force)
    {  
        //abstraction
        StartCoroutine(BallForce(force/2, upForce, 1));
    }

    //Drop ball, then kick towards target with upward force
    public void Punt()
    {
        StartCoroutine(BallForce(defaultForce/1.75f, puntForce, 0.4f));
    }

    public void Punt(float force)
    {
        StartCoroutine(BallForce(force/3.5f, puntForce, 0.4f));
    }

    //Apply force at given time after dropping ball
    private IEnumerator BallForce(float force, float up, float time)
    {
        ballRb.useGravity = true;
        yield return new WaitForSeconds(time);

        //abstraction
        ApplyForce(force, up, 6);
    }
}
