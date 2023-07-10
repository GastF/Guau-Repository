using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{

   
    public Animator buttonAnimator;

   
     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.Z))
         {
             buttonAnimator.SetTrigger("Z");
         }
         else if (Input.GetKeyDown(KeyCode.X))
         {
             buttonAnimator.SetTrigger("X");
         }
         else if (Input.GetKeyDown(KeyCode.C))
         {
             buttonAnimator.SetTrigger("C");
         }
         if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
         {
             buttonAnimator.SetTrigger("Up");
         }
         else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
         {

             buttonAnimator.SetTrigger("Left");
         }
         else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
         {
             buttonAnimator.SetTrigger("Right");
         }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {

            buttonAnimator.SetTrigger("DiagonalLeft");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            buttonAnimator.SetTrigger("DiagonalRight");
        }
    }

}

 
