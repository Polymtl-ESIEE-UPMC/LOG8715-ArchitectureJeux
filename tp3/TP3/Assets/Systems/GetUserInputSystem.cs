using UnityEngine;
public class GetUserInputSystem : ISystem
{
    public string Name => "GetUserInputSystem";

    public void UpdateSystem()
    {
        if (ECSManager.Instance.NetworkManager.isClient)
        {
            uint playerId = (uint)CustomNetworkManager.Singleton.LocalClientId;
            InputMessage input = new InputMessage();
            input.playerId = playerId;
            input.timeCreated = Utils.SystemTime;

            if (Input.GetKey(KeyCode.A))
            {
                input.keycode = KeyCode.A;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                input.keycode = KeyCode.W;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                input.keycode = KeyCode.D;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                input.keycode = KeyCode.S;
            }
            else
            {
                input.keycode = KeyCode.None;
            }
            if (input.keycode != KeyCode.None)
                ComponentsManager.Instance.SetComponent<InputMessage>(playerId, input);
        }
    }
}
