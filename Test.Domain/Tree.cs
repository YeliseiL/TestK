﻿namespace Test.Domain;

public class Tree
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Node>? Nodes { get; set; }
}
