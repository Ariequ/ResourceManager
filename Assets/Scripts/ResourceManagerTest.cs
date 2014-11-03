using UnityEngine;
using System.Collections;

public class ResourceManagerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obj = ResourceManager.load("Scenes/Scene_1/Capsule", typeof(GameObject)) as GameObject;
		Instantiate(obj);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
