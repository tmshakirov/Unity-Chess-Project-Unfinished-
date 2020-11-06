using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private bool tutorialOpened;
    [SerializeField] private CanvasGroup tutorialCanvas;

    public void OpenTutorial()
    {
        tutorialOpened = true;
        tutorialCanvas.gameObject.SetActive(true);
        tutorialCanvas.DOFade(1, 0.5f);
    }
    public void CloseTutorial()
    {
        tutorialOpened = false;
        tutorialCanvas.DOFade(0, 0.5f).OnComplete(() => tutorialCanvas.gameObject.SetActive(false));
    }
}
