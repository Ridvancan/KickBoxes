using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    int _kickCounter=0;
    public void Kick()
    {
        if (_kickCounter!=GameManager.Instance.boxList.Count)
        {
           Player.Instance.FinalAnimation();
           Player.Instance.FinalKickBoxesAnimation(_kickCounter);
            _kickCounter++;
            GameManager.Instance.PlayParticle();
        }
    }
    public void KickProtect()
    {
        if (_kickCounter == GameManager.Instance.boxList.Count)
        {
            Player.Instance.FinalDanceAnimation();
        }
    }
}
