using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Test.Domain;
namespace Test.Persistence.Configurations;
public class NodeConfiguration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {

        builder
            .HasKey(_ => _.Id);

        builder
            .HasOne(e => e.Parent)
            .WithMany(e => e.ChildTreeNodes)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.Tree)
            .WithMany(e => e.Nodes)
            .HasForeignKey(e => e.TreeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}