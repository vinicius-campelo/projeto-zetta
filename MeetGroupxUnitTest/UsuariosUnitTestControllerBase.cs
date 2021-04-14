using MeetGroupAPI.Controllers;
using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MeetGroupxUnitTest
{
    public class UsuariosUnitTestControllerBase
    {
        ApiDbContext _apiDbContext;
        UsuariosController _usuariosController;
        IUsuarioInterface _usuarioInterface;

        public static DbContextOptions<ApiDbContext> dbContextOptions { get; }

        public static string connectionString = "host=172.17.0.1;port=5432;database=meetgroupdb;username=postgres;password=postgres;";
   
        static UsuariosUnitTestControllerBase()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
                .UseNpgsql(connectionString)
                .Options;
        }
        public UsuariosUnitTestControllerBase()
        {
            _apiDbContext = new ApiDbContext(dbContextOptions);
            _usuarioInterface = new UsuarioRepositoryTest();
            _usuariosController = new UsuariosController(_apiDbContext, _usuarioInterface);
        }

        [Fact]
        public void Post_WhenCalled_ReturnsAllItems()
        {
            //Arrange
            _usuariosController = new UsuariosController(_apiDbContext, _usuarioInterface);

            // Act
            var okResult = _usuariosController.GetAll();
            // Assert
            Assert.IsType<Usuario>(okResult);
        }
    }
}