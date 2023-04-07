using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volleyball : Ball
{
    //private forces
    [SerializeField]
    private float spikeForce = 1;
    [SerializeField]
    private float setForce = 10;

    //Set up ball
    void Start()
    {
        //abstraction
        Setup();
    }

    //Polymorphism
    //Hit ball towards target with upward force
    //Two versions of method, one uses a default force if input is empty or 0
    public override void Pass()
    {
        //abstraction
        ApplyForce(defaultForce, upForce, 10);
    }

    public override void Pass(float force)
    {
        //abstraction
        ApplyForce(force, upForce, 20);
    }

    //Throw ball up then spike towards target
    public void Spike()
    {
        StartCoroutine(SpikeBall(defaultForce, 0.4f));
    }

    public void Spike(float force)
    {
        StartCoroutine(SpikeBall(force/1.5f, 0.4f));
    }

    //Applies the spike force after throwing the ball up
    private IEnumerator SpikeBall(float force, float time)
    {
        ballRb.useGravity = true;
        ballRb.AddForce(Vector3.up * setForce, ForceMode.Impulse);
        yield return new WaitForSeconds(time);

        ApplyForce(force, spikeForce, 4);
    }
}
