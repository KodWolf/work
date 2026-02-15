public class ResumeCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Resume();
    }
}
