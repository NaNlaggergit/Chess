using Chess;

class Program
{
    public static string GetName(Piece piece)
    {
        if (piece is King)
            return "Король";
        if (piece is Shadow)
            return "Тень";
        if (piece is Queen)
            return "Королева";
        if (piece is Rook)
            return "Ладья";
        if (piece is Bishop)
            return "Слон";
        if (piece is Knight)
            return "Конь";
        if (piece is ShadowFlame)
            return "ТеневойОгонь";
        return "";
    }
    public static string ShortGetName(Piece piece)
    {
        if (piece is King)
            return "Ki";
        if (piece is null)
            return "  ";
        if (piece is Shadow)
            return "Sh";
        if (piece is Queen)
            return "Qu";
        if (piece is Rook)
            return "Ro";
        if (piece is Bishop)
            return "Bi";
        if (piece is Knight)
            return "Kn";
        if (piece is ShadowFlame)
            return "SF";
        return "";
    }
    static void Main(string[] args)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Piece.txt");
        Board board = new Board();
        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length < 1 || lines.Length >= 10)
            throw new Exception("Количество фигур должно быть в пределах от 1<x<=10");
        foreach(string line in lines)
        {
            string[] parts = line.Split(' ');
            if(parts.Length == 3)
            {
                string name = parts[0];
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);
                board.AddPiece(name, x, y);
            }
            else
                throw new Exception("Строка должна содержать 3 аргумента разделённые пробелом");
        }
        for (int y = 0; y < board.Map.GetLength(1); y++)
        {
            for(int x = 0; x < board.Map.GetLength(0); x++)
            {
                var shortName=ShortGetName(board.Map[x, y]);
                Console.Write($"{shortName}|");
            }
            Console.Write('\n');
            Console.WriteLine("------------------------");
        }
        foreach(var piece in board.Pieces)
        {
            var list=piece.ListDestroyPiece(board);
            foreach(var element in list)
            {
                string name=GetName(piece);
                string nameEnemy = GetName(board.Map[element.X,element.Y]);
                Console.WriteLine($"{name}({piece.X},{piece.Y}) бьёт {nameEnemy}({element.X},{element.Y})");
            }
        }
        board.Pieces[0].ListDestroyPiece(board);
    }
}