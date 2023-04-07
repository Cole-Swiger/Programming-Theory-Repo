using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : Ball
{
    [SerializeField]
    private float downForce = -1;
    
    void Start()
    {
        //Abstraction
        Setup();
    }

    //Polymorphism
    //Throw ball to target with slight arch
    public override void Pass()
    {
        //abstraction
        ApplyForce(defaultForce, upForce, 4);
    }

    public override void Pass(float force)
    {
        //abstraction
        ApplyForce(force, upForce, 8);
    }

    //Bounce ball on ground towards target
    public void BouncePass()
    {
        ApplyForce(defaultForce, downForce, 2);
    }

    public void BouncePass(float force)
    {
        ApplyForce(force, downForce, 4);
    }
}
