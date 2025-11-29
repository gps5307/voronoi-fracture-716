public static class PatternGenerator
{
    public static List<Vector2> Generate(PatternDefinition def)
    {
        switch (def.Type)
        {
            case "circle": return GenerateCircle(def);
            case "grid": return GenerateGrid(def);
            case "hex": return GenerateHex(def);
            case "random": return GenerateRandom(def);
            case "spiral": return GenerateSpiral(def);
            case "star": return GenerateStar(def);

            default:
                throw new Exception($"Unknown pattern type: {def.Type}");
        }
    }

    private static List<Vector2> GenerateCircle(PatternDefinition def)
    {
        int count = Convert.ToInt32(def.Params["count"]);
        float radius = Convert.ToSingle(def.Params["radius"]);

        List<Vector2> points = new List<Vector2>();

        for (int i = 0; i < count; i++)
        {
            float angle = (float)(i * Math.PI * 2 / count);
            points.Add(new Vector2(
                radius * MathF.Cos(angle),
                radius * MathF.Sin(angle)
            ));
        }

        return points;
    }

    private static List<Vector2> GenerateGrid(PatternDefinition def)
    {
        int rows = Convert.ToInt32(def.Params["rows"]);
        int cols = Convert.ToInt32(def.Params["cols"]);
        float spacing = Convert.ToSingle(def.Params["spacing"]);

        List<Vector2> points = new();

        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                points.Add(new Vector2(x * spacing, y * spacing));

        return points;
    }

    private static List<Vector2> GenerateRandom(PatternDefinition def)
    {
        int count = Convert.ToInt32(def.Params["count"]);
        float w = Convert.ToSingle(def.Params["width"]);
        float h = Convert.ToSingle(def.Params["height"]);

        Random rng = new();
        List<Vector2> points = new();

        for (int i = 0; i < count; i++)
        {
            points.Add(new Vector2(
                (float)rng.NextDouble() * w,
                (float)rng.NextDouble() * h
            ));
        }

        return points;
    }


}