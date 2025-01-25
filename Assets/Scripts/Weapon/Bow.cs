using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bow : MonoBehaviour 
{
    [System.Serializable]
    public class BowSettings
    {
        [Header("Arrow Settings")]
        public float arrowCount;
        public GameObject arrowPrefab;
        public Transform arrowPos; 
        public Transform arrowEquipParent;

        [Header("Bow Equip & UnEquip Settings")]
        public Transform EquipPos;
        public Transform UnEquipPos;

        public Transform UnEquipParent;
        public Transform EquipParent;

        [Header("Bow String Settings")]
        public Transform bowString;
        public Transform stringInitialPos;
        public Transform stringHandPullPos;
        public Transform stringInitialParent;


    }
    [SerializeField]
    public BowSettings bowSettings;

    [Header("Crosshair Settings")]
    public GameObject crossHairPrefab;
    GameObject currentCrossHair;

    GameObject currentArrow;

    bool canPullString = false;
    bool canFireArrow = false;


    void Start()
    {

    }

    void Update()
    {
        if(canPullString)
        {

        }
    }

    public void PickArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(true);
    }

    public void DisableArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(false);
    }

    public void PullString()
    {
        bowSettings.bowString.transform.position = bowSettings.stringHandPullPos.position;
        bowSettings.bowString.transform.parent = bowSettings.stringHandPullPos;
    }

    public void ReleaseString()
    {
        bowSettings.bowString.transform.position = bowSettings.stringInitialPos.position;
        bowSettings.bowString.transform.parent = bowSettings.stringInitialParent; 
    }

    void EquipBow()
    {
        this.transform.position = bowSettings.EquipPos.position;
        this.transform.rotation = bowSettings.EquipPos.rotation;
        this.transform.parent = bowSettings.EquipParent;
    }

    void UnEquipBow()
    {
        this.transform.position = bowSettings.UnEquipPos.position;
        this.transform.rotation = bowSettings.UnEquipPos.rotation;
        this.transform.parent = bowSettings.UnEquipParent;
    }

    public void ShowCrossHair(Vector3 crossHairPos)
    {
        if(!currentCrossHair)
        {
            currentCrossHair = Instantiate(crossHairPrefab) as GameObject;
        }
        currentCrossHair.transform.position = crossHairPos;
        currentCrossHair.transform.LookAt(Camera.main.transform);
    }
    public void RemoveCrossHair()
    {
        if(currentCrossHair)
        {
            Destroy(currentCrossHair);
        }
    }

}
