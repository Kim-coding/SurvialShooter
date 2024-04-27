using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 offSet;

    private void Update()
    {
        transform.position = Player.position + offSet;
    }
}
