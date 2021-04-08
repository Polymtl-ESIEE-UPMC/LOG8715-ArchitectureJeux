public class InputMessageIdTracker : IComponent
{
    public int currentMessageId = 0;

    public InputMessageIdTracker(int messageId)
    {
        currentMessageId = messageId;
    }
}
