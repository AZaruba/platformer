using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerCollisionDetection))]
public class PlayerMovement : MonoBehaviour {



    // Use this for initialization
    void Start () {
        col = GetComponent<PlayerCollisionDetection>();
        grounded = true;
	}

    public static float speed = 48;
    public static float Rightspeed = speed;
    public static float Leftspeed = speed;
    public float jump;
    public float gravity;
    public float terminalVelocity;
    public static float yspeed;
    public static Vector3 horizontalStep;
    public static Vector3 verticalStep = new Vector3(0,1,0);
    public static Vector3 groundCorrection;
    public Vector3 spawnPoint;
    public static bool grounded;
    PlayerCollisionDetection col;


    // Update is called once per frame
    void Update () {
        horizontalMovement();
        jumpPress();
        verticalMovement();
        transform.position += (horizontalStep);
        QuitGame();
	}

    //Movement and Menu methods
    void horizontalMovement ()
    {
        for (int i = 0; i < speed; i++)
        {
            if (col.checkRightWall())
                Rightspeed = 0;
            else
                Rightspeed = speed;
            if (col.checkLeftWall())
                Leftspeed = 0;
            else
                Leftspeed = speed;
        }
        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        if (Input.GetAxis("Horizontal") > 0)
            horizontalStep = Rightspeed * move * Time.deltaTime;
        if (Input.GetAxis("Horizontal") < 0)
            horizontalStep = Leftspeed * move * Time.deltaTime;
        if (Input.GetAxis("Horizontal") == 0)
            horizontalStep.x = 0;
    }

    void verticalMovement ()
    {
        if (!grounded)
        {
            if (yspeed > terminalVelocity)
                yspeed -= gravity;
            if (col.checkCeiling())
                yspeed = gravity * -1;
            for (int i = 0; i < Mathf.Abs(yspeed); i++)
            {
                if (yspeed > 0)
                    transform.position += verticalStep * Time.deltaTime;
                if (yspeed < 0 && !col.checkLand())
                    transform.position -= verticalStep * Time.deltaTime;
                if (yspeed < 0 && col.checkLand())
                {
                    yspeed = 0;
                    groundCorrection = transform.position;
                    groundCorrection.y = col.groundHit.transform.position.y;
                    transform.position = groundCorrection;
                    grounded = true;
                }

            }
        }
    }

    void jumpPress ()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            yspeed = jump;
            grounded = false;
        }
        else if (grounded && !col.checkLand())
            grounded = false;
    }

    void QuitGame ()
    {
        if (Input.GetKeyDown("escape"))
            Application.Quit();
    }

}
