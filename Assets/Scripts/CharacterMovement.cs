using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
   
    private bool isMoving;
    private Vector2 input;
    private bool isCollidingWithBoundary = false; // Flag to track collision with boundary

    private void Start()
   {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0f);
                StartCoroutine(Move(targetPos));

                // Set animation parameters based on movement direction
                animator.SetFloat("Horizontal", input.x);
                animator.SetFloat("Vertical", input.y);
                animator.SetBool("isMoving", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                // If not moving, play idle animation
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", false);
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        if (!isCollidingWithBoundary)
        {
            isMoving = true;
            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;

            isMoving = false;
        } else
        {
            transform.position = Vector3.zero;
        }
    }

    // Called when the player enters a trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Debug.Log("Player collides with boundary");
            isCollidingWithBoundary = true;
        }
    }

    // Called when the player exits a trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            Debug.Log("Player exits boundary");
            isCollidingWithBoundary = false;
        }
    }
}