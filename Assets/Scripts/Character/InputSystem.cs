using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]

public class InputSystem : MonoBehaviour
{
    Movement moveScript;

    [System.Serializable]
    public class InputSettings
    {
        public string forwardInput = "Vertical";
        public string strafeInput = "Horizontal";
        public string sprintInput = "Sprint";

        public string aim = "Fire2";
        public string fire = "Fire1";
    }

    [SerializeField]
    public InputSettings input;

    [Header("Camera & Charcter Syncing")]
    public float lookDistance = 5;
    public float lookSpeed = 5;

    [Header("Aiming Settings")]
    RaycastHit hit;
    public LayerMask aimLayers;
    Ray ray;

    [Header("Spine Settings")]
    public Transform spine;
    public Vector3 spineOffset;

    [Header("Head Rotation Settings")]
    public float lookAtPoint = 2.8f;

    Transform camCenter;
    Transform mainCam;

    public Bow bowScript;
    bool isAiming;

    public bool testAim;

        void Start()
    {
        moveScript = GetComponent<Movement>();
        camCenter = Camera.main.transform.parent;
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetAxis(input.forwardInput) !=0 || Input.GetAxis(input.strafeInput) !=0)
        {
            RotateToCameView();
        }
        isAiming = Input.GetButton(input.aim);
        if(testAim)
        {
            isAiming = true;
        }
        moveScript.AnimateCharacter(Input.GetAxis(input.forwardInput), Input.GetAxis(input.strafeInput));
        moveScript.SprintCharacter(Input.GetButton(input.sprintInput));
        moveScript.CharacterAim(isAiming);
        

        if(isAiming)
        {
            Aim();
            moveScript.CharacterPullString(Input.GetButton(input.fire));
            if(Input.GetButtonUp(input.fire))
                {
                    moveScript.CharacterFireArrow();
                }
        }
        else
        {
            bowScript.RemoveCrossHair();
            DisableArrow();
            Release();
        }
    }

    void LateUpdate()
    {
        if(isAiming)
        {
            RotateCharactersSpine();
        }
    }

    void RotateToCameView()
    {
        Vector3 camCenterPos = camCenter.position;

        Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDistance);
        Vector3 direction = lookPoint - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        Quaternion finalRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        transform.rotation = finalRotation;
    }

    // Does the aiming and sends a raycast to target
    void Aim()
    {
        Vector3 camPosition = mainCam.position;
        Vector3 dir = mainCam.forward;

        ray = new Ray(camPosition, dir);
        if(Physics.Raycast(ray, out hit, 500f, aimLayers))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            bowScript.ShowCrossHair(hit.point);
        }
        else
        {
            bowScript.RemoveCrossHair();
        }
    }

    void RotateCharactersSpine()
    {
        spine.LookAt(ray.GetPoint(50));
        spine.Rotate(spineOffset);
    }

    public void Pull()
    {
        bowScript.PullString();
    }

    public void EnableArrow()
    {
        bowScript.PickArrow();
    }

    public void DisableArrow()
    {
        bowScript.DisableArrow();
    }

    public void Release()
    {
        bowScript.ReleaseString();
    }
}
