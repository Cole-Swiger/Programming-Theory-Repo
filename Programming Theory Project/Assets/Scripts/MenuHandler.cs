using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedBall;
    public TextMeshProUGUI uniqueActionText;
    //Inputting 0 currently uses default force instead of no force
    //editor limits input to 2 digits (encapsulation)
    public TMP_InputField forceInput;

    void Start()
    {
        selectedBall = GameObject.Find("BasketBall");
        uniqueActionText.text = "Bounce Pass";
    }

    //Set up event listeners
    private void OnEnable()
    {
        Ball.OnClickedFinish += SetActiveBall;
    }

    private void OnDisable()
    {
        Ball.OnClickedFinish -= SetActiveBall;
    }

    //When a ball is clicked, set it as active
    private void SetActiveBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            if (ball.GetComponent<Ball>().isSelected)
            {
                selectedBall = ball;
                SetActionText(ball.name);
                return;
            }
        }
    }

    //Change text on Action button to match the unique action of the selected ball
    private void SetActionText(string name)
    {
        switch (name)
        {
            case "SoccerBall":
                uniqueActionText.text = "Punt";
                break;
            case "BasketBall":
                uniqueActionText.text = "Bounce Pass";
                break;
            case "VolleyBall":
                uniqueActionText.text = "Spike";
                break;
            default:
                uniqueActionText.text = "Action";
                break;
        }       
    }

    //When clicked, call Bounce method of parent Ball class (Inheritance)
    public void Bounce()
    {
        //format string
        string forceValue = forceInput.text.TrimStart('0');
        if (forceValue != "") 
        {
            float force = float.Parse(forceValue);
            selectedBall.GetComponent<Ball>().Bounce(force);
        }
        else
        {
            selectedBall.GetComponent<Ball>().Bounce();
        }
    }

    //When clicked, call Pass method of selected ball (Polymorphism)
    public void Pass()
    {
        //format string
        string forceValue = forceInput.text.TrimStart('0');

        if (forceValue != "")
        {
            //Each ball has their own class and Pass methods
            float force = float.Parse(forceValue);
            if (selectedBall.name.Equals("SoccerBall"))
            {
                selectedBall.GetComponent<Soccer>().Pass(force);
            }
            else if (selectedBall.name.Equals("BasketBall"))
            {
                selectedBall.GetComponent<Basketball>().Pass(force);
            }
            else
            {
                selectedBall.GetComponent<Volleyball>().Pass(force);
            }
        }
        //A default value is used when no force is input (or if it is 0)
        else
        {
            if (selectedBall.name.Equals("SoccerBall"))
            {
                selectedBall.GetComponent<Soccer>().Pass();
            }
            else if (selectedBall.name.Equals("BasketBall"))
            {
                selectedBall.GetComponent<Basketball>().Pass();
            }
            else
            {
                selectedBall.GetComponent<Volleyball>().Pass();
            }
        }
    }

    //When clicked, Calls the unique action of the selected ball.
    public void Action()
    {
        //format string
        string forceValue = forceInput.text.TrimStart('0');

        if (forceValue != "")
        {
            //Each ball has 2 versions of their unique action
            float force = float.Parse(forceValue);
            if (uniqueActionText.text.Equals("Punt"))
            {
                selectedBall.GetComponent<Soccer>().Punt(force);
            }
            else if (uniqueActionText.text.Equals("Bounce Pass"))
            {
                selectedBall.GetComponent<Basketball>().BouncePass(force);
            }
            else
            {
                selectedBall.GetComponent<Volleyball>().Spike(force);
            }
        }
        else
        {
            if (uniqueActionText.text.Equals("Punt"))
            {
                selectedBall.GetComponent<Soccer>().Punt();
            }
            else if (uniqueActionText.text.Equals("Bounce Pass"))
            {
                selectedBall.GetComponent<Basketball>().BouncePass();
            }
            else
            {
                selectedBall.GetComponent<Volleyball>().Spike();
            }
        }
    }

    //Reset the scene
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
