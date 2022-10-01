using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsInteracting {get; private set;}
    public List<string> ingredients;

    [SerializeField] private float moveSpeed;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ingredients = new List<string>();
        IsInteracting = false;
    }

    // Update is called once per frame1
    void Update()
    {
        UpdatePlayerInputs();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Ingredient") 
        {
            Debug.Log($"{other.gameObject.name} Grabbed");

            ingredients.Add(other.gameObject.name);
        }
    }

    private void UpdatePlayerInputs() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        IsInteracting = Input.GetKey(KeyCode.E);
    }
}
