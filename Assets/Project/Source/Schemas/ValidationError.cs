namespace Exa.Schemas
{
    public abstract class ValidationError
    {
        public ValidationError(string id, string message)
        {
            Message = message;
            Id = id;
        }

        public string Message { get; private set; }
        public string CreatorId { get; internal set; }
        public string Id { get; private set; }
        public int Order { get; internal set; }

        public string ErrorId
        {
            get => $"{CreatorId}:{Id}:{Order}";
        }
    }
}