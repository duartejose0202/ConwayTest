namespace ConwayGame.Models
{
    public class Board
    {
        public int ID { get; set; }
        public required string InitialState { get; set; }
        public required string CurrentState { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int CurrentStep {  get; set; }
    }
}
