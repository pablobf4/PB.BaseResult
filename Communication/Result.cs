namespace PB.BaseResult.Communication
{
    public class Result<T>
    {
        private Result(T? data, bool isSuccess, string message, MessageTypeEnum? messageType, List<string>? errors = null)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
            MessageType = messageType;
            Errors = errors;

        }
        public T? Data { get; }
        public bool IsSuccess { get; }
        public string Message { get; }
        public MessageTypeEnum? MessageType { get; }
        public List<string>? Errors { get; }

      
        public static Result<T> Success(T data, string message = "Operação realizada com sucesso") =>
            new(data, true, message, MessageTypeEnum.SUCCESS);

        public static Result<T> Success(string message = "Operação realizada com sucesso") =>
            new(default, true, message, MessageTypeEnum.SUCCESS);

        public static Result<T> Fail(string message, MessageTypeEnum messageType, List<string>? errors = null) =>
            new(default, false, message, messageType, errors);

        public static implicit operator Result<T>(T data) => Success(data);
        public static implicit operator Result<T>(string message) => Fail(message, MessageTypeEnum.ERROR);

        public override string ToString()
        {
            return IsSuccess
                ? $"Success: {Message} - Data: {Data}"
                : $"Failure: {Message} - Type: {MessageType}";
        }

    }


}