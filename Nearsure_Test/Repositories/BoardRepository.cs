using ConwayGame.Models;

namespace ConwayGame.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly AppDbContext _appContext;
        public BoardRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<int> CreateBoard(BoardDTO boardDto)
        {
            var board = new Board
            {
                InitialState = boardDto.InitialState,
                CurrentState = boardDto.CurrentState,
                RowCount = boardDto.RowCount,
                ColumnCount = boardDto.ColumnCount,
                CurrentStep = boardDto.CurrentStep,
            };

            _appContext.Boards.Add(board);
            await _appContext.SaveChangesAsync();

            return board.ID;
        }

        public async Task<Board> UpdateBoard(BoardDTO boardDto)
        {
            var board = await _appContext.Boards.FindAsync(boardDto.ID);

            if (board == null)
            {
                throw new Exception("Existing board not found.");
            }

            board.CurrentState = boardDto.CurrentState;
            board.CurrentStep = boardDto.CurrentStep;

            await _appContext.SaveChangesAsync();
            return board;
        }

        public async Task<Board> GetBoard(int id)
        {
            var board = await _appContext.Boards.FindAsync(id);

            if (board == null)
            {
                throw new Exception("Existing board not found.");
            }

            return board;
        }
    }
}
