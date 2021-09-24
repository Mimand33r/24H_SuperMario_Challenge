using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    private Transform player;
    public float screenMaxRelativeHorOffset = 0.33f;
    public float screenMaxRelativeTopOffset = 0.8f;
    public float screenMaxRelativeBotOffset = 0.4f;

    private float initialY;

    private Camera cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        initialY = cam.transform.position.y;
    }

    void Update()
    {
        int screenWidth = cam.pixelWidth;
        int screenHeight = cam.pixelHeight;
        Vector3 absScreenPosition = cam.WorldToScreenPoint(player.position);
        float relativeScreenPositionX = absScreenPosition.x / screenWidth;
        float relativeScreenPositionY = absScreenPosition.y / screenHeight;

        // User reached left Screen
        if (relativeScreenPositionX < screenMaxRelativeHorOffset)
        {
            float deltaX = screenMaxRelativeHorOffset - relativeScreenPositionX;
            float deltaXAbsolute = screenWidth * deltaX;

            transform.position = new Vector3(transform.position.x - deltaXAbsolute * Time.deltaTime, transform.position.y, transform.position.z);
        }

        // User reached right Screen
        if (relativeScreenPositionX > 1 - screenMaxRelativeHorOffset)
        {
            float deltaX = relativeScreenPositionX - 1 + screenMaxRelativeHorOffset;
            float deltaXAbsolute = screenWidth * deltaX;

            transform.position = new Vector3(transform.position.x + deltaXAbsolute * Time.deltaTime, transform.position.y, transform.position.z);
        }

        // User reached Top Screen
        if (relativeScreenPositionY > screenMaxRelativeTopOffset)
        {
            float deltaY = relativeScreenPositionY - screenMaxRelativeTopOffset;
            float deltaYAbsolute = screenHeight * deltaY;

            transform.position = new Vector3(transform.position.x, transform.position.y + deltaYAbsolute * Time.deltaTime, transform.position.z);
        }

        // User reached Bot Screen
        if (relativeScreenPositionY < screenMaxRelativeBotOffset)
        {
            // Never go Below initial Y 
            if (transform.position.y < initialY * 1.1f)
                transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            else
            {
                float deltaY = screenMaxRelativeBotOffset - relativeScreenPositionY;
                float deltaYAbsolute = screenHeight * deltaY;

                transform.position = new Vector3(transform.position.x, transform.position.y - deltaYAbsolute * Time.deltaTime, transform.position.z);
            }

        }





        //transform.position = new Vector3(player.position.x + OffsetX, player.position.y + OffsetY, OffsetZ);
    }
}
