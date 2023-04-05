using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedBall;
    // Start is called before the first frame update
    void Start()
    {
        selectedBall = GameObject.Find("BasketBall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Ball.OnClickedFinish += SetActiveBall;
    }

    private void OnDisable()
    {
        Ball.OnClickedFinish -= SetActiveBall;
    }

    private void SetActiveBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            if (ball.GetComponent<Ball>().isSelected)
            {
                selectedBall = ball;
                return;
            }
        }
    }

    public void Bounce()
    {
        selectedBall.GetComponent<Rigidbody>().useGravity = true;
    }
}
