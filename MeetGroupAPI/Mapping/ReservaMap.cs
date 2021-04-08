using MeetGroupAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class ReservaMap : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("reserva");
            builder.HasKey(e => e.IdReserva).HasName("reserva_pkey");
            builder.Property(e => e.IdReserva).HasColumnName("idreserva");
            builder.Property(e => e.DataFimReserva).HasColumnType("date").HasColumnName("datafimreserva");
            builder.Property(e => e.DataInicioReserva).HasColumnType("date").HasColumnName("datainicioreserva");
            builder.Property(e => e.HoraFimReserva).IsRequired().HasMaxLength(8).IsUnicode(false).HasColumnName("horafimreserva");
            builder.Property(e => e.HoraInicioReserva).IsRequired().HasMaxLength(8).IsUnicode(false).HasColumnName("horainicioreserva");
            builder.Property(e => e.QuantidadePessoasReserva).HasColumnName("quantidadepessoasreserva");
            builder.Property(e => e.ComputadorReserva).HasColumnName("computadorreserva");
            builder.Property(e => e.IdUsuarioReserva).HasColumnName("idusuarioreserva");
            builder.Property(e => e.TvReserva).HasColumnName("tvreserva");
            builder.Property(e => e.InternetReserva).HasColumnName("internetreserva");
            builder.Property(e => e.WebcamReserva).HasColumnName("webcamreserva");
            builder.Property(e => e.StatusReserva).HasColumnName("statusreserva");

        }
    }
}
