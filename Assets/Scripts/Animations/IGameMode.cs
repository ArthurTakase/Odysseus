using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameMode : MonoBehaviour
{
    protected List<Func<IEnumerator>> steps = new();
    protected int currentStep = -1;
    protected Coroutine currentStepCoroutine;

    public virtual void AnimationUpdate()
    {
        return;
    }

    public virtual void RelaunchAnimation()
    {
        Reset();
        NextStep();
    }

    public virtual void Reset()
    {
        currentStep = -1;

        if (currentStepCoroutine != null)
        {
            StopCoroutine(currentStepCoroutine);
            currentStepCoroutine = null;
        }
    }

    public void NextStep()
    {
        Debug.Log("Next step");

        currentStep++;

        if (currentStep >= steps.Count)
        {
            Debug.Log("No more steps");
            return;
        }

        if (currentStepCoroutine != null) StopCoroutine(currentStepCoroutine);
        currentStepCoroutine = StartCoroutine(steps[currentStep]());
    }

    public void PreviousStep()
    {
        Debug.Log("Previous step");

        currentStep--;

        if (currentStep < 0)
        {
            Debug.Log("No previous steps");
            return;
        }

        if (currentStepCoroutine != null) StopCoroutine(currentStepCoroutine);
        currentStepCoroutine = StartCoroutine(steps[currentStep]());
    }

    protected WaitForSeconds ActionAndWait(Action action, float waitDuration)
    {
        action();
        return new WaitForSeconds(waitDuration);
    }

    protected void ToggleBottomButtons(bool nextStatus = false, bool infoStatus = false)
    {
        AnimationManager.Instance.ToggleNextButton(nextStatus);
        AnimationManager.Instance.ToggleInfosButton(infoStatus);
    }
}