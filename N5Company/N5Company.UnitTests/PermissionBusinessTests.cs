using AutoMapper;
using MediatR;
using Moq;
using N5Company.Business;
using N5Company.Business.Interfaces;
using N5Company.Entities.DTOS;
using N5Company.Entities.Models;
using N5Company.MapperProfiles;
using N5Company.Queries.GetPermissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace N5Company.UnitTests
{
    public class PermissionBusinessTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IElasticSearchBusiness<Permission>> _elasticSearchBusinessMock;
        private readonly IMapper _mapper;

        public PermissionBusinessTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _elasticSearchBusinessMock = new Mock<IElasticSearchBusiness<Permission>>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<PermissionProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task GetAllPermissionsAsync_ShouldReturnListOfPermissions()
        {
            // Arrange
            var permissions = new List<Permission>
            {
                new Permission { Id = 1, EmployeeForename = "John", EmployeeSurname = "Doe", PermissionTypeId = 1 },
                new Permission { Id = 2, EmployeeForename = "Jane", EmployeeSurname = "Doe", PermissionTypeId = 2 },
                new Permission { Id = 3, EmployeeForename = "Bob", EmployeeSurname = "Smith", PermissionTypeId = 1 }
            };
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllPermissionsQuery>(), default)).ReturnsAsync(permissions);
            var permissionBusiness = new PermissionBusiness(_mediatorMock.Object, _elasticSearchBusinessMock.Object, _mapper);

            // Act
            var result = await permissionBusiness.GetAllPermissionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PermissionDTO>>(result);
            Assert.Equal(permissions.Count, result.Count());
        }
    }
}
