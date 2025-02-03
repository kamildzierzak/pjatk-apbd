static float computeAverage (int[] arrayOfInts)
{
    float result = 0;
    for (int i = 0; i < arrayOfInts.Length; i++)
    {
        result += arrayOfInts[i];
    }
    return result / arrayOfInts.Length;
}

static float findMax(int[] arrayOfInts)
{
    int maximum = 0;

    if (arrayOfInts.Length == 0) return maximum;

    for (int i = 0; i < arrayOfInts.Length; i++)
    {
        if (arrayOfInts[i] > maximum) maximum = arrayOfInts[i];    
    }

    return maximum;
}

int[] arrayOfInts = [1, 2, 3, 4, 5, 6];

Console.WriteLine("Tablica: [" + string.Join(',', arrayOfInts) + "]");
Console.WriteLine("Średnia: " + computeAverage(arrayOfInts)); // 21 / 6 = 3.5
Console.WriteLine("Max: " + findMax(arrayOfInts));