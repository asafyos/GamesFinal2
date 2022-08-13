using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GuideType
{
    Move,
    Jump,
    Pause,
    Collect,
    Shoot
}

public class GuideScript : MonoBehaviour
{
    public GuideType guideType;
    public Text guideText;

    public void clearGuide()
    {
        guideText.text = "";
    }

    public void moveGuide()
    {
        guideText.text = "Move with A, S, W, D keys";
    }

    public void jumpGuide()
    {
        guideText.text = "Jump with Space bar\nRun with Shift key\nSneek with Ctrl key";
    }

    public void pauseGuide()
    {
        guideText.text = "Pause with Esc key";
    }

    public void OnTriggerStay(Collider other)
    {
        calcMessage(other);
    }

    public void OnTriggerEnter(Collider other)
    {
        calcMessage(other);
    }

    private void calcMessage(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (guideType)
            {
                case GuideType.Move:
                    moveGuide();
                    break;
                case GuideType.Jump:
                    jumpGuide();
                    break;
                case GuideType.Pause:
                    pauseGuide();
                    break;
                case GuideType.Collect:
                    collectGuide();
                    break;
                case GuideType.Shoot:
                    shootGuide();
                    break;
                default:
                    clearGuide();
                    break;
            }
        }
    }

    private void shootGuide()
    {
        guideText.text = "Select ducks with 1(red)/2(yellow) keys, or with the scroll wheel\nThrow duck with right click";
    }

    private void collectGuide()
    {
        guideText.text = "Collect duck to use as distarction";
    }

    public void OnTriggerExit()
    {
        clearGuide();
    }
}
