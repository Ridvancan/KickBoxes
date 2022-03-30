using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public Enums.GameStatus gameStatus;
    public GameObject boxPrefab;
    public List<GameObject> boxList = new List<GameObject>();
    public Transform parentBox;
    public Transform explosionsBoxParent;
    public GameObject handObject;
    public GameObject finalCamera;
    public GameObject playerCamera;
    public Vector3 particalStartPoint;
    public GameObject particalExplosion;

    public void PlayButton()
    {
        if (gameStatus == Enums.GameStatus.mainMenu)
        {
            gameStatus = Enums.GameStatus.inGame;
            Player.Instance.StartGameAnimation();
            handObject.SetActive(false);
        }
    }
    public void PlayParticle()
    {
        GameObject tempPartical = Instantiate(particalExplosion);
        tempPartical.transform.position = particalStartPoint;
        Destroy(tempPartical, 1f);
            //TODO Particul de light kullanmamak -> optimizasyon
    }
    public void ChangeCamera()
    {
        finalCamera.SetActive(true);
        playerCamera.SetActive(false);
    }
    public void BoxCreator()
    {
        GameObject obj = null;

        if (boxList.Count == 0)
        {
            obj = Instantiate(boxPrefab, parentBox);
        }
        else
        {
            Transform parentTransform = boxList[boxList.Count - 1].transform;
            obj = Instantiate(boxPrefab, parentTransform);
            obj.transform.localScale = new Vector3(2, 2, 2);
            LeanTween.scale(obj, Vector3.one, 0.4f).setEase(LeanTweenType.easeOutBack);
        }

        if (boxList.Count < 1)
        {
            obj.transform.localPosition = Vector3.zero;
        }
        
        boxList.Add(obj);
    }
    public void BoxPositioner()
    {
        for (int i = 0; i < boxList.Count; i++)
        {
            if (i == 0)
            {
                boxList[i].transform.localPosition = CharacterController.Instance.mainObject.transform.localPosition;
                boxList[i].transform.localRotation = CharacterController.Instance.mainObject.transform.localRotation;
            }
            else
            {
                boxList[i].transform.localPosition = Vector3.Lerp(boxList[i].transform.localPosition, new Vector3(boxList[i - 1].transform.localPosition.x, 1, boxList[i].transform.localPosition.z), Time.deltaTime / (0.025f * 3.5f));
                boxList[i].transform.localRotation = Quaternion.Slerp(boxList[i].transform.localRotation, boxList[i - 1].transform.localRotation, Time.deltaTime / (0.025f * 3.5f));
            }
        }
    }

    bool _protectBox;
    bool ProtectBox()
    {
        if (_protectBox)
        {
            return false;
        }
        else
        {
            _protectBox = true;
        }
        return true;
    }
    public void ExplosionsProtect()
    {
        int tempIndex = 0;
        for (int i = 0; i < boxList.Count; i++)
        {
            if (boxList[i].GetComponent<Box>().expBox)
            {
                tempIndex = i;
                break;
            }
        }

        for (int i = boxList.Count - 1; i > tempIndex - 1; i--)
        {
            boxList[i].GetComponent<Box>().BoxExplosions();
        }
    }
    
    private void Update()
    {
        if (gameStatus==Enums.GameStatus.inGame)
        {
            BoxPositioner();
        }
    }
}
