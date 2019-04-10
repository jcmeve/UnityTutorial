using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
        
    }
    
    protected bool Move(int xdir,int ydir,out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xdir, ydir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end , blockingLayer);
        boxCollider.enabled = true;
        if (hit.transform == null)
        {
            Debug.Log("아무것도없땅");
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        Debug.Log("뭐가있네"+this.tag);
        return false;
    }

    protected virtual void AttemptMove<T>(int xdir, int ydir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xdir, ydir, out hit);
        if (hit.transform == null)
            return;
        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    protected abstract void OnCantMove<T>(T component);

}
