
using UnityEngine;
public class CamaraController : MonoBehaviour {   


    [SerializeField] private Transform cynoTransform;
    
    private float offset_back_distance;
    private float offset_up_distance;
    private Vector3 offset;

    private float smooth;

    // Start is called before the first frame update
    void Start() {
        offset_back_distance = 12.0f;
        offset_up_distance = 5.0f;

        offset = new Vector3(0, offset_up_distance, 0);

        smooth = 0.05f;
    }

    // Update is called once per frame
    void Update() {

        if (cynoTransform != null) {

            float rot = (int) cynoTransform.eulerAngles.y;

            if (rot == 90) {
                offset.z = 0;
                offset.x = -offset_back_distance; ;

            } else if (rot == 180) {
                offset.z = +offset_back_distance;
                offset.x = 0;

            } else if (rot == 270) {
                offset.z = 0;
                offset.x = offset_back_distance;

            } else {
                offset.z = -offset_back_distance;
                offset.x = 0;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, cynoTransform.position + offset, smooth);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cynoTransform.rotation, smooth);
        }

    }
}
