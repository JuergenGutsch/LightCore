namespace LightCore.TestTypes
{
    public class FooWithArguments : FooBase
    {
        public string Arg1
        {
            get;
            set;
        }

        public bool Arg2
        {
            get;
            set;
        }

        public FooWithArguments(string arg1, bool arg2)
        {
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }
    }
}