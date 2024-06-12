using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    public GameObject map;
    float tileSize;
    int tilesNeededX, tilesNeededY;
    Camera cam;

    void Start()
    {
        tileSize = map.GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main;
        Vector2 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        tilesNeededX = Mathf.CeilToInt(screenBounds.x * 2 / tileSize);
        tilesNeededY = Mathf.CeilToInt(screenBounds.y * 2 / tileSize);

        // Creating map tiles
        for (int x = -tilesNeededX; x < tilesNeededX; x++)
        {
            for (int y = -tilesNeededY; y < tilesNeededY; y++)
            {
                Vector3 tilePosition = new Vector3(x * tileSize, y * tileSize, transform.position.z);
                GameObject newTile = Instantiate(map, tilePosition, Quaternion.identity);
                newTile.transform.parent = transform;
            }
        }
    }
    void RepositionTile()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child == transform) continue;
            if (cam.transform.position.x > child.transform.position.x + tileSize * tilesNeededX)
            {
                child.transform.position = new Vector3(child.transform.position.x + tileSize * tilesNeededX * 2, child.transform.position.y, child.transform.position.z);
            }
            if (cam.transform.position.x < child.transform.position.x - tileSize * tilesNeededX)
            {
                child.transform.position = new Vector3(child.transform.position.x - tileSize * tilesNeededX * 2, child.transform.position.y, child.transform.position.z);
            }
            if (cam.transform.position.y > child.transform.position.y + tileSize * tilesNeededY)
            {
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + tileSize * tilesNeededY * 2, child.transform.position.z);
            }
            if (cam.transform.position.y < child.transform.position.y - tileSize * tilesNeededY)
            {
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - tileSize * tilesNeededY * 2, child.transform.position.z);
            }
        }
    }
    private void LateUpdate()
    {
        RepositionTile();
    }
}

