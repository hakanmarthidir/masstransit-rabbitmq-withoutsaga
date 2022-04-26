namespace SharedKernel
{
    public interface INewsCommand
    {
        string NewsTitle { get; set; }
        string NewsContent { get; set; }
        string NewsAuthor { get; set; }
    }
}

