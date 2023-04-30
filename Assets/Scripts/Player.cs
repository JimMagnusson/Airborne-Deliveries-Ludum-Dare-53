using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject paperPrefab;

    [SerializeField] private Sprite bagFilledPlayerSprite;

    [SerializeField] private Sprite bagEmptyPlayerSprite;

    [SerializeField] private float throwingSpeed = 2f;

    [SerializeField] private float paperFallMultiplier = 2.5f;

    [SerializeField] private float lineWidth = 0.1f;

    [SerializeField] private LineRenderer lineRenderer;
    private PlayerMovementController playerMovementController;
    private LevelSettings levelSettings;
    private int papersLeft;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private bool limitPapers = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        levelSettings = FindObjectOfType<LevelSettings>();
        papersLeft = levelSettings.GetNumberOfPapers();
        lineRenderer.material.mainTextureScale = new Vector2(1f / lineWidth, 1.0f);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (!limitPapers || papersLeft > 0)) {
            HandlePaperThrowing();
        }

        // TODO: refactor
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldpos3D = Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 mouseWorldPos = new Vector3(mouseWorldpos3D.x,mouseWorldpos3D.y);
        Vector2 playerToCursor = (mouseWorldPos - new Vector2(transform.position.x, transform.position.y)).normalized;

        DrawTrajectory(playerToCursor*throwingSpeed);
    }

    private void HandlePaperThrowing() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldpos3D = Camera.main.ScreenToWorldPoint(mousePos);  
        Vector2 mouseWorldPos = new Vector3(mouseWorldpos3D.x,mouseWorldpos3D.y);
        // Throw paper toward cursor position
        Paper paper = Instantiate(paperPrefab, transform.position, Quaternion.identity).GetComponent<Paper>();

        Vector2 playerToCursor = (mouseWorldPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        paper.Throw(playerToCursor, throwingSpeed, paperFallMultiplier);
        playerMovementController.LaunchToward(-playerToCursor);

        if(limitPapers) {
            papersLeft--;
            if(papersLeft <= 0) {
                spriteRenderer.sprite = bagEmptyPlayerSprite;
            }
        }
        
    }

    /*
    if(rb.velocity.y < 0) {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
    */

    void DrawTrajectory(Vector2 velocity)
    {
        List<Vector2> simulatedArc = SimulateArc(velocity);
        lineRenderer.positionCount = simulatedArc.Count;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, simulatedArc[i]); //Add each Calculated Step to a LineRenderer to display a Trajectory. Look inside LineRenderer in Unity to see exact points and amount of them
        }
    }

    private List<Vector2> SimulateArc(Vector2 velocity) {
        List<Vector2> lineRendererPoints = new List<Vector2>(); //Reset LineRenderer List for new calculation

        float maxDuration = 1f; //INPUT amount of total time for simulation
        float timeStepInterval = 0.1f; //INPUT amount of time between each position check
        int maxSteps = (int)(maxDuration / timeStepInterval);//Calculates amount of steps simulation will iterate for

        for (int i = 0; i < maxSteps; ++i)
        {
            //Remember f(t) = (x0 + x*t, y0 + y*t - 9.81t²/2)
            //calculatedPosition = Origin + (transform.up * (speed * which step * the length of a step);
            Vector2 calculatedPosition = velocity * i * timeStepInterval; //Move both X and Y at a constant speed per Interval
            calculatedPosition.y += Physics2D.gravity.y/2 * Mathf.Pow(i * timeStepInterval, 2); //Subtract Gravity from Y

            lineRendererPoints.Add(calculatedPosition); //Add this to the next entry on the list

            /*
            if (CheckForCollision(calculatedPosition)) //if you hit something, stop adding positions
            {
                break; //stop adding positions
            }
            */
        }
        return lineRendererPoints;
    }
    
}
