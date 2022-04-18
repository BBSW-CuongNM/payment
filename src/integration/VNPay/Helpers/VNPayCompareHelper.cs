namespace VNPay.Helpers;
public class VNPayCompareHelper : IComparer<string>
{
    public int Compare([AllowNull] string x, [AllowNull] string y)
    {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var vnpCompare = CompareInfo.GetCompareInfo("en-US");
        return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
    }
}

