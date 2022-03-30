using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : Singleton<Player>
{
    Animator playerAnimator;
    public GameObject palet;
 
  
    void Start()
    {
        playerAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    public void StartGameAnimation()
    {
        playerAnimator.SetBool("Run", true);
        
    }
   
    public void FinalAnimation()
    {
        LeanTween.moveLocal(gameObject, new Vector3(-1, 0, 125), 1.5f);
        GameManager.Instance.ChangeCamera();
        foreach (var item in GameManager.Instance.boxList)
        {
            LeanTween.rotateLocal(item, Vector3.zero, 0.5f);
        }

        StartCoroutine(StartFinalAnimation());
    }
    IEnumerator StartFinalAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        palet.SetActive(false);
        playerAnimator.SetBool("FinalKick", true);
    }

    List<Vector3> finalBoxesCordinations = new List<Vector3>();
    public void FinalKickBoxesAnimation(int indexOfBox)
    {
        finalBoxesCordinations.Add(new Vector3(-1, 11.4f, 135));
        finalBoxesCordinations.Add(new Vector3(0, 11.4f, 135));
        finalBoxesCordinations.Add(new Vector3(1, 11.4f, 135));
        finalBoxesCordinations.Add(new Vector3(1, 12.4f, 135));
        finalBoxesCordinations.Add(new Vector3(0, 12.4f, 135));
        finalBoxesCordinations.Add(new Vector3(-1, 12.4f, 135));
        finalBoxesCordinations.Add(new Vector3(1, 13.4f, 135));
        finalBoxesCordinations.Add(new Vector3(0, 13.4f, 135));
        finalBoxesCordinations.Add(new Vector3(-1, 13.4f, 135));
        foreach (var item in GameManager.Instance.boxList)
        {
            item.transform.parent = null;
        }
       
        LeanTween.moveLocal(GameManager.Instance.boxList[indexOfBox], finalBoxesCordinations[indexOfBox], 0.5f);
        LeanTween.rotateLocal(GameManager.Instance.boxList[indexOfBox], Vector3.zero, 0);
        DropBoxes();
    }
    public void DropBoxes()
    {
        foreach (var item in GameManager.Instance.boxList)
        {
            if (item.transform.localPosition.z < 135)
            {
                LeanTween.moveLocalY(item, item.transform.localPosition.y - 0.80f, 0.2f);
            }
        }
    }
    public void FinalDanceAnimation()
    {
        playerAnimator.SetBool("FinalKick", false);
        playerAnimator.SetBool("FinalDance", true);
    }
}
