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

            //send history up to 9 frames
            int historyTosend = 9;

            int diff = World.Instance.inputHistory.Count - historyTosend;
            if (diff < 0)
            {
                for (int i = 0; i < historyTosend - World.Instance.inputHistory.Count; i++)
                {
                    input.keycode.Add((short)KeyCode.None);
                }
                for (int i = 0; i < World.Instance.inputHistory.Count; i++)
                {
                    input.keycode.Add((short)World.Instance.inputHistory[i]);
                }
            }
            else
            {
                for (int i = 0; i < historyTosend; i++)
                {
                    input.keycode.Add((short)World.Instance.inputHistory[World.Instance.inputHistory.Count - historyTosend + i]);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                input.keycode.Add((short) KeyCode.A);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                input.keycode.Add((short)KeyCode.W);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                input.keycode.Add((short)KeyCode.D);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                input.keycode.Add((short)KeyCode.S);
            }
            else
            {
                input.keycode.Add((short)KeyCode.None);
            }

            if ((KeyCode)input.keycode[input.keycode.Count-1] != KeyCode.None)
                ComponentsManager.Instance.SetComponent<InputMessage>(playerId, input);
        }
    }
}
