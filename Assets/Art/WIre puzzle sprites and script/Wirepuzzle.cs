using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Wirepuzzle : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        // intial wire position
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // check if connection is nearby
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);

                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    Finish.Instance.WireChange(1);


                    collider.GetComponent<Wirepuzzle>()?.Done();
                    Done();
                }



                return;
            }
        }

        // updating wire 
        UpdateWire(newPosition);

    }


    void Done()
    {
        lightOn.SetActive(true);

        Destroy(this);
    }

    private void OnMouseUp()
    {
        //resets wire position
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
        // updating wire position
        transform.position = newPosition;

        // update wire direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        // update wire scale
        float space = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(space, wireEnd.size.y);
    }
}
