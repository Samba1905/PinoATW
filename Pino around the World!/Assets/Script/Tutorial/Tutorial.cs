using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject [] tutorialStep, canvasStep;
    public bool pause, step1, step2, step3, step4;
    [SerializeField]
    bool W, A, S, D;

    private void Update()
    {
        if(pause)
        {
            Time.timeScale = 0f;
            if (step1)
            {
                canvasStep[0].SetActive(true);

                if (Input.GetAxisRaw("Vertical") == 1) W = true;
                if (Input.GetAxisRaw("Vertical") == -1) S = true;
                if (Input.GetAxisRaw("Horizontal") == 1) D = true;
                if (Input.GetAxisRaw("Horizontal") == -1) A = true;

                if (W && A && S && D)
                {
                    tutorialStep[0].SetActive(false);
                    canvasStep[0].SetActive(false);
                    Time.timeScale = 1.0f;
                    step1 = false;
                    pause = false;
                }
            }
            else if (step2)
            {
                canvasStep[1].SetActive(true);

                if (Input.GetButton("Attack1"))
                {
                    tutorialStep[1].SetActive(false);
                    canvasStep[1].SetActive(false);
                    Time.timeScale = 1.0f;
                    step2 = false;
                    pause = false;
                }
            }
            else if (step3)
            {
                canvasStep[2].SetActive(true);

                if (Input.GetButton("Attack1"))
                {
                    tutorialStep[2].SetActive(false);
                    canvasStep[2].SetActive(false);
                    Time.timeScale = 1.0f;
                    step3 = false;
                    pause = false;
                }
            }
            else if (step4)
            {
                canvasStep[3].SetActive(true);

                if (Input.GetButton("Attack1"))
                {
                    tutorialStep[3].SetActive(false);
                    canvasStep[3].SetActive(false);
                    Time.timeScale = 1.0f;
                    step4 = false;
                    pause = false;
                }
            }
        }
    }
}
