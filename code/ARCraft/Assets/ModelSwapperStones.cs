using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ModelSwapperStones : MonoBehaviour
{
    public Vuforia.TrackableBehaviour theTrackable;
    public Mesh objectToCreate;
    private bool mSwapModel = false; // this is public because of its use in other scripts
    public int count = 0; // this is public because of its use in other scripts

    // Use this for initialization
    void Start()
    {
        if (theTrackable == null)
        {
            Debug.Log("Warning: Trackable not set !!");
        }

        //disable any pre-existing augmentation
        GameObject trackableGameObject = theTrackable.gameObject;
        for (int i = 0; i < trackableGameObject.transform.childCount; i++)
        {
            Transform child = trackableGameObject.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }

        // define instruction field parameters
        float rectangle_width = Screen.width / 3;
        float rectangle_height = Screen.height / 6;

        // specify instruction field background
        GameObject go_myRect = GameObject.Find("InstructionTextImage");
        RectTransform tr_myRect = go_myRect.GetComponent<RectTransform>();
        tr_myRect.sizeDelta = new Vector2(rectangle_width, rectangle_height);
        tr_myRect.anchoredPosition = new Vector3(0, -rectangle_height/2, 0);

        // specify instruction field text
        GameObject go_myInstr = GameObject.Find("InstructionText");
        RectTransform tr_myInstr = go_myInstr.GetComponent<RectTransform>();
        tr_myInstr.sizeDelta = new Vector2(rectangle_width-10, rectangle_height-5);
        tr_myInstr.anchoredPosition = new Vector3(0, -rectangle_height / 2, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (mSwapModel && theTrackable != null)
        {
            SwapModel();
            mSwapModel = false;
        }
    }
    void OnGUI()
    {
        float button_width = Screen.width / 5;
        float button_height = Screen.height / 5;

        if (GUI.Button(new Rect(Screen.width-button_width, Screen.height-button_height, button_width, button_height), "Weiter"))
        {
            mSwapModel = true;
            if (count <= 8){
                count = count + 1;
            }
            else{
                
            }

        }

        if (GUI.Button(new Rect(0, 0, button_width, button_height), "Zurück"))
        {
            mSwapModel = true;
            if (count > 0)
            {
                count = count - 1;
            }
            else
            {

            }
        }

    }
    void createCube(float x, float y, float z)
    {
        // Create a simple cube object
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Re-parent the cube as child of the trackable gameObject
        cube.transform.parent = theTrackable.transform;
        // Adjust the position and scale
        // so that it fits nicely on the target
        cube.transform.localPosition = new Vector3(x, y, z);
        cube.transform.localRotation = Quaternion.identity;
        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        // Make sure it is active
        cube.SetActive(true);
    }
    void destroyCurrentModelSetup()
    {
        GameObject trackableGameObject = theTrackable.gameObject;
        //disable any existing augmentation
        for (int i = 0; i < trackableGameObject.transform.childCount; i++)
        {
            Transform child = trackableGameObject.transform.GetChild(i);
            child.gameObject.SetActive(false);
            Destroy(child.gameObject, .5f);
        }
    }
    void put_currentShapeInScene(Vector3 AR_shape_position, Quaternion AR_shape_rotation, Vector3 AR_shape_scale, string mesh_title, string path_mesh_shape, string path_mesh_material)
    {
        // sub-meshes handling with a proper ObjLoader (see https://gist.github.com/supachailllpay/893cd5b0c31dff3bb025)
        // Load and introduce that shape in current scene
        GameObject go = new GameObject(mesh_title);
        go.transform.parent = theTrackable.transform;
        Mesh mesh = (Mesh)Resources.Load(path_mesh_shape, typeof(Mesh));

        Debug.Log("Submeshes: " + mesh.subMeshCount);

        Material material = Resources.Load(path_mesh_material, typeof(Material)) as Material;
        MeshFilter meshFilter = go.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.material = material;

        // Adjust the position and scale so that it fits nicely on the target
        go.transform.localPosition = AR_shape_position;
        go.transform.localRotation = AR_shape_rotation;
        go.transform.localScale = AR_shape_scale;

    }
    string get_meshShape(string xml_shape_name)
    {
        string current_path_mesh_shape = null;
        if (string.Compare(xml_shape_name, "Activity") == 0) { current_path_mesh_shape = "KMDL/Activity/Activity"; }
        else if (string.Compare(xml_shape_name, "KnowledgeObject") == 0) { current_path_mesh_shape = "KMDL/ToBeInserted/ToBeInserted"; }
        else if (string.Compare(xml_shape_name, "Schritt_1") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_1"; }
        else if (string.Compare(xml_shape_name, "Schritt_1_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_1_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_2_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_2_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_3_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_3_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_4_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_4_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_5_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_5_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_6_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_6_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_7_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_7_ManuallyDeletedMeshGroups"; }
        else if (string.Compare(xml_shape_name, "Schritt_8_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "MethodSchrank/Schritt_8_ManuallyDeletedMeshGroups"; }

        // furhter shapes ...

        return current_path_mesh_shape;
    }
    string get_meshMaterial(string xml_shape_name)
    {
        string current_path_mesh_shape = null;
        if (string.Compare(xml_shape_name, "Activity") == 0) { current_path_mesh_shape = "Materials/Glass_(Green)"; }
        else if (string.Compare(xml_shape_name, "KnowledgeObject") == 0) { current_path_mesh_shape = "Materials/ToBeInserted"; }
        else if (string.Compare(xml_shape_name, "Schritt_1") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_1_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_2_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_3_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_4_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_5_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_6_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_7_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        else if (string.Compare(xml_shape_name, "Schritt_8_ManuallyDeletedMeshGroups") == 0) { current_path_mesh_shape = "Materials/Weiss"; }
        // furhter shapes ...

        return current_path_mesh_shape;
    }
    private void SwapModel()
    {
        //disable any pre-existing augmentation
        GameObject trackableGameObject = theTrackable.gameObject;
        for (int i = 0; i < trackableGameObject.transform.childCount; i++)
        {
            Transform child = trackableGameObject.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }

        // Identify single shape of current XML file
        Vector3 AR_shape_position = new Vector3(0.0f, -0.24f, 0.0f); // Position
        Quaternion AR_shape_rotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Winkel
        Vector3 AR_shape_scale = new Vector3(0.042f, 0.042f, 0.042f); // Skalierung

        if (count == 1) // initialize with setup 0 (current xml)
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_1");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            //AR_shape_position.Set(0.0f, -0.02f, 0.0f); // Position
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Bitte installieren Sie gezeigte acht Schrauben (insgesamt 4 in jeder Seitenwand)!";
        }

        if (count == 2) // initialize with setup 1 (Squares right) -> menu
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_2");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Bitte installieren Sie gezeigte beide Winkel, jeden mit insgesamt drei Schrauben!";
        }
        if (count == 3) // initialize with setup 1 (Squares right) -> menu
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_3");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Stecken Sie die Kopf- und Fußteile auf die Seitenwand, vergessen Sie nicht gezeigte 4 Holzdübel!";
        }
        if (count == 4) // initialize with setup 1 (Squares right) -> menu
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_4");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Bitte drehen Sie nun gezeigte vier Schrauben fest (insgesamt 2 je Teil)!";
        }
        if (count == 5) // initialize with setup 1 (Squares right) -> menu
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_5");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Bitte schieben Sie die Rückwand in die gezeigten Führungsschienen!";
        }
        if (count == 6) // initialize with setup 1 (Squares right) -> menu
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_6");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Stecken Sie die zweite Seitenwand auf die Kopf- und Fußteile. Vergessen Sie nicht gezeigte 4 Holzdübel!";
        }
        if (count == 7) // initialize with setup 1 (Squares right) -> menu
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_7");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Bitte drehen Sie gezeigte vier Schrauben fest (insgesamt 2 je Teil)!";
        }
        if (count == 8) // initialize with setup 2 (Squares left) -> shapes in overview
        {
            // create model setup
            Transform go = trackableGameObject.transform.Find("Schritt_8");
            go.gameObject.SetActive(true);

            // Adjust the position and scale so that it fits nicely on the target
            go.transform.localPosition = AR_shape_position;
            go.transform.localRotation = AR_shape_rotation;
            go.transform.localScale = AR_shape_scale;

            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Kippen Sie den Schrank nach vorne und schlagen Sie gezeigte 5 Nägel mit dem Hammer von oben in den Schrank!";
        }
        if (count == 9) // initialize with setup 2 (Squares left) -> shapes in overview
        {
            // adjust instruction text
            GameObject tempObject = GameObject.Find("InstructionText");
            Text myText = tempObject.GetComponent<Text>();
            myText.text = "Vielen Dank für Ihre Teilnahme am Aufbau-Experiment!";
        }
    }
}