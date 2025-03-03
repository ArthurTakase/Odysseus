using UnityEngine;

public class DebugEditor : MonoBehaviour
{
    public IGameMode modeScenario;
    public IGameMode modeLibre;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            modeScenario.RelaunchAnimation();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            modeLibre.RelaunchAnimation();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            modeScenario.NextStep();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            modeScenario.PreviousStep();
        }
    }
}