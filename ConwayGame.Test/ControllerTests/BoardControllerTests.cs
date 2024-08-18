using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ConwayGame.Controllers;
using ConwayGame.Models;
using ConwayGame.Services;
using System.Text;

namespace ConwayGame.Test.ControllerTests
{
    public class BoardControllerTests
    {
        private readonly BoardController _controller;
        private readonly Mock<IBoardService> _serviceMock;
        private readonly string mockBoardState = "-----\r\n--X--\r\n--X--\r\n--X--\r\n-----";

        public BoardControllerTests()
        {
            _serviceMock = new Mock<IBoardService>();
            _controller = new BoardController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetCurrentBoardState_ReturnsSuccess_WhenBoardExists()
        {
            int testId = 1;
            _serviceMock.Setup(s => s.GetBoardState(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<string> { Data = mockBoardState });

            var result = await _controller.GetCurrentBoardState(testId);

            var apiResult = Assert.IsType<ObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(apiResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal("success", apiResponse.Message);
            Assert.Equal(mockBoardState, apiResponse.Data);
        }

        [Fact]
        public async Task CreateBoard_ShouldReturnSuccess_WhenFileIsValid()
        {
            var fileMock = new Mock<IFormFile>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(mockBoardState));
            stream.Position = 0;

            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(stream.Length);

            _serviceMock.Setup(s => s.CreateBoard(It.IsAny<BoardDTO>()))
                .ReturnsAsync(new ServiceResponse<int> { Data = 1 });

            var result = await _controller.CreateBoard(fileMock.Object);
            var apiResult = Assert.IsType<ObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(apiResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("success", apiResponse.Message);
            Assert.Equal(1, apiResponse.Data);
        }

        [Fact]
        public async Task GetNextBoardState_ShouldReturnSuccess_WhenBoardExists()
        {
            _serviceMock.Setup(s => s.GetNextBoardState(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<string> { Data = mockBoardState });

            var result = await _controller.GetNextBoardState(1);
            var apiResult = Assert.IsType<ObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(apiResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("success", apiResponse.Message);
            Assert.Equal(mockBoardState, apiResponse.Data);
        }

        [Fact]
        public async Task GetFutureBoardState_ShouldReturnSuccess_WhenBoardExists()
        {
            _serviceMock.Setup(s => s.GetFutureBoardState(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<string> { Data = mockBoardState });

            var result = await _controller.GetFutureBoardState(1, 5);
            var apiResult = Assert.IsType<ObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(apiResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("success", apiResponse.Message);
            Assert.Equal(mockBoardState, apiResponse.Data);
        }

        [Fact]
        public async Task GetFinalBoardState_ShouldReturnSuccess_WhenBoardExists()
        {
            _serviceMock.Setup(s => s.GetFinalBoardState(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<string> { Data = mockBoardState });

            var result = await _controller.GetFinalBoardState(1);
            var apiResult = Assert.IsType<ObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(apiResult.Value);

            Assert.True(apiResponse.Success);
            Assert.Equal("success", apiResponse.Message);
        }
    }
}