using Moq;
using Project.Abstractions.Repositories;
using Project.Abstractions.Services;
using Project.Business.Services;
using Project.DataAccess;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests.ServiceTests
{
    public class VolunteerOrganizationTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IVolunteerOrganizationRepository> _volunteerOrganizationRepositoryMock;
        private readonly VolunteerOrganizationService _service;

        public VolunteerOrganizationTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _volunteerOrganizationRepositoryMock = new Mock<IVolunteerOrganizationRepository>();

            _unitOfWorkMock.Setup(u => u.VolunteerOrganizationRepository)
                           .Returns(_volunteerOrganizationRepositoryMock.Object);

            _service = new VolunteerOrganizationService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddVolunteerToOrganizationAsync_AddsVolunteer_WhenValid()
        {
            int volunteerId = 1;
            int organizationId = 1;

            _volunteerOrganizationRepositoryMock.Setup(o => o.GetVolunteersForOrganization(organizationId))
                                                .ReturnsAsync(new List<VolunteerEntity>());

            await _service.AddVolunteerToOrganizationAsync(volunteerId, organizationId);

            _volunteerOrganizationRepositoryMock.Verify(r => r.AddVolunteerToOrganization(volunteerId, organizationId), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddVolunteerToOrganizationAsync_ThrowsInvalidOperationException_WhenVolunteerAlreadyExists()
        {
            int volunteerId = 1;
            int organizationId = 1;

            _volunteerOrganizationRepositoryMock.Setup(r => r.GetVolunteersForOrganization(organizationId))
                                                .ReturnsAsync(new List<VolunteerEntity> { new VolunteerEntity { Id = volunteerId } });

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddVolunteerToOrganizationAsync(volunteerId, organizationId));
        }

        [Fact]
        public async Task RemoveVolunteerFromOrganizationAsync_RemoveVolunteer_WhenValid()
        {
            int volunteerId = 1;
            int organizationId = 1;

            _volunteerOrganizationRepositoryMock.Setup(vo => vo.GetVolunteersForOrganization(organizationId))
                                                .ReturnsAsync(new List<VolunteerEntity> { new VolunteerEntity { Id = volunteerId } });

            await _service.RemoveVolunteerFromOrganizationAsync(volunteerId, organizationId);

            _volunteerOrganizationRepositoryMock.Verify(r => r.RemoveVolunteerFromOrganization(volunteerId, organizationId), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveVolunteerFromOrganizationAsync_ThrowsArgumentException_WhenNotExist()
        {
            int volunteerId = 1;
            int organizationId = 1;

            _volunteerOrganizationRepositoryMock.Setup(vo => vo.GetVolunteersForOrganization(organizationId))
                                                .ReturnsAsync(new List<VolunteerEntity>());

            await Assert.ThrowsAsync<ArgumentException>(() => _service.RemoveVolunteerFromOrganizationAsync(volunteerId, organizationId));
        }
    }
}
