using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject PickUpPanel;
    [SerializeField] private GameObject Item;
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        PickUpPanel = GameObject.Find("CameraPlayer").GetComponent<Cam>().PickUpPanel;
        PickUpPanel.SetActive(false);
        Item = null;
    }

    private void Update()
    {
        if (Item != null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.Add(Item.transform.gameObject.GetComponent<Items>().dataItem);
            Destroy(Item.transform.gameObject);
            PickUpPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("qqch");
        if (other.CompareTag("Item"))
        {
            Debug.Log("item");
            PickUpPanel.GetComponentInChildren<Text>().text = "Pick Up";
            Item = other.gameObject;
            PickUpPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item = null;
            PickUpPanel.SetActive(false);
        }
    }
}
