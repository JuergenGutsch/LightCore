using System;

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

        public int Arg3
        {
            get;
            set;
        }

        public Guid Arg4
        {
            get;
            set;
        }

        public Foo()
        {

        }

        public Foo(Guid arg4)
        {
            this.Arg4 = arg4;
        }

        public Foo(IBar bar)
        {
            this.Bar = bar;
        }

        public Foo(Bar bar)
        {
            this.Bar = bar;
        }

        public Foo(int arg3)
        {
            this.Arg3 = arg3;
        }

        public Foo(Bar bar, string arg1)
        {
            this.Bar = bar;
            this.Arg1 = arg1;
        }

        public Foo(IBar bar, string arg1)
        {
            this.Bar = bar;
            this.Arg1 = arg1;
        }

        public Foo(string arg1, IBar bar, bool arg2)
        {
            this.Bar = bar;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }

        public Foo(string arg1, bool arg2)
        {
            this.Arg1 = arg1;
            this.Arg2 = arg2;
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