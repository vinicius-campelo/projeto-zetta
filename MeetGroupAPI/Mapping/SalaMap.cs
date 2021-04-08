using MeetGroupAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class SalaMap : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("sala");
            builder.HasKey(e => e.IdSala).HasName("sala_pkey");
            builder.Property(e => e.IdSala).HasColumnName("idsala");
            builder.Property(e => e.IdSalaEquipamento).HasColumnName("idsalaequipamento");
            builder.Property(e => e.NumeroSala).HasColumnName("numerosala");
            builder.Property(e => e.QuantidadeEquipamentoSala).HasColumnName("quantidadeequipamentosala");
            builder.Property(e => e.QuantidadeLugaresSala).HasColumnName("quantidadelugaressala");
            builder.Property(e => e.StatusSala).HasColumnName("statussala");

            builder.HasOne(d => d.IdSalaEquipamentoNavigation)
                .WithMany(p => p.Sala)
                .HasForeignKey(d => d.IdSalaEquipamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sala_idsalaequipamento_fkey");

        }
    }
}
