using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody2D rb;

    private bool isAdded = false;
    private bool isPlayedAudio = false;
    private float height;
    private float weight;
    private float raycastDistance = 0.05f;
    private float raycastDownDistance = 0.15f;
    private float blockOffset = 0.08f;

    private string blockTag = "Block";
    string playerTag="Player";
    string groundTag="Ground";

    // Start is called before the first frame update
    void Start()
    {
        height = GetComponent<BoxCollider2D>().bounds.extents.y+0.01f;
        weight = GetComponent<BoxCollider2D>().bounds.extents.x+0.01f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MoveNextPoint();
    }

    private void FixedUpdate()
    {
        //GroundCheck();
        //MoveNextPoint();
    }

    public bool GetRightWall { get; set; } = false;
    public bool GetLeftWall { get; set; } = false;

    private bool IsPlayerOnTheLeft()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - weight, transform.position.y),
          Vector2.left, raycastDistance);
        Debug.DrawRay(new Vector2(transform.position.x - weight, transform.position.y), Vector2.left * raycastDistance, Color.green);

        Collider2D leftCollider = leftHit.collider;
        bool isPlayerOnTheLeft = false;

        if (leftCollider != null)
        {
            if (leftCollider.CompareTag(blockTag) || leftCollider.CompareTag(groundTag))
            {
                //isPlayerOnTheLeft = false;
                //leftCollider.gameObject.GetComponent<Block>().GetRightBlock = true;
                GetLeftWall = true;
            }
            else if (leftCollider.CompareTag(playerTag)/* && !leftCollider.CompareTag("Ground")*/)
            {
                isPlayerOnTheLeft = true;
                GetLeftWall = false;
            }
        }
        return isPlayerOnTheLeft;
    }

    private bool IsPlayerOnTheRight()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + weight, transform.position.y),
           Vector2.right, raycastDistance);
        Debug.DrawRay(new Vector2(transform.position.x + weight, transform.position.y), Vector2.right * raycastDistance, Color.green);

        Collider2D rightCollider = rightHit.collider;

        bool isPlayerOnTheRight = false;

        if (rightCollider != null)
        {
            if (rightCollider.CompareTag(blockTag) || rightCollider.CompareTag(groundTag))
            {
                //isPlayerOnTheRight = false;
                //rightCollider.gameObject.GetComponent<Block>().GetLeftBlock = true;
                GetRightWall = true;
            }
            else if (rightCollider.CompareTag(playerTag) /*&& !rightCollider.CompareTag("Ground")*/)
            {
                isPlayerOnTheRight = true;
                GetRightWall = false;
            }
        }
        return isPlayerOnTheRight;
    }

    private bool IsBlockAbove()
    {
        RaycastHit2D upperHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + height),
          Vector2.up, raycastDistance);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + height), Vector2.up * raycastDistance, Color.green);

        Collider2D upperCollider = upperHit.collider;

        bool isBlockAbove = false;
        if (upperCollider == null)
        {
            isBlockAbove = false;
        }
        else if (upperCollider != null)
        {
            if (upperCollider.CompareTag(blockTag) || upperCollider.CompareTag(playerTag))
            {
                isBlockAbove = true;
            }
        }
        return isBlockAbove;
    }

    private void GroundCheck()
    {
        RaycastHit2D downHitRight = Physics2D.Raycast(new Vector2(transform.position.x+weight-blockOffset, transform.position.y - height),
           Vector2.down, raycastDownDistance);
        Debug.DrawRay(new Vector2(transform.position.x+weight- blockOffset, transform.position.y - height), Vector2.down * raycastDownDistance, Color.green);
        Collider2D downColliderRight = downHitRight.collider;

        RaycastHit2D downHitLeft = Physics2D.Raycast(new Vector2(transform.position.x-weight+ blockOffset, transform.position.y - height),
           Vector2.down, raycastDownDistance);
        Debug.DrawRay(new Vector2(transform.position.x-weight+ blockOffset, transform.position.y - height), Vector2.down * raycastDownDistance, Color.green);
        Collider2D downColliderLeft = downHitLeft.collider;

        RaycastHit2D downHitMiddle = Physics2D.Raycast(new Vector2(transform.position.x , transform.position.y - height),
           Vector2.down, raycastDownDistance);
        Debug.DrawRay(new Vector2(transform.position.x , transform.position.y - height), Vector2.down * raycastDownDistance, Color.green);
        Collider2D downColliderMiddle = downHitMiddle.collider;

        if (downColliderRight == null && downColliderLeft==null && downColliderMiddle==null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), transform.position.y);
            isPlayedAudio = false;
            //transform.Translate(Vector2.down * fallingBlockSpeed * Time.deltaTime);
            //rb.AddForce(Vector2.down * fallingBlockSpeed);//only dynamic
        }

        //List<Collider2D> colliders = new List<Collider2D>();
        //colliders.Add(downColliderLeft);
        //colliders.Add(downColliderMiddle);
        //colliders.Add(downColliderRight);
        //foreach(Collider2D collider in colliders)
        //{
        //    if (collider != null)
        //        CheckTouching(collider);
        //}
        else if (downColliderRight != null)
        {
            CheckTouching(downColliderRight);
        }
        else if (downColliderLeft != null)
        {
            CheckTouching(downColliderLeft);
        }
        else if (downColliderMiddle != null)
        {
            CheckTouching(downColliderMiddle);
        }
    }

    private void CheckTouching(Collider2D touchingCollider)
    {
        if (touchingCollider.CompareTag(blockTag) || touchingCollider.CompareTag(groundTag))
        {
            if (!isPlayedAudio)
            {
                AudioManager.PlayBlockSound();
                isPlayedAudio = true;
            }

            rb.bodyType = RigidbodyType2D.Kinematic;
            GameOver(touchingCollider.gameObject);
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            if (!isAdded && touchingCollider.CompareTag(groundTag))
            {
                GameManager.instance.CombineBlocks(gameObject);
                isAdded = true;
            }
        }
        else if (touchingCollider.CompareTag(playerTag))
        {
            UIManager.instanse.ReplaceLifePicture();
            AudioManager.PlayOuchSound();
            Destroy(gameObject);
            GameManager.instance.ShowParticles(gameObject);
        }
    }

    private void MoveNextPoint()
    {
        if (!IsBlockAbove())
        {
            if (IsPlayerOnTheLeft() && !GetRightWall)
            {
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                //transform.position = Vector2.MoveTowards(transform.position, newPosition, blockSpeed * Time.deltaTime);
                //transform.Translate(Vector2.right * blockSpeed * Time.deltaTime);
                rb.MovePosition(position + Vector2.right);
            }
            else if (IsPlayerOnTheRight() && !GetLeftWall)
            {
                Vector2 newPosition = new Vector2(transform.position.x - 1, transform.position.y);
                //transform.position = Vector2.MoveTowards(transform.position, newPosition, blockSpeed * Time.deltaTime);
                rb.MovePosition(newPosition);
            }
        }
    }

    private void GameOver(GameObject block)
    {
        if(block.transform.position.y >= Camera.main.orthographicSize)
        {
            UIManager.instanse.ShowGameOverPanel();
        }
    }
}
