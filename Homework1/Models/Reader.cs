class Reader
{
    public string Name { get; set; }
    public int ReaderId { get; set; }

    public Reader(string name, int readerId)
    {
        Name = name;
        ReaderId = readerId;
    }
}
