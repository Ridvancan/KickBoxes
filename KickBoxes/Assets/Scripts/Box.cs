using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxIndex;
    public GameObject explosionsBox;
    public bool expBox;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>())
        {
            expBox = true;
            other.gameObject.GetComponent<BoxCollider>().enabled=false; 
            GameManager.Instance.ExplosionsProtect();
        }
    }
    public void BoxExplosions()
    {
        GameObject obj = Instantiate(explosionsBox, gameObject.transform);
        obj.transform.parent = null;//Kýrýlan nesneler parenttan cýkarýlýr.
        List<GameObject> brokenBoxes = new List<GameObject>();
        brokenBoxes.Clear();
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            brokenBoxes.Add(obj.transform.GetChild(i).gameObject);
            Rigidbody rb = brokenBoxes[i].GetComponent<Rigidbody>();
            rb.AddExplosionForce(50, Vector3.zero, 10);
        }
        GameManager.Instance.boxList.Remove(gameObject);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 2);
    }
    
}
