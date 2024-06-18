using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    // Kamera referansı
    public Camera mainCamera;

    // İşaretçi (cursor) objesi
    public GameObject cursorObject;

    // İşaretçinin hareket hızı
    public float cursorSpeed = 5f;

    void Update()
    {
        
    // Fare hareketi kontrolü
    float mouseX = Input.GetAxis("Mouse X");
    float mouseY = Input.GetAxis("Mouse Y");

    // Eğer fare hareket ediyorsa işaretçiyi güncelle
    if (Mathf.Abs(mouseX) > Mathf.Epsilon || Mathf.Abs(mouseY) > Mathf.Epsilon)
    {
        // Kamera pozisyonunu ve bakış yönünü al
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // Raycast ile kameranın baktığı noktayı belirle
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraPosition, cameraForward, out hitInfo))
        {
            // İşaretçinin pozisyonunu belirle (kameranın baktığı noktaya doğru)
            Vector3 cursorPosition = hitInfo.point;

            // İşaretçiyi hareket ettir
            cursorObject.transform.position = Vector3.Lerp(cursorObject.transform.position, cursorPosition, Time.deltaTime * cursorSpeed);

            // İşaretçiyi kameranın baktığı noktaya doğru döndür
            cursorObject.transform.LookAt(mainCamera.transform);
        }
    }
    }
}




