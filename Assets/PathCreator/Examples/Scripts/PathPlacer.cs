using PathCreation;
using UnityEngine;

namespace PathCreation.Examples {

    [ExecuteInEditMode]
    public class PathPlacer : PathSceneTool {

        public GameObject prefab;
        public GameObject holder;
        public float spacing = 3;

        public Vector3 scala= new Vector3(0,0,0);
        public Vector3 orientacion=new Vector3(0,0,0);
        public Vector3 posicion=new Vector3(0,0,0);
        Vector3 point;
        Vector3 normal;
        Vector3 direction;
        /*
        public float alpha;
        public float beta;
        public float gamma;

        public float deltaX;
        public float deltaY;
        public float deltaZ;
        */
        

        const float minSpacing = .1f;

        void Generate () {
            if (pathCreator != null && prefab != null && holder != null) {
                DestroyObjects ();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;

                while (dst < path.length) {
                    point = path.GetPointAtDistance (dst);

                    normal=path.GetNormalAtDistance(dst);
                    direction=path.GetDirectionAtDistance(dst);
                    Debug.Log(normal);
                    point.x+=normal.x*posicion.z+direction.x*posicion.y;
                    point.y+=normal.y*posicion.z+direction.y*posicion.y;
                    point.z+=normal.z*posicion.z+direction.z*posicion.y;
                    //point+=posicion;
                    Quaternion rot = path.GetRotationAtDistance (dst);
                    rot.eulerAngles+=orientacion;
                    GameObject newObject=Instantiate (prefab, point, rot, holder.transform);
                    newObject.transform.localScale = scala;
                    dst += spacing;
                }
            }
        }

        void Update(){
            PathUpdated();
        }
        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}