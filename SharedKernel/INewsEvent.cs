namespace SharedKernel
{
    public interface INewsEvent
    {
        Guid NewsId { get; set; }
        string NewsTitle { get; set; }
        string NewsContent { get; set; }
        string NewsAuthor { get; set; }
    }
}

