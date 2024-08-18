using ConwayGame.Models;

namespace ConwayGame.Repositories
{
    public interface IBoardRepository
    {
        Task<int> CreateBoard(BoardDTO board);
        Task<Board> GetBoard(int id);
        Task<Board> UpdateBoard(BoardDTO board);
    }
}
