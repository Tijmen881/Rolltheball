using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI CountText;
    public GameObject winTextObject;
    public float jumpHeight = 50;
    public bool isGrounded;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        DisplayCountText();
        winTextObject.SetActive(false);
    }

    // Whenever you input a move, this gets called
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // The counter on the top left which shows how many 'coins' you've collected
    void DisplayCountText()
    {
        CountText.text = "Count: " + count.ToString();  
        if(count >= 29)
        {
            winTextObject.SetActive(true);
        }
    }   

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpHeight);
            }
        }
    }

    // Similar to Update, however this only gets called every frame on which there's a physics calculation
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // Gets called whenever you touch one of the 'coins'
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            DisplayCountText();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }


    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}

