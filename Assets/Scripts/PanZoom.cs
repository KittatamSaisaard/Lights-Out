using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1.105f;
    float zoomOutMax = Mathf.Infinity;
    public float zoomSpeed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * zoomSpeed);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            pos = Camera.main.transform.position;
        }

        pos.x = Mathf.Clamp(pos.x, 0, (GameObject.FindWithTag("Tiles").GetComponent<TileManager>().tile_count - 1) * 1.05f);
        pos.y = Mathf.Clamp(pos.y, (GameObject.FindWithTag("Tiles").GetComponent<TileManager>().tile_count - 1) * -1.05f, 0);

        Camera.main.transform.position = pos;

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    //void LateUpdate()
    //{
    //    Vector3 viewPos = Camera.main.transform.position;
    //    viewPos.x = Mathf.Clamp(viewPos.x, 9 * -1.05f, 9 * 1.05f);
    //    viewPos.y = Mathf.Clamp(viewPos.y, 9 * -1.05f, 9 * 1.05f);
    //    Camera.main.transform.position = viewPos;
    //}

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, (GameObject.FindWithTag("Tiles").GetComponentInChildren<SpriteRenderer>().bounds.size.x + 0.12f) * Screen.height / Screen.width * 0.5f, GameObject.FindWithTag("Tiles").GetComponent<TileManager>().orthoSize);
    }
}