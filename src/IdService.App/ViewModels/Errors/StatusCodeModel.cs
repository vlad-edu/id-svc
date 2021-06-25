namespace IdService.App.ViewModels.Errors
{
    public sealed class StatusCodeModel
    {
        public int Code { get; init; }

        public string? Message { get; init; }

        public string? Description { get; init; }
    }
}
