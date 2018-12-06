namespace DailyCodingProblems.Base
{
    using System.Threading.Tasks;
    public abstract class BasePuzzle : IPuzzle
    {
        public string Name { get; }

        protected BasePuzzle(string name)
        {
            Name = name;
        }

        protected abstract Task<string> ExecuteImpl();

        public async Task<string> Execute()
        {
            var t = await ExecuteImpl();
            return Name + " | " + t;
        }
    }
}
