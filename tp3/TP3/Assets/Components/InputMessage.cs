using UnityEngine;
using System.Collections.Generic;
public class InputMessage : IComponent
{

    public InputMessage()
    {
        keycode = new List<short>();
    }

    public int messageID;
    public int timeCreated;
    public uint playerId;
    public List<short> keycode;
}
