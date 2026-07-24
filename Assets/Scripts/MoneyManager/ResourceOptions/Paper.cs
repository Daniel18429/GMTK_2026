using UnityEngine;
public class Paper : Resource
{
    public Paper()
    {
        prefab = Resources.Load<GameObject>("Resource/Paper");
    }
}