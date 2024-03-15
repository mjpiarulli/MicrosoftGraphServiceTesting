using ActiveDirectory.Common;
using ADService;
using Microsoft.Graph;
using MicrosoftGraph.Service;
using ActiveDirectoryService.UnitTest.Helpers;
using NSubstitute;
using FluentAssertions;
using System.Net;
using Common;
using Bogus;
using NSubstitute.ExceptionExtensions;

namespace ActiveDirectoryService.UnitTest
{
    public class ADServiceTest
    {
        private readonly IADService _sut;
        private readonly IGraphService _graphService = Substitute.For<IGraphService>();
        private readonly Faker<ActiveDirectory.Common.User> _userGenerator = new Faker<ActiveDirectory.Common.User>();

        private const string _baseUrl = "https://localhost/v1.0";        

        public ADServiceTest()
        {
            _sut = new ADService.ADService(_graphService);
        }

        [Fact]
        public async Task GetUserByGuid_ShouldReturnUser_WhenUserGuidExists()
        {
            // Arrange
            var user = _userGenerator.Generate();
            var responseMessage = new HttpResponseMessage
            {
                //Content = StringContent with serialized json user/group data,
                StatusCode = HttpStatusCode.OK
            };
            var response = MicrosoftGraphResponseHelper.CreateGraphResponse<Microsoft.Graph.User>(_baseUrl, responseMessage);
            _graphService.GetUserByGuidAsync(Arg.Any<string>(), null, null).Returns(response);
            var expected = new ResponseWithContent<ActiveDirectory.Common.User>
            {
                Content = user,
                ResponseMessage = "Success",
                IsSuccess = true
            };

            // Act
            var actual = await _sut.GetUserByGuidAsync(user.Id);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetUserByGuidAsync_ShouldReturnFailure_WhenGraphResponseStatusCodeIsNotOk()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
            var response = MicrosoftGraphResponseHelper.CreateGraphResponse<Microsoft.Graph.User>(_baseUrl, responseMessage);
            _graphService.GetUserByGuidAsync(Arg.Any<string>(), null, null).Returns(response);

            // Act
            var actual = await _sut.GetUserByGuidAsync(Guid.NewGuid().ToString());

            // Assert
            actual.IsSuccess.Should().BeFalse();
            actual.Content.Should().BeNull();
            actual.ResponseMessage.Should().NotBeNullOrEmpty();
            actual.ResponseMessage.Should().NotBe("Success");
        }

        [Fact]
        public async Task GetUserByGuidAsync_ShouldReturnSuccess_WhenUserResponseIsNull()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage { Content = null };
            var response = MicrosoftGraphResponseHelper.CreateGraphResponse<Microsoft.Graph.User>(_baseUrl, responseMessage);
            _graphService.GetUserByGuidAsync(Arg.Any<string>(), null, null).Returns(response);

            // Act
            var actual = await _sut.GetUserByGuidAsync(Guid.NewGuid().ToString());

            // Assert
            actual.IsSuccess.Should().BeTrue();
            actual.Content.Should().BeNull();
            actual.ResponseMessage.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetUserByGuidAsync_ShouldThrowException_WhenNoUserExists()
        {
            // Arrange
            _graphService.GetUserByGuidAsync(Arg.Any<string>(), null, null).Throws(new ServiceException(Substitute.For<Error>()));

            // Act

            // Assert
            await Assert.ThrowsAsync<ServiceException>(() => _sut.GetUserByGuidAsync(Guid.NewGuid().ToString()));
        }

        [Fact]
        public async Task GetAllUsersPagedAsync_ShouldReturnFailure_WhenGraphResponseStatusCodeIsNotOK()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            var response = MicrosoftGraphResponseHelper.CreateGraphResponse<GraphServiceUsersCollectionResponse>(_baseUrl, responseMessage); 
                
            _graphService.GetAllUsersPagedAsync(null, null, null, null).Returns(response);

            // Act
            var actual = await _sut.GetAllUsersPagedAsync(null, null);

            // Assert
            actual.IsSuccess.Should().BeFalse();
            actual.Content.Should().BeNull();
            actual.ResponseMessage.Should().NotBeNullOrEmpty();
            actual.ResponseMessage.Should().NotBe("Success");
        }

        [Fact]
        public async Task GetAllUsersPagedAsync_ShouldReturnFailure_WhenUserResponseIsNull()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                Content = null
            };

            var response = MicrosoftGraphResponseHelper.CreateGraphResponse<GraphServiceUsersCollectionResponse>(_baseUrl, responseMessage);
            _graphService.GetAllUsersPagedAsync(null, null, Arg.Any<int>(), null).Returns(response);

            // Act
            var actual = await _sut.GetAllUsersPagedAsync(null, null);

            // Assert
            actual.IsSuccess.Should().BeFalse();
            actual.Content.Should().BeNull();
            actual.ResponseMessage.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllUsersPaged_ShouldReturnNextPageToken_WhenGraphReturnsSkipToken()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                //Content = StringContent with serialized json user/group data,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            var response = MicrosoftGraphResponseHelper.CreateGraphResponse<GraphServiceUsersCollectionResponse>(_baseUrl, responseMessage);
            _graphService.GetAllUsersPagedAsync(null, null, Arg.Any<int>(), null).Returns(response);

            // Act
            var actual = await _sut.GetAllUsersPagedAsync(Arg.Any<int>(), null);

            // Assert
            actual.IsSuccess.Should().BeTrue();
            actual.Content.Should().NotBeNull();
            actual.ResponseMessage.Should().NotBeNullOrEmpty();
            actual.NextPageToken.Should().NotBeNullOrEmpty();
        }
    }
}