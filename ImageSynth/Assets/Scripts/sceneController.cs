using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    [SerializeField] private ImageSynthesis synth;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int minObjects = 4;
    [SerializeField] private int maxObjects = 5;
	private string save_results = @"/home/prithvi/Documents/git/image_synthesis_using_unity/results.csv";
    // Camera cam;
    // private GameObject[] createdObjects;
    private ShapePool pool;
    private int frameCount = 0;

   Renderer rend;
    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    void OnDrawGizmosSelected()
    {
        // A sphere that fully encloses the bounding box.
        Vector3 center = rend.bounds.center;
        float radius = rend.bounds.extents.magnitude;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center, radius);
    } 
    // Start is called before the first frame update
    void Start()
    {
        // createdObjects = new GameObject[maxObjects];
        pool = ShapePool.Create(prefabs);
        try{
            if (System.IO.File.Exists(save_results))    
            {    
    	        System.IO.File.Delete(save_results);    
            }    
        }
        catch(System.Exception Ex){
            Debug.Log(Ex.ToString());
        }
        // rend = GetComponent<Renderer>();
        // // cam = GetComponent<Camera>();
        
        // GenerateRandom();
        // string filename = $"image_{frameCount.ToString().PadLeft(5, '0')}";
        // synth.Save(filename, 512, 512, "captures");
        // // Debug.Log(pool.pools);

        // string result = "List contents: ";
        // // foreach (var item in pool.pools)
        // // {
        // //     result += "\n" + item.ToString() + ", ";
        // //     // Debug.Log(item.GetType()+" \n");
        // // }
        // // foreach (KeyValuePair<ShapeLabel, List<Shape>> kvp in pool.pools)
        // // {
        // //     // Debug.Log( kvp.Key.ToString());
        // //     foreach (var item in kvp)
        // //     {
        // //     result += "\n" + item.ToString() + ", ";
        // //     }
        // // }
        // // Debug.Log(result);
		// Object[] allObjects = Object.FindObjectsOfType<ShapePool>();
		// // // Renderer renderer = Object.FindObjectsOfType<Renderer>();
		// // var renderers = Object.FindObjectsOfType<Renderer>();
        // // // Debug.Log(renderers.GetMaterials());
        // // foreach (var r in renderers)
		// // {
		// // 	var id = r.gameObject.GetInstanceID();
		// // 	var layer = r.gameObject.layer;
		// // 	var tag = r.gameObject.tag;
        // //     var tf = r.gameObject.transform.position;
        // //     Vector3 screenPos = cam.WorldToScreenPoint(tf);
        // //     // var abc = r.gameObject.GetMaterials();
        // //     Debug.Log( "target is " + tf.x + " pixels from the left for "+tf.ToString() );
        // // }

        // foreach (ShapePool go in allObjects)
        // {
        //     // Debug.Log(string.Join(", ", go.active.GetType() ));
        //     foreach (var item in go.active)
        //     {
        //         // obj =  item.obj;
        //         int someLabel = (int)item.label;
        //         result += "\n" + someLabel.ToString()+", "+ item.obj.transform.position.ToString() + ", ";
        //     }
        //     // Debug.Log(result);

        // //     // // Debug.Log(go.pools.ToString());// + " is an active object " + go.transform.ToString());
        // //     // Debug.Log(string.Join(", ", go.pools.pools));
            
        // //     // foreach (KeyValuePair<ShapeLabel, List<Shape>> sp in go.pools)  
        // //     // {  
        // //     //     // Console.WriteLine("Key: {0}, Value: {1}",  
        // //     //     // author.Key, author.Value);  
        // //     //     // Debug.Log(sp.Key.ToString());
        // //     //     foreach (var item in sp.Value)
        // //     //     {
        // //     //         result += "\n "+ sp.Key.ToString() + string.Join(", ", sp.Value) + ", ";
        // //     //     }
        // //     //     Debug.Log(result);
                
        //     // }
        

        // }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount % 75 == 0){
            string filename = $"image_{frameCount.ToString().PadLeft(5, '0')}";
            GenerateRandom(filename);
            synth.Save(filename, 512, 512, "captures", 1); //specific pass 
            // specific pass; 1 _id, 2- layer, 3 depth, 4 normals, 5 flow, -1 for all
        }
        frameCount++;
    }

    void GenerateRandom(string filename){

        // for (int i=0; i < createdObjects.Length; i++){
        //     if (createdObjects[i] != null){
        //         Destroy(createdObjects[i]);
        //     }
        // }
        pool.ReclaimAll();
        int objectsThisTime = Random.Range(minObjects, maxObjects);

        for (int i=0; i<objectsThisTime; i++){

            int prefabIndex = Random.Range(0, prefabs.Length);
            GameObject prefab  = prefabs[prefabIndex];

            //Position
            float newX, newY, newZ;
            newX = Random.Range(-20.0f, 20.0f);
            newY = Random.Range(2.0f, 30.0f);
            newZ = Random.Range(-20.0f, 20.0f);
            Vector3 newPos = new Vector3(newX, newY, newZ);

            // Rotation
            var newRot = Random.rotation;
            // var newObj = Instantiate(prefab, newPos, newRot);
            var shape = pool.Get((ShapeLabel)prefabIndex);
            var newObj = shape.obj;
            newObj.transform.position = newPos;
            newObj.transform.rotation = newRot;

            //Scale
            float sc = Random.Range(1.5f, 4.2f);
            Vector3 newScale = new Vector3(sc, sc, sc);
            newObj.transform.localScale = newScale;

            //Color
            float newR, newG, newB;
            newR = Random.Range(0.0f, 1.0f);
            newG = Random.Range(0.0f, 1.0f);
            newB = Random.Range(0.0f, 1.0f);
            var newColor = new Color(newR, newG, newB);
            newObj.GetComponent<Renderer>().material.color=newColor;
            // Access game object's renderer componenent. Then we are accessing its material 
            //  and setting the color of it as random new color.


        }
    
        synth.OnSceneChange(filename, save_results);
    }
}
