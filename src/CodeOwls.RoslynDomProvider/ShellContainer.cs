namespace CodeOwls.RoslynDomProvider
{
    public class ShellContainer
    {
        public ShellContainer(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
    }
}