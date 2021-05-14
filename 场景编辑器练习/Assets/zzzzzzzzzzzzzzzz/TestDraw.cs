using UnityEngine;
using System.Collections;

public class TestDraw : MonoBehaviour
{

    void Start()
    {
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            DrawTool.DrawSectorSolid(transform, transform.localPosition + new Vector3(0,-0.5f,0), 60, 3);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DrawTool.DrawCircleSolid(transform, transform.localPosition + new Vector3(0, -0.5f, 0), 3);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DrawTool.DrawRectangleSolid(transform, transform.localPosition + new Vector3(0, -0.5f, 0), 5, 2);
        }
    }
}
