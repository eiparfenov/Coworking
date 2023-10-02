namespace Coworking.Logic.Maths;

public static class Matchmaker
{
    private static int[] mt;
    private static bool[] used;
    private static List<int>[] g;
    
    public static Dictionary<int, int> MakePairs(List<int>[] resToWp, int maxn)
    {
        g = resToWp;
        mt = Enumerable.Repeat(-1, maxn).ToArray();
        for (int i = 0; i < maxn; i++)
        {
            used = Enumerable.Repeat(false, maxn).ToArray();
            dfs(i);
        }
        used = new bool[maxn];

        var result = new Dictionary<int, int>();

        for (int i = 0; i < maxn; i++)
        {
            if (mt[i] != -1)
            {
                result[mt[i]] = i;
            }
        }

        return result;
    }

    public static bool dfs(int v)
    {
        if (used[v])
            return false;
        used[v] = true;

        foreach (int u in g[v])
        {
            if (mt[u] == -1 || dfs(mt[u]))
            {
                mt[u] = v;
                return true;
            }
        }

        return false;
    }
}