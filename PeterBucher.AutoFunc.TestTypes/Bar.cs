namespace PeterBucher.AutoFunc.TestTypes
{
    public class Bar : IBar
    {
        public IFoo Foo
        {
            get;
            set;
        }

        public string Arg1 { get; private set; }

        public bool Arg2 { get; private set; }

        public Bar()
        {

        }

        public Bar(IFoo foo)
        {
            this.Foo = foo;
        }

        public Bar(bool arg2)
        {
            this.Arg2 = arg2;
        }

        public Bar(string arg1, bool arg2)
        {
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }

        public Bar(IFoo foo, string arg1)
        {
            this.Foo = foo;
            this.Arg1 = arg1;
        }

        public Bar(IFoo foo, string arg1, bool arg2)
        {
            this.Foo = foo;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }
    }
}