using UnityEngine;

public class CorridorSceneLogic : MonoBehaviour
{
    private VariableSystem _variableSystem;
    
    [SerializeField] private Item firedDoor;
    [SerializeField] private Item labDoor;
    
    private void Start()
    {
        _variableSystem = VariableSystem.Instance;
    }

    public void LOGIC_ExtinguishFire()
    {
        _variableSystem.SetVariable(firedDoor.info.itemName+Item.VisibleSuffix, "false", true);
        _variableSystem.SetVariable(labDoor.info.itemName+Item.VisibleSuffix, "true", true);
        
        firedDoor.gameObject.SetActive(false);
        labDoor.gameObject.SetActive(true);
    }
}
