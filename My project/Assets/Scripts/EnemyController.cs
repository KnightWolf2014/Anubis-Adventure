using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private Transform cynoTransform;
    [SerializeField] private CynoController cynoController;

    [SerializeField] private int enemyID;

    private Animator enemyAnim;

    private float offset_back_distance;
    private Vector3 offset;

    private float smooth;

    // Start is called before the first frame update
    void Start() {
        enemyAnim = GetComponent<Animator>();

        offset_back_distance = 5.0f;

        offset = new Vector3(0, 0, offset_back_distance);

        smooth = 0.05f;
    }

    // Update is called once per frame
    void Update() {

        string current_animation = enemyAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (current_animation != "Run") enemyAnim.CrossFade("Run", 0.2f);

        follow_cyno();

 
    }

    private void follow_cyno() {
        if (cynoTransform != null) {

            float rot = (int) cynoTransform.eulerAngles.y;

            bool cynoHalfLife = cynoController.get_half_life();


            if (rot == 90) {

                offset.x = -offset_back_distance;

                if (!cynoHalfLife) offset.x *= 3;

                    if (enemyID == 0) offset.z = 1;
                else offset.z = -1;

            } else if (rot == 180) {
                offset.z = +offset_back_distance;

                if (!cynoHalfLife) offset.z *= 3;


                if (enemyID == 0) offset.x = 1;
                else offset.x = -1;

            } else if (rot == 270) {
                offset.x = offset_back_distance;

                if (!cynoHalfLife) offset.x *= 3;

                if (enemyID == 0) offset.z = 1;
                else offset.z = -1;

            } else {
                offset.z = -offset_back_distance;

                if (!cynoHalfLife) offset.z *= 3;


                if (enemyID == 0) offset.x = 1;
                else offset.x = -1;
            }

            this.transform.position = Vector3.Lerp(this.transform.position, cynoTransform.position + offset, smooth);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cynoTransform.rotation, smooth);
        }
    }
}
