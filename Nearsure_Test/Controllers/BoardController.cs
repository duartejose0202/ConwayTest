using Azure;
using Microsoft.AspNetCore.Mvc;
using ConwayGame.Models;
using ConwayGame.Services;

namespace ConwayGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : APIControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetCurrentBoardState(int id)
        {
            var response = await _boardService.GetBoardState(id);

            if (response.Successful)
            {
                return JsonResult("success", response.Data);
            }
            else return JsonResult(response.Error, null, StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBoard(IFormFile file)
        {
            // Check if file is not null
            if (file == null || file.Length == 0)
                return JsonResult("File is not selected", null, StatusCodes.Status400BadRequest);

            // Read the file content
            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();
            String[] rows = content.Split("\r\n");
            if (rows.Length == 0)
                return JsonResult("Empty file", null, StatusCodes.Status400BadRequest);

            var board = new BoardDTO
            {
                CurrentState = content,
                InitialState = content,
                CurrentStep = 0,
                RowCount = rows.Length,
                ColumnCount = rows[0].Length
            };

            var response = await _boardService.CreateBoard(board);

            if (response.Successful)
            {
                return JsonResult("success", response.Data);
            }
            else return JsonResult(response.Error, null, StatusCodes.Status400BadRequest);
        }

        [HttpGet("{id}/next")]
        public async Task<ActionResult<string>> GetNextBoardState(int id)
        {
            var response = await _boardService.GetNextBoardState(id);

            if (response.Successful)
            {
                return JsonResult("success", response.Data);
            }
            else return JsonResult(response.Error, null, StatusCodes.Status400BadRequest);
        }

        [HttpGet("{id}/states/{steps}")]
        public async Task<ActionResult<string>> GetFutureBoardState(int id, int steps)
        {
            var response = await _boardService.GetFutureBoardState(id, steps);

            if (response.Successful)
            {
                return JsonResult("success", response.Data);
            }
            else return JsonResult(response.Error, null, StatusCodes.Status400BadRequest);
        }

        [HttpGet("{id}/final")]
        public async Task<ActionResult<Board>> GetFinalBoardState(int id)
        {
            var response = await _boardService.GetFinalBoardState(id);

            if (response.Successful)
            {
                return JsonResult("success", response.Data);
            } else return JsonResult(response.Error, null, StatusCodes.Status400BadRequest);
        }
    }
}
