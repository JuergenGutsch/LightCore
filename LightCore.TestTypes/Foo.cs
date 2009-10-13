namespace LightCore.TestTypes
{
    public class Foo : FooBase
    {
        public string Arg1
        {
            get;
            private set;
        }

        public bool Arg2
        {
            get;
            private set;
        }

        public Foo()
        {

        }

        public Foo(IBar bar)
        {
            this.Bar = bar;
        }

        public Foo(bool arg2)
        {
            this.Arg2 = arg2;
        }

        public Foo(IBar bar, string arg1, bool arg2)
        {
            this.Bar = bar;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }

        public Foo(string arg1)
        {
            this.Arg1 = arg1;
        }
    }
}