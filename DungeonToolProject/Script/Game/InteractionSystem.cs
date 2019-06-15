using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InteractionSystem : NetworkBehaviour
{

    private enum Mode
    {
        Selection,
        Talk,
        Attack
    }

    private Mode mode;

    [SerializeField] private GameObject talkPrefab;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Camera _camera;

    private GameObject talkObject;
    private GameObject attackObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //prendi posizione da schermo
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            var position = _camera.ScreenToWorldPoint(mousePos);

            position.x = Mathf.Floor(position.x);
            position.y = Mathf.Floor(position.y);


        }
    }

    public void ZoomIn()
    {
        _camera.orthographicSize -= 1;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 1, 18);
    }

    public void ZoomOut()
    {
        _camera.orthographicSize += 1;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 1, 18);
    }

    public void Selection()
    {
        mode = Mode.Selection;
    }

    public void Attack()
    {
        mode = Mode.Attack;
    }

    public void Talk()
    {
        mode = Mode.Talk;
    }
}
