using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject paperPrefab;

    public float throwingSpeed = 2f;
    private PlayerMovementController playerMovementController;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            HandlePaperThrowing();
        }
    }

    private void HandlePaperThrowing() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldpos3D = Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 mouseWorldPos = new Vector3(mouseWorldpos3D.x,mouseWorldpos3D.y);
        // Throw paper toward cursor position
        Paper paper = Instantiate(paperPrefab, transform.position, Quaternion.identity).GetComponent<Paper>();

        Vector2 playerToCursor = (mouseWorldPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        paper.Throw(playerToCursor, throwingSpeed);
        playerMovementController.LaunchToward(-playerToCursor);
    }
}
