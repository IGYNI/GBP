using UnityEngine;

public class DemoTrigger : MonoBehaviour
{
    [SerializeField] private VariableSystem variableSystem;
    [SerializeField] private VariableDesc playerEnter;
    [SerializeField] private VariableDesc playerExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            variableSystem.SetVariable(playerEnter.name, playerEnter.value);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            variableSystem.SetVariable(playerExit.name, playerExit.value);
        }
    }
}
