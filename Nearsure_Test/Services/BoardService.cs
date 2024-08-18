using ConwayGame.Helpers;
using ConwayGame.Models;
using ConwayGame.Repositories;

namespace ConwayGame.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<ServiceResponse<int>> CreateBoard(BoardDTO board)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            try
            {
                response.Data = await _boardRepository.CreateBoard(board);
            }
            catch (Exception ex)
            {
                response.Error = $"Exception while creating a new board. Reason: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> GetBoardState(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                var board = await _boardRepository.GetBoard(id);
                response.Data = board.CurrentState;
            }
            catch (Exception ex)
            {
                response.Error = $"Exception while getting the board state. Reason: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse<string>> GetNextBoardState(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                var board = await _boardRepository.GetBoard(id);

                string nextState = ConwayHelper.GetNextState(board.CurrentState, board.RowCount, board.ColumnCount);

                // Update Database
                await _boardRepository.UpdateBoard(new BoardDTO
                {
                    ID = id,
                    CurrentState = nextState,
                    CurrentStep = board.CurrentStep + 1,
                });

                response.Data = nextState;
            }
            catch (Exception ex)
            {
                response.Error = $"Exception while getting the next board state. Reason : {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> GetFutureBoardState(int id, int steps)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var board = await _boardRepository.GetBoard(id);
                string futureState = ConwayHelper.GetFutureState(board.CurrentState, board.RowCount, board.ColumnCount, steps);

                // Update Database
                await _boardRepository.UpdateBoard(new BoardDTO
                {
                    ID = id,
                    CurrentState = futureState,
                    CurrentStep = board.CurrentStep + steps,
                });

                response.Data = futureState;
            }
            catch (Exception ex)
            {
                response.Error = $"Exception while getting next states away for board. Reason: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> GetFinalBoardState(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            try
            {
                var board = await _boardRepository.GetBoard(id);
                var finalState = ConwayHelper.GetFinalState(board.InitialState, board.RowCount, board.ColumnCount);

                if (string.IsNullOrEmpty(finalState))
                {
                    response.Error = finalState == "" ? "Final state ends with no live cells." : $"Board did not reach a final state after {ConwayHelper.MAX_ATTEMPTS} iterations.";
                    return response;
                }

                response.Data = finalState;
            } 
            catch(Exception ex)
            {
                response.Error = $"Exception while getting the final state of the board. Reason: {ex.Message}";
            }

            return response;
        }
    }
}
