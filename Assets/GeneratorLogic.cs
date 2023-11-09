using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLogic : MonoBehaviour
{
    public GameObject rotatingPiece;
    public float rotationSpeed = 90f; 
    public float moveUpDistance = 2f; 
    public float moveUpDuration = 2f; 
    public float shootDownDuration = 0.5f; 

    private bool isMoving = false;
    
    void Update()
    {
        
        rotatingPiece.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        
        if (!isMoving)
        {
            StartCoroutine(MovePiece());
        }
    }

    IEnumerator MovePiece()
    {
        isMoving = true;

       
        Vector3 startPosition = rotatingPiece.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveUpDistance;
        for (float t = 0; t < 1; t += Time.deltaTime / moveUpDuration)
        {
            rotatingPiece.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        
        rotatingPiece.transform.position = endPosition;

        
        for (float t = 0; t < 1; t += Time.deltaTime / shootDownDuration)
        {
            rotatingPiece.transform.position = Vector3.Lerp(endPosition, startPosition, t);
            BaseScript.credits += 1;
            //print(BaseScript.credits);
            yield return null;
        }

       
        rotatingPiece.transform.position = startPosition;

        isMoving = false;
    }
}
