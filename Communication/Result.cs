namespace PB.BaseResult.Communication
{
    public class Result<T>
    { 
        public T? Data { get; private set; }
        public Message? Message { get; private set; }

        public Result(T data)
        {
            Data = data;
            Message = new();
        }
        public Result(Message message)
        {
            Message = message;
        }
        public Result(T data, Message message)
        {
            Data = data;
            Message = message;
        }
    }
}