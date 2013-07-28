namespace Oleg_ivo.PrismExtensions.NamedObject
{
    public interface INamedObject
    {
        INamedObject Parent { get; set; }
        string Name { get; set; }
    }
}
