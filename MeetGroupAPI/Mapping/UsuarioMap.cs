using MeetGroupAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(e => e.IdUsuario).HasName("usuario_pkey");
            builder.Property(e => e.IdUsuario).HasColumnName("idusuario");
            builder.Property(e => e.EmailUsuario).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("emialusuario");
            builder.Property(e => e.NomeUsuario).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("nomeusuario");
            builder.Property(e => e.SenhaUsuario).IsRequired().HasMaxLength(53).IsUnicode(false).HasColumnName("senhausuario");
            builder.Property(e => e.StatusUsuario).HasColumnName("statususuario");
            builder.Property(e => e.TelefoneUsuario).HasMaxLength(11).IsUnicode(false).HasColumnName("telefoneusuario");
        }
    }
}
