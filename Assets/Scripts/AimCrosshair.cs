using UnityEngine;

public class AimCrosshair : MonoBehaviour
{
    public void SetPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);

    }
    public void SetVisable(bool visable)
    {
        Cursor.visible = !visable;
        gameObject.SetActive(visable);
    }
}
