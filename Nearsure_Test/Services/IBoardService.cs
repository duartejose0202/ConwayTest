using ConwayGame.Models;

namespace ConwayGame.Services
{
    public interface IBoardService
    {
        Task<ServiceResponse<int>> CreateBoard(BoardDTO board);
        Task<ServiceResponse<string>> GetBoardState(int id);
        Task<ServiceResponse<string>> GetNextBoardState(int id);
        Task<ServiceResponse<string>> GetFutureBoardState(int id, int steps);
        Task<ServiceResponse<string>> GetFinalBoardState(int id);
    }
}
