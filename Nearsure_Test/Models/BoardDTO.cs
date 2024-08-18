using System.ComponentModel.DataAnnotations;

namespace ConwayGame.Models
{
    public class BoardDTO
    {
        public string CurrentState { get; set; }
        public string InitialState { get; set; }
        public int ID { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int CurrentStep { get; set; }
    }
}
