namespace PB.BaseResult.Communication
{
    public class Message
    {
        public string? Text { get; private set; }
        public MessageTypeEnum Type { get; private set; }

        public Message()
        {
            Text = string.Empty;
            Type = MessageTypeEnum.SUCCESS;
        }
        public Message(string text)
        {
            Text = text;
            Type = MessageTypeEnum.SUCCESS;
        }

        public Message(string text, MessageTypeEnum type)
        {
            Text = text;
            Type = (MessageTypeEnum)type;
        }
    }
}
