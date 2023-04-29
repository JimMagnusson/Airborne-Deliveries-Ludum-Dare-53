using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject paperPrefab;
    public Transform paperSpawnPos;

    public float throwingSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Paper paper = Instantiate(paperPrefab, paperSpawnPos.position, Quaternion.identity).GetComponent<Paper>();
        paper.Throw(mouseWorldPos - new Vector2(transform.position.x, transform.position.y));
    }
}
