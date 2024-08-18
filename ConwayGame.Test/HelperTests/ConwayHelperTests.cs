using ConwayGame.Helpers;

namespace ConwayGame.Test.HelperTests
{
    public class ConwayHelperTests
    {
        [Fact]
        public void ConvertStringToBoard_ReturnsCorrectBoard()
        {
            // Arrange
            var inputString = "XX\r\n--";
            var rowCount = 2;
            var columnCount = 2;

            // Act
            var result = ConwayHelper.ConvertStringToBoard(inputString, rowCount, columnCount);

            // Assert
            Assert.Equal("X", result[0, 0]);
            Assert.Equal("X", result[0, 1]);
            Assert.Equal("-", result[1, 0]);
            Assert.Equal("-", result[1, 1]);
        }

        [Fact]
        public void ConvertBoardToString_ReturnsCorrectString()
        {
            // Arrange
            var inputBoard = new string[,] { { "X", "X" }, { "-", "-" } };

            // Act
            var result = ConwayHelper.ConvertBoardToString(inputBoard, 2, 2);

            // Assert
            Assert.Equal("XX\r\n--", result);
        }

        [Fact]
        public void CountLiveNeighbors_ReturnsCorrectCount()
        {
            // Arrange
            var inputBoard = new string[,] { { "X", "X", "X" }, { "X", "X", "X" }, { "X", "X", "X" } };

            // Act
            var result = ConwayHelper.CountLiveNeighbors(inputBoard, 1, 1);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void GetNextState_ReturnsCorrectState()
        {
            // Arrange
            var inputState = "XX\r\n--";
            var rowCount = 2;
            var columnCount = 2;

            // Act
            var result = ConwayHelper.GetNextState(inputState, rowCount, columnCount);

            // Assert
            Assert.Equal("--\r\n--", result);
        }

        [Fact]
        public void GetFutureState_ReturnsCorrectState()
        {
            // Arrange
            var inputState = "XX\r\n--";
            var rowCount = 2;
            var columnCount = 2;
            var steps = 2;

            // Act
            var result = ConwayHelper.GetFutureState(inputState, rowCount, columnCount, steps);

            // Assert
            Assert.Equal("--\r\n--", result);
        }

        [Fact]
        public void GetFinalState_ReturnsCorrectState()
        {
            // Arrange
            var inputState = "XX\r\n--";
            var rowCount = 2;
            var columnCount = 2;

            // Act
            var result = ConwayHelper.GetFinalState(inputState, rowCount, columnCount);

            // Assert
            Assert.Equal("", result);
        }
    }
}