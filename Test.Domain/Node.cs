namespace Test.Domain;
public class Node
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Node>? ChildTreeNodes { get; set; }
    public int? ParentId { get; set; }
    public Node? Parent { get; private set; }
    public int TreeId { get; set; }
    public Tree Tree { get; set; }
    public Node()
    {
        
    }
    public Node(int treeId, string name, int? parentId = null)
    {
        TreeId = treeId;
        Name = name;
        ParentId = parentId;
        ChildTreeNodes = new List<Node>();
    }
}