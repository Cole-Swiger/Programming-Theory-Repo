using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public List<Material> materials;
    [SerializeField]
    private bool m_isSelected;
    public bool isSelected
    {
        get { return m_isSelected; }
        private set { 
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

    public delegate void ClickAction();
    public static event ClickAction OnClicked;

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
        if (OnClicked != null)
        {
            OnClicked();
        }
        isSelected = true;
    }

    private void Deselect()
    {
        isSelected = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
