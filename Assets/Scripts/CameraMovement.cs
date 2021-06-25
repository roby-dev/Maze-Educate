using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cameraMain;
    [SerializeField] private CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //cameraMain.transform.position = new Vector3(player.transform.position.x + 10f, cameraMain.transform.position.y, player.transform.position.z);
    }
}
