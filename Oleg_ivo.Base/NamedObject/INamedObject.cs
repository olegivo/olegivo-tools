namespace Oleg_ivo.Base.NamedObject
{
    public interface INamedObject
    {
        INamedObject Parent { get; set; }
        string Name { get; set; }
    }
}
