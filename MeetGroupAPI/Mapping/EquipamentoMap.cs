using MeetGroupAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class EquipamentoMap : IEntityTypeConfiguration<Equipamento>
    {
        public void Configure(EntityTypeBuilder<Equipamento> builder)
        {
            builder.ToTable("equipamento");
            builder.HasKey(e => e.IdEquipamento).HasName("equipamento_pkey");
            builder.Property(e => e.IdEquipamento).HasColumnName("idequipamento");
            builder.Property(e => e.NomeEquipamento).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnName("nomeequipamento");
            builder.Property(e => e.StatusEquipamento).HasColumnName("statusequipamento");
        }
    }
}
