using System.IO;

class DebugHelper {

    public static void WriteMap(float[,] map, string path) {
        // Create a StreamWriter to write to the file
        using (StreamWriter writer = new StreamWriter(path))
        {
            // Loop through the array and write each element to the file
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    writer.Write(map[i, j].ToString() + ","); // Separate values with a tab
                }
                writer.WriteLine(); // Move to the next line for the next row
            }
        }
    }
}