namespace Ultils.Model;
public class TreeList<T>
{
    public T? Node { get; set; }
    public IEnumerable<TreeList<T>>? Childrens { get; set; }
}