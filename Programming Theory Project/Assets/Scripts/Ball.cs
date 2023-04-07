using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance
public class Ball : MonoBehaviour
{
    //Selected object
    [SerializeField]
    protected GameObject selectedTarget;
    //force fields
    protected Rigidbody ballRb;
    protected float defaultForce = 25;
    [SerializeField]
    protected float upForce;
    //Encapsulation
    [SerializeField]
    private bool m_isSelected;
    public bool isSelected
    {
        get
        {
            return m_isSelected;
        }
        //only allow this class and child classes to set field
        protected set
        {
            m_isSelected = value;
        }
    }

    //Set up events
    public delegate void ClickAction();
    public delegate void ClickFinishAction();
    public static event ClickAction OnClicked;
    public static event ClickFinishAction OnClickedFinish;

    void Start()
    {
        Setup();
    }

    //Set Default selected target and ball's rigidbody
    protected void Setup()
    {
        selectedTarget = GameObject.Find("Target2");
        ballRb = gameObject.GetComponent<Rigidbody>();
    }

    //Set up event listeners
    protected void OnEnable()
    {
        Target.OnClickedFinish += UpdateSelected;
        Ball.OnClicked += UpdateSelfSelected;
        //Set the inital selectedTarget
        UpdateSelected();
    }

    protected void OnDisable()
    {
        Target.OnClickedFinish -= UpdateSelected;
        Ball.OnClicked -= UpdateSelfSelected;
    }

    protected void OnMouseDown()
    {
        isSelected = true;
        if (OnClicked != null)
        {
            OnClicked();
        }
        //Reverse the isSelected = false from event for calling object
        isSelected = true;

        if (OnClickedFinish != null)
        {
            OnClickedFinish();
        }
    }

    //Update selected target
    protected void UpdateSelected()
    {
        SetSelected("Target");
    }

    //update isSelected of self
    protected void UpdateSelfSelected()
    {
        SetSelected("Ball");
    }

    //Updates isSelected field of corresponding object.
    private void SetSelected(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            //Component will either ball or target
            Component comp = target.GetComponent(tag);
            if (comp is Target && target.GetComponent<Target>().isSelected)
            {
                selectedTarget = target;
                return;
            }
            if (comp is Ball && target.GetComponent<Ball>().isSelected)
            {
                isSelected = false;
            }
        }
    }

    //Inheritance
    public void Bounce()
    {
        //Allow ball to fall to ground and bounce
        GetComponent<Rigidbody>().useGravity = true;
    }

    //Bounce with a given force
    public void Bounce(float force)
    {
        ballRb.useGravity = true;
        ballRb.AddForce(Vector3.down * (force/2), ForceMode.Impulse);
    }

    //Polymorphism
    //Currently, both Pass methods here are overridden by all children, but allows for a default fallback method
    public virtual void Pass()
    {
        ballRb.useGravity = true;
        Vector3 actionDirection = selectedTarget.transform.position - transform.position;
        ballRb.AddForce(actionDirection * defaultForce, ForceMode.Impulse);
    }

    public virtual void Pass(float force)
    {
        ballRb.useGravity = true;
        Vector3 actionDirection = selectedTarget.transform.position - transform.position;
        ballRb.AddForce(actionDirection * force, ForceMode.Impulse);
    }

    /// <summary>
    /// All actions use this to apply appropriate force for situation
    /// </summary>
    /// <param name="force">forward force to be applied towards target</param>
    /// <param name="yForce">up/down force</param>
    /// <param name="divisor">controls the force to not be too large</param>
    protected void ApplyForce(float force, float yForce, float divisor)
    {
        ballRb.useGravity = true;
        Vector3 actionDirection = selectedTarget.transform.position - transform.position;
        //Add up/down angle to force
        actionDirection += new Vector3(0, yForce, 0);
        ballRb.AddForce(actionDirection * (force / divisor), ForceMode.Impulse);
    }
}
