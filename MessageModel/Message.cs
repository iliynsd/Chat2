namespace MessageModel
{
    public enum TypeMessage
    {
        Stop, Message, Name
    }
    
    public class Message
    {
        public TypeMessage Type { get; set; }
        public string Msg { get; set; }
    }
}