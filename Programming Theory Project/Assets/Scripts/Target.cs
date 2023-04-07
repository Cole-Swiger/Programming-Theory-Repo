using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public List<Material> materials;
    //encapsulation
    [SerializeField]
    private bool m_isSelected;
    public bool isSelected
    {
        get { return m_isSelected; }
        private set { 
            //change materials if the ball is selected
            if (value)
            {
                m_isSelected = true;
                GetComponent<Renderer>().material = materials[1];
            }
            else
            {
                m_isSelected = false;
                GetComponent<Renderer>().material = materials[0];
            }
        }
    }

    //Set up events
    public delegate void ClickAction();
    public delegate void ClickFinishAction();
    public static event ClickAction OnClicked;
    public static event ClickFinishAction OnClickedFinish;

    //Set up event listeners
    private void OnEnable()
    {
        Target.OnClicked += Deselect;
    }

    private void OnDisable()
    {
        Target.OnClicked -= Deselect;
    }

    private void OnMouseDown()
    {
        //Update Selected Target
        if (OnClicked != null)
        {
            OnClicked();
        }
        isSelected = true;
        //Assign new selected target to ball
        if (OnClickedFinish != null)
        {
            OnClickedFinish();
        }
    }

    //Only one target should be selected at a time
    private void Deselect()
    {
        isSelected = false;
    }
}
