namespace MonadicTemplate
{
    public interface IGenerator<out T>
    {
        ICovariantTuple<T, Context> Run(Context ctx);
    }
}
