using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;


public class PlayerSimple : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float tresh = 0.2f;

    [Header("Interaction")]
    public float radius;

    private Rigidbody _rb;
    private Vector2 _movement;
    private Vector2 _currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        _movement = Vector2.zero;
        _currentPosition = new Vector2(transform.position.x, transform.position.y);


        if (Mathf.Abs(xAxis) > tresh)
        {
            if (xAxis > 0f)
            {
                _movement += Vector2.right;
            }
            else
            {
                _movement += Vector2.left;
            }
        }

        if (Mathf.Abs(yAxis) > tresh)
        {
            if (yAxis > 0f)
            {
                _movement += Vector2.up;
            }
            else
            {
                _movement += Vector2.down;
            }
        }

        if (Input.GetKeyDown("e"))
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, Physics.AllLayers, QueryTriggerInteraction.Collide);
            if(hitColliders.Length > 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    IQuestInteraction[] interactions = hitColliders[i].GetComponents<IQuestInteraction>();
                    if(interactions.Length > 0)
                    {
                        for (int j = 0; j < interactions.Length; j++)
                        {
                            interactions[j].Interact();
                        }
                    }
                }
            }
        }

        
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_currentPosition + _movement * speed * Time.deltaTime);
    }


}
