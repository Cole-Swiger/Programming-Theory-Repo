using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance
public class Ball : MonoBehaviour
{
    protected GameObject selectedTarget;
    //protected bool isEventTrigger;
    //Encapsulation
    [SerializeField]
    private bool m_isSelected;
    public bool isSelected
    {
        get
        {
            return m_isSelected;
        }
        protected set
        {
            m_isSelected = value;
        }
    }

    public delegate void ClickAction();
    public delegate void ClickFinishAction();
    public static event ClickAction OnClicked;
    public static event ClickFinishAction OnClickedFinish;

    // Start is called before the first frame update
    void Start()
    {
        selectedTarget = GameObject.Find("Target2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnEnable()
    {
        Target.OnClicked += UpdateSelected;
        Ball.OnClicked += UpdateSelfSelected;
        //Set the inital selectedTarget
        UpdateSelected();
    }

    protected void OnDisable()
    {
        Target.OnClicked -= UpdateSelected;
        Ball.OnClicked -= UpdateSelfSelected;
    }

    protected void OnMouseDown()
    {
        isSelected = true;
        //Allows event to not change the isSelected value of object that called event
        //isEventTrigger = true;
        if (OnClicked != null)
        {
            OnClicked();
        }
        isSelected = true;

        if (OnClickedFinish != null)
        {
            OnClickedFinish();
        }
        //isEventTrigger = false;
        Debug.Log(gameObject.name + " selected: " + isSelected);
    }

    protected void UpdateSelected()
    {
        SetSelected("Target");
        /*GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
        {
            if (target.GetComponent<Target>().isSelected)
            {
                selectedTarget = target;
                return;
            }
        }*/
    }

    protected void UpdateSelfSelected()
    {
        SetSelected("Ball");
    }

    private void SetSelected(string tag)
    {
        Debug.Log("In SetSelected method");
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject target in targets)
        {
            Component comp = target.GetComponent(tag);
            if (comp is Target && target.GetComponent<Target>().isSelected)
            {
                selectedTarget = target;
                Debug.Log("Selected target: " + selectedTarget.name);
                return;
            }
            if (comp is Ball && target.GetComponent<Ball>().isSelected)
            {
                isSelected = false;
            }
        }
    }

    protected void Bounce()
    {
        Debug.Log(gameObject.name + " is bouncing");
        Debug.Log("Current target: " + selectedTarget.name);
    }

    protected void Bounce(float force)
    {

    }

    protected virtual void Pass()
    {

    }

    protected virtual void Pass(float force)
    {

    }
}
