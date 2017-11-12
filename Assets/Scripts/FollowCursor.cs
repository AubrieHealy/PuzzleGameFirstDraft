using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    [SerializeField]
    public int maxNumClicks;

    public int clickCounter; 

    private float distance = 3.0f ;

    private Quaternion originalTransform;
    private Quaternion tempRotation;

    public GameObject TextParentWall; //3D text wall 
    public TextMesh counterDisplay; //Number to display

    // Use this for initialization
    void Start () {
        Cursor.visible = true; //To Show the cursor for 
        originalTransform = this.transform.rotation;
        clickCounter = 0;
        counterDisplay.transform.parent = TextParentWall.transform;
        counterDisplay.transform.position = TextParentWall.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        CursorFollow();
        counterDisplay.text = (maxNumClicks - clickCounter).ToString(); 
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit; 

            if(Physics.Raycast(ray,out hit))
            {
                Debug.Log(hit.collider.name);
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.name == "mirror")
                    {
                        if (clickCounter < maxNumClicks)
                        {
                            clickCounter = clickCounter + 1;
                        }
                        else
                        {
                            //End game splash screen 
                            MainCameraController.ShakeCameraWin();

                        }

                    }
                }
            }

            var x = this.transform.rotation.x;
            var z = this.transform.rotation.z;

            tempRotation = Quaternion.Euler(-59f, 0f, 24f);
            this.transform.rotation = tempRotation;
        }
        else
            this.transform.rotation = originalTransform; 
    }

    void CursorFollow()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var point = ray.origin + (ray.direction * distance);
        this.transform.position = point; 
    }

}
