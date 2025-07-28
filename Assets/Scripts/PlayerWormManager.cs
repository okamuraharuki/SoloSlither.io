using UnityEngine;

public class PlayerWormManager : WormManager
{
    public Vector3 GetPlayerPos => this.transform.position;
    protected override void Start()
    {
        base.Start();
        _isActive = true;
    }
    protected override void Update()
    {
        if (_isActive)
        {
            Vector3 moveVec = MouseVectorReader.Instance.GetMousePosVector();
            EyeMove(moveVec);
            BodyMove();
            HeadMove(moveVec);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            BodyGenerate();
        }
    }
}
