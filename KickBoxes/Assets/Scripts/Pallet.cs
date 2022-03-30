using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : Singleton<Pallet>
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectableBox>())
        {
            GameManager.Instance.BoxCreator();
            Destroy(other.gameObject);      
        }
        if (other.GetComponent<FinalChase>())
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            GameManager.Instance.gameStatus = Enums.GameStatus.finalAnimation;
            Player.Instance.FinalAnimation();
        }
    }
}
