using UnityEngine;

public class DockingRoomSceneLogic : MonoBehaviour
{
    private VariableSystem _variableSystem;
    
    [SerializeField] private Item lockedDoor;
    [SerializeField] private Item unlockedDoor;
    
    private void Start()
    {
        _variableSystem = VariableSystem.Instance;
    }

    public void UnlockDoor()
    {
        _variableSystem.SetVariable(lockedDoor.info.itemName+Item.VisibleSuffix, "false", true);
        _variableSystem.SetVariable(unlockedDoor.info.itemName+Item.VisibleSuffix, "true", true);
        
        lockedDoor.gameObject.SetActive(false);
        unlockedDoor.gameObject.SetActive(true);
    }
}
