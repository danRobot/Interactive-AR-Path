using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        //public Transform[] waypoints;
        public List<Transform> waypoints = new List<Transform>();
        public Transform myPrefab;
        public GameObject scene;
        BezierPath bezierPath;
        Vector3 pointPos;
        RaycastHit hit;
        int i=0;
        public int numbers;
        void Starte () {
            if (waypoints.Count > 0) {
                // Create a new bezier path from the waypoints.
                 bezierPath= new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
            scene.SetActive(false);
        }
        
        
        void Update(){
            //Debug.Log(bezierPath.NumAnchorPoints);
            //Debug.Log(i);
            if(Input.GetMouseButtonDown(0)){
                //Debug.Log(Input.mousePosition);
                RaycastHit hit;
                Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                if (Physics.Raycast(ray, out hit,100)){
                    pointPos = hit.point;
                    //Debug.Log(hit.point);
                    //transform.position = pointPos;
                }
                //Debug.Log(waypoints);
                
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = pointPos;
                //Instantiate(myPrefab, pointPos, Quaternion.identity);

                waypoints.Add(Instantiate(myPrefab, pointPos, Quaternion.identity));
                for(int j=0;j<waypoints.Count;j++){
                    //waypoints[j].position=pointPos.position
                    Debug.Log(waypoints[j].position);
                }
                //bezierPath.AddSegmentToEnd(pointPos);
                //Debug.Log(bezierPath.NumAnchorPoints);
                //closedLoop=!closedLoop;
                if (waypoints.Count > 3) {
                    bezierPath=new BezierPath (waypoints,closedLoop,PathSpace.xyz);
                    //GetComponent<PathCreator> ().bezierPath = bezierPath;
                    //Vector3 ve=new Vector3(0.0f, 0.0f, 0.0f);
                    //bezierPath.MovePoint(0,ve);
                    i++;
                    GetComponent<PathCreator> ().bezierPath = bezierPath;
                    numbers=bezierPath.NumAnchorPoints;
                    scene.SetActive(true);
                }
            }
            if (Input.GetMouseButtonDown(1)){
                
                bezierPath.MovePoint(0,pointPos);}
            if (Input.GetMouseButtonDown(2))
                Debug.Log("Pressed middle click.");
        }
        
        void Start(){
            pointPos=new Vector3(0.0f, 0.0f, 0.0f);
            
            //GameObject plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Starte();
        }
    }
}
