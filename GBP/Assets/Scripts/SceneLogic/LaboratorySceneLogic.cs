using UnityEngine;

public class LaboratorySceneLogic : MonoBehaviour
{
    private VariableSystem _variableSystem;
    
    [SerializeField] private Item labHole;
    
    private void Start()
    {
        _variableSystem = VariableSystem.Instance;
    }

    public void UnlockHole()
    {
        _variableSystem.SetVariable(labHole.info.itemName+Item.VisibleSuffix, "true", true);
        labHole.gameObject.SetActive(true);
    }
}
