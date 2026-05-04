using FrontiersDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontiersDemo.Infrastructure.Persistence.Configurations;

public sealed class ReviewerInvitationConfiguration : IEntityTypeConfiguration<ReviewerInvitation>
{
    public void Configure(EntityTypeBuilder<ReviewerInvitation> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => i.UserId);
    }
}
