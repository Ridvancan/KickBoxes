using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject hand;
   
    void Start()
    {
        HandAnimation();
    }

   
    void Update()
    {
        
    }
    public void HandAnimationReset()
    {
        hand.transform.localPosition = new Vector2(-400, 101);//Reset position.
    }
    public void HandAnimation()
    {
        HandAnimationReset();
        LeanTween.moveLocalX(hand, hand.transform.localPosition.x + 800, 1f).setLoopPingPong();
    }
}
