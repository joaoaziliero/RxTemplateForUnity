using UnityEngine;

public class CustomBehaviourScript : AssetDeployer
{
    protected override GameObject PrepareTarget(UnityEngine.Object[] assets)
    {
        // Your code here
        return base.PrepareTarget(assets);
    }
}
