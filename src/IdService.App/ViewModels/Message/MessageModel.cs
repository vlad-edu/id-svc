namespace IdService.App.ViewModels.Message
{
    public sealed class MessageModel
    {
        public const string TempDataKey = nameof(MessageModel) + "Key";

        public MessageLevel Level { get; set; }

        public string Caption { get; set; } = "Message";

        public string? Message { get; set; }

        public string? Description { get; set; }

        public string? ActionName { get; set; }

        public string ActionUrl { get; set; } = "#";
    }
}
