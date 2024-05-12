using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour {   

    [SerializeField] private Transform cynoTransform;
    
    private Vector3 offset;

    // Start is called before the first frame update
    void Start() {
        offset = new Vector3(0,7,-10);
        
    }

    // Update is called once per frame
    void Update() {

        if (cynoTransform != null) {

            this.transform.position = cynoTransform.position + offset;
        
        }

    }
}
