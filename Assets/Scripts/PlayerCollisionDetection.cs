using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerCollisionDetection : MonoBehaviour {
    Vector3 hitbox_rMiddle;
    Vector3 hitbox_rTop;
    Vector3 hitbox_rBottom;

    Vector3 hitbox_dMiddle;
    Vector3 hitbox_dLeft;
    Vector3 hitbox_dRight;

    Vector3 hitbox_lMiddle;
    Vector3 hitbox_lTop;
    Vector3 hitbox_lBottom;

    Vector3 hitbox_uMiddle;
    Vector3 hitbox_uLeft;
    Vector3 hitbox_uRight;

    Vector3 upDiagonalRight = new Vector3(1, 1, 0);
    Vector3 upDiagonalLeft = new Vector3(1, 1, 0);


    Vector3 right;
    Vector3 left;
    Vector3 up;
    Vector3 down;
    BoxCollider2D col;
    PlayerMovement mov;

    // Use this for initialization
    void Start() {
        col = GetComponent<BoxCollider2D>();
        mov = GetComponent<PlayerMovement>();
        right = transform.right;
        left = transform.right * -1;
        up = transform.up;
        down = transform.up * -1;

    }
    public GameObject groundHit;

    // Update is called once per frame
    void Update() {
        UpdateCollider();
        checkKill();
    }

    public void UpdateCollider()
    {
        col = GetComponent<BoxCollider2D>();

        //Right side collider updates
        hitbox_rBottom.x = transform.position.x + (col.size.x) / 2;
        hitbox_rBottom.y = transform.position.y + 0.001f;
        hitbox_rBottom.z = transform.position.z;

        hitbox_rMiddle.x = transform.position.x + (col.size.x) / 2;
        hitbox_rMiddle.y = transform.position.y + (col.size.y) / 2;
        hitbox_rMiddle.z = transform.position.z;

        hitbox_rTop.x = transform.position.x + (col.size.x) / 2;
        hitbox_rTop.y = transform.position.y + col.size.y;
        hitbox_rTop.z = transform.position.z;

        //bottom collider updates
        hitbox_dLeft.x = transform.position.x - (col.size.x) / 2 + 0.1f;
        hitbox_dLeft.y = transform.position.y;
        hitbox_dLeft.z = transform.position.z;

        hitbox_dMiddle = transform.position;

        hitbox_dRight.x = transform.position.x + (col.size.x) / 2 - 0.1f;
        hitbox_dRight.y = transform.position.y;
        hitbox_dRight.y = transform.position.z;

        //Left collider updates
        hitbox_lBottom.x = transform.position.x - (col.size.x) / 2;
        hitbox_lBottom.y = transform.position.y + 0.001f;
        hitbox_lBottom.z = transform.position.z;

        hitbox_lMiddle.x = transform.position.x - (col.size.x) / 2;
        hitbox_lMiddle.y = transform.position.y + (col.size.y) / 2;
        hitbox_lMiddle.z = transform.position.z;

        hitbox_lTop.x = transform.position.x - (col.size.x) / 2;
        hitbox_lTop.y = transform.position.y + col.size.y;
        hitbox_lTop.z = transform.position.z;

        //Top collider updates
        hitbox_uLeft.x = transform.position.x - (col.size.x) / 2 + 0.1f;
        hitbox_uLeft.y = transform.position.y + (col.size.y);
        hitbox_uLeft.z = transform.position.z;

        hitbox_uMiddle = transform.position;
        hitbox_uMiddle.y = transform.position.y + (col.size.y);

        hitbox_uRight.x = transform.position.x + (col.size.x) / 2 - 0.1f;
        hitbox_uRight.y = transform.position.y + (col.size.y);
        hitbox_uRight.y = transform.position.z;
    }

    public bool checkRightWall()
    {
        //Right side rays
        RaycastHit2D hitRBottom = Physics2D.Raycast(hitbox_rBottom, right, 0.1f, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitRMiddle = Physics2D.Raycast(hitbox_rMiddle, right, 0.1f, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitRTop = Physics2D.Raycast(hitbox_rTop, right, 0.1f, 1 << LayerMask.NameToLayer("Environment"));

        if (hitRBottom || hitRMiddle || hitRTop)
            return true;
        else
            return false;
    }
    public bool checkLeftWall()
    {

        //Left side rays
        RaycastHit2D hitLBottom = Physics2D.Raycast(hitbox_lBottom, left, 0.1f, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitLMiddle = Physics2D.Raycast(hitbox_lMiddle, left, 0.1f, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitLTop = Physics2D.Raycast(hitbox_lTop, left, 0.1f, 1 << LayerMask.NameToLayer("Environment"));

        if (hitLBottom || hitLMiddle || hitLTop)
            return true;
        else
            return false;
    }

    public bool checkLand()
    {
        //Bottom rays
        RaycastHit2D hitBLeft = Physics2D.Raycast(hitbox_dLeft, down, 1, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitBMiddle = Physics2D.Raycast(hitbox_dMiddle, down, 1, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitBRight = Physics2D.Raycast(hitbox_dRight, down, 1, 1 << LayerMask.NameToLayer("Environment"));

        if (hitBLeft)
            groundHit = hitBLeft.collider.gameObject;
        if (hitBMiddle)
            groundHit = hitBMiddle.collider.gameObject;
        if (hitBRight)
            groundHit = hitBRight.collider.gameObject;
        if (hitBLeft || hitBMiddle || hitBRight)
            return true;
        return false;
    }

    public bool checkCeiling()
    {
        RaycastHit2D hitTLeft = Physics2D.Raycast(hitbox_uLeft, up, 0, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitTMid = Physics2D.Raycast(hitbox_uMiddle, up, 0, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitTRight = Physics2D.Raycast(hitbox_uRight, up, 0, 1 << LayerMask.NameToLayer("Environment"));

        RaycastHit2D hitDiagRight = Physics2D.Raycast(hitbox_uRight, upDiagonalRight, 1, 1 << LayerMask.NameToLayer("Environment"));
        RaycastHit2D hitDiagLeft = Physics2D.Raycast(hitbox_uLeft, upDiagonalLeft, 1, 1 << LayerMask.NameToLayer("Environment"));
        if (hitTLeft || hitTRight || hitTMid || hitDiagRight || hitDiagLeft)
            return true;
        return false;
    }

    public void checkKill()
    {
        //Bottom rays
        RaycastHit2D hitBLeft = Physics2D.Raycast(hitbox_dLeft, down, 0, 1 << LayerMask.NameToLayer("Killplane"));
        RaycastHit2D hitBMiddle = Physics2D.Raycast(hitbox_dMiddle, down, 0, 1 << LayerMask.NameToLayer("Killplane"));
        RaycastHit2D hitBRight = Physics2D.Raycast(hitbox_dRight, down, 0, 1 << LayerMask.NameToLayer("Killplane"));

        if (hitBLeft || hitBMiddle || hitBRight)
        {
            transform.position = mov.spawnPoint;
        }
    }
}
